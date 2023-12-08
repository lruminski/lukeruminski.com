using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Word : MonoBehaviour {

	public Word first;
	public GameObject textLetter;
	public GameObject[] letters;
	private float[] letterWidths;
	public Material material;
	public int letterNum;
	public int letterStart;
	float radius = 140f; //2.2f;
	float offset = -0.2f;
	public float phase;
	float time;
	float et;
	public float spiralRadius;
	CharacterInfo cinfo;
	public Font font;

	public Transform center;
	public float m;
	public float scaler;
	private float startTime;
	private bool position = true;//true;

	private bool created = false;
	CharacterInfo[] glyph;
	private float spin;
	void Start () {


		time = 4; //Random.Range (8, 13);


		StartCoroutine( CreateLetters() );
		FindObjectOfType<moveCam> ().enter.AddListener (Enter);
	}

	void OnDestroy()
	{
//		FindObjectOfType<moveCam> ().enter.RemoveListener (Enter);
	}

	void Update () {

		if (position) {
			et = (Time.time - startTime);

			PositionLetters ();
		}
	}

	public void Enter ()
	{
		//DelayedStart ();

		Invoke ("DelayedStart", 4f);

	}

	void DelayedStart ()
	{
		Debug.Log ("Delayed Start Word");
		startTime = Time.time;
//		spiralRadius = 0;
		float off = (letterStart - first.letterStart) * 0.05f;
		DOVirtual.Float(10, spiralRadius, 10, setRadius).SetEase(Ease.InOutQuint).SetDelay(4 + off);
		DOVirtual.Float(145, 360, 10, setSpin).SetEase(Ease.InOutQuint).SetDelay(4 + off);

		DOVirtual.Float(10, 250, 4, setScale).SetEase(Ease.Linear);

		position = true;
		spiralRadius = 10;

//		Invoke ("Done", 15);
	}

	public void Done()
	{
		position = false;
	}

	public void setRadius(float value)
	{
		spiralRadius = value;
	}

	public void setSpin(float value)
	{
		spin = value;
	}

	public void setScale (float value)
	{
		transform.localScale = new Vector3 (value, value, value);
	}

	void PositionLetters()
	{

		if (!created)
			return;
		
		bool done = true;
//		float x = 0;
		float rad;
		float pos = 0;
		float letterdeg = letterNum * 1.95f;


		for (int i = 0; i < letters.Length ; i++) {



			Transform t = letters[i].transform;


			float deg = -spin  + (float)(letterStart)/(letterNum-1) * letterdeg+ pos+ (180-letterdeg) / 2;// - spin;


			rad = deg * Mathf.PI / 180f;
			pos += glyph [i].advance * m;// * 1500f;

			t.position = center.position +  new Vector3 (-(Mathf.Cos (rad)) * spiralRadius, transform.localPosition.y, -(Mathf.Sin (rad) * spiralRadius));// / transform.localScale.x;
			t.localRotation = Quaternion.Euler (0, ((270f-deg))/1f, 0);

		}	

		if (done) {
			//position = false;
		}
	}

	IEnumerator CreateLetters()
	{
		int i = 0;
		GameObject[] newLetters = new GameObject[letters.Length];
		glyph = new CharacterInfo[letters.Length];


		letterWidths = new float[letters.Length];

		string word = "";

		Text text;

		GameObject letterText = Instantiate (textLetter);
		letterText.GetComponent<Text> ().text = "";


		foreach (var l in letters) {
			string txt = l.name.Replace ("_", "");
			text = letterText.GetComponent<Text> ();

			text.text = txt;

			CharacterInfo cinfo;
			Font font = letterText.GetComponent<Text> ().font;


			yield return  new WaitForEndOfFrame ();
			yield return  new WaitForEndOfFrame ();

			font.GetCharacterInfo (txt [0], out cinfo);//, text.fontSize, text.fontStyle);

			Vector2 s = letterText.GetComponent<RectTransform> ().sizeDelta;

			glyph[i] = cinfo;


			word += txt;

			GameObject letter = Instantiate (l, transform);
			letter.layer = gameObject.layer;

			letter.GetComponent<Renderer> ().material = material;

			letter.transform.rotation = Quaternion.identity;

			letter.transform.localRotation = Quaternion.Euler(0, 180, 0);
			letter.transform.localScale = new Vector3 (10, 10, 10);


			yield return  new WaitForEndOfFrame ();

			letterWidths [i] = letter.GetComponent<Renderer> ().bounds.size.x;//*0.65f;

			newLetters [i] = letter;
			i++;
		}
		letters = newLetters;
		created = true;
		PositionLetters ();
	}

}
