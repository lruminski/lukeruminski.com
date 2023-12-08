using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Nav : MonoBehaviour {

	public enum Sections { Home, About, Services, Recent, Intro };

	public Transform AboutContent;
	public Transform ServiceContent;
	public Transform RecentContent;

	public Shader AboutShader;
	public Transform sun;
	public Sections section;

	private Collider[] nav;
	private bool transition;

	void Awake()
	{
		AboutContent.gameObject.SetActive (false);
		RecentContent.gameObject.SetActive (false);
	}

	void Start () {
		section = Sections.Intro;

		nav = GetComponentsInChildren<Collider>();
	}

	public void Enter()
	{
		Invoke ("IntroFinished", 15f);
	}

	private void IntroFinished()
	{
		section = Sections.Home;
	}
	
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			GoHome ();
		}

		if (section == Sections.Intro || transition) {
			return;
		}

		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);


				if(hitInfo)
				{
					Debug.Log( hitInfo.transform.gameObject.name );
				}
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Clicked");

			RaycastHit hit;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			if (Physics.Raycast (ray, out hit, 1000.0f)) {
				Debug.Log ("hit: " + hit.collider.gameObject.name);

				if (section == Sections.Home) {
					if (hit.collider.gameObject.name == "about") {
						About ();
					} else if (hit.collider.gameObject.name == "services") {
						Services ();
					} else if (hit.collider.gameObject.name == "recent") {
						RecentWork ();
					}
				} else {
					Debug.Log ("we are not home");

					if (hit.collider.gameObject.name == "OpenURL_HHOF") {
						//Application.OpenURL("https://www.hhof.com/htmlExhibits/exwoh00.shtml");
						Application.ExternalEval("window.open(\"https://www.hhof.com/htmlExhibits/exwoh00.shtml\")");
					}
					if (hit.collider.gameObject.name == "OpenURL_DMC") {
						Application.ExternalEval("window.open(\"https://www.drawmeclo.se/\")");
					}
					if (hit.collider.gameObject.name == "OpenURL_BOD") {
						Application.ExternalEval("window.open(\"https://www.nfb.ca/interactive/the_book_of_distance/\")");
					}
					if (hit.collider.gameObject.name == "OpenURL_BH") {
						Application.ExternalEval("window.open(\"https://vimeo.com/569372622/e092a823d2\")");
					}



				}
			} else {
				Debug.Log ("hit nothing");
				StartCoroutine ("Home");
			}



			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			if(hitInfo)
			{
				Debug.Log( hitInfo.transform.gameObject.name );
				// Here you can check hitInfo to see which collider has been hit, and act appropriately.
			}


			if (section != Sections.Home) {
				
			}

		}
	}

	private IEnumerator Home()
	{

		if (section == Sections.About) {
			HideAbout ();
		} else if (section == Sections.Services) {
			HideServices ();
		} else if (section == Sections.Recent) {
			HideRecent ();
		}

		Sections lastSection = section;


		section = Sections.Home;

		Debug.Log ("section: " + section + ", last: " + lastSection);

		yield return new WaitForSeconds (4f);

		if (lastSection == Sections.About) {
			AboutContent.GetComponent<RectTransform> ().DOKill ();
			AboutContent.gameObject.SetActive (false);
		} else if (lastSection == Sections.Recent) {
//			AboutContent.gameObject.SetActive (false);
		}

	}

	IEnumerator Transition()
	{
		Debug.Log ("transition start");
		transition = true;

		yield return new WaitForSeconds (4f);

		transition = false;
		Debug.Log ("transition end");
	}

	private void HideNav()
	{
		StartCoroutine ("Transition");

		foreach (Collider c in nav)
		{
			c.enabled = false;
		}

	}

	private void ShowNav()
	{
		CancelInvoke ("GoHome");
		foreach (Collider c in nav)
		{
			c.enabled = true;
		}
	}

	private void RecentWork()
	{
		HideNav ();

		section = Sections.Recent;

		sun.DOKill ();
		sun.DOMove (new Vector3 (0, -185f, 250), 3.75f);

		RecentContent.gameObject.SetActive (true);
		RecentContent.GetComponent<RecentWork> ().Enter ();

		Camera.main.transform.DOMove (new Vector3 (0, -180, 50), 10).SetEase(Ease.InOutSine);
		Camera.main.transform.DORotate (new Vector3 (-58, 0, -90), 10).SetEase(Ease.InOutSine);
	}

	private void Services()
	{
		HideNav ();

		section = Sections.Services;
		ServiceContent.gameObject.SetActive (true);
		ServiceContent.GetComponent<Services> ().Enter ();

//		ServiceContent.GetComponent<Services> ().textMaterial.DOFade (1, "_FaceColor", 3.75f);
//		ServiceContent.GetComponent<Services> ().textMaterial.DOFloat (1, "_GlowPower", 3.75f);

		Camera.main.transform.DOMove (new Vector3 (0, -250f, -10f), 3.75f).SetEase(Ease.InOutSine);

		sun.DOMove (new Vector3 (0, 385f, 500), 3.75f);

	}


	private void About()
	{
		HideNav ();

		section = Sections.About;

		AboutContent.GetComponent<RectTransform> ().DOKill ();

		Camera.main.transform.DOKill ();
		Camera.main.transform.DOMove (new Vector3 (0, -125f, 175f), 3.75f);

		sun.DOKill ();
		sun.DOMove (new Vector3 (0, -185f, 0), 3.75f);

		Invoke ("SetAboutActive", 2.5f); 
	}

	public void SetAboutActive()
	{		

		AboutContent.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (0, -55, 100.75f);
		AboutContent.gameObject.SetActive (true);
		TextMeshProUGUI tmp = AboutContent.GetComponent<TextMeshProUGUI> ();
		tmp.fontMaterial.SetColor ("_FaceColor", Color.white);
//		AboutContent.GetComponent<CanvasRenderer> ().GetMaterial().DOFade(1, "_FaceColor", 1f).SetDelay(0.5f);

		AboutContent.GetComponent<RectTransform> ().DOLocalMove (new Vector3 (0, 600f, 100.75f), 45f).SetEase (Ease.Linear);

		Invoke ("GoHome", 36f);
	}

	public void GoHome()
	{
		StartCoroutine ("Home");
	}

	public void HideAbout()
	{
		ShowNav ();
		AboutContent.GetComponent<TextMeshProUGUI> ().fontMaterial.DOFade(0, "_FaceColor", 4);

		Camera.main.transform.DOKill ();
		Camera.main.transform.DOMove (new Vector3 (0, -125f, -10f), 5f).SetDelay(0.25f).SetEase(Ease.InOutSine);

		sun.DOKill ();
		sun.DOMove (new Vector3 (0, -185f, 500f), 5).SetDelay(0.25f).SetEase(Ease.InOutSine);


	}
	public void HideServices()
	{
		ShowNav ();
		//		AboutContent.GetComponent<CanvasRenderer> ().GetMaterial().DOFade(0, "_FaceColor", 5);
		Camera.main.transform.DOKill();
		Camera.main.transform.DOMove (new Vector3 (0, -125f, -10f), 5f).SetDelay(0.25f).SetEase(Ease.InOutSine);

		ServiceContent.GetComponent<Services> ().Hide ();
//		ServiceContent.GetComponent<Services> ().textMaterial.DOFade (0, "_FaceColor", 3.75f);
//		ServiceContent.GetComponent<Services> ().textMaterial.DOFloat (0, "_GlowPower", 3.75f);

		sun.DOKill ();
		sun.DOMove (new Vector3 (0, -185f, 500f), 5).SetDelay(0.25f).SetEase(Ease.InOutSine);
	}

	public void HideRecent()
	{
		ShowNav ();
		//		AboutContent.GetComponent<CanvasRenderer> ().GetMaterial().DOFade(0, "_FaceColor", 5);
		Camera.main.transform.DOKill();
		Camera.main.transform.DOMove (new Vector3 (0, -125f, -10f), 5f).SetDelay(0.25f).SetEase(Ease.InOutSine);
		Camera.main.transform.DORotate (new Vector3 (0, 0, 0), 5f).SetDelay (0.25f).SetEase (Ease.InOutSine);

		RecentContent.GetComponent<RecentWork> ().Hide ();

		sun.DOKill ();
		sun.DOMove (new Vector3 (0, -185f, 500f), 5).SetDelay(0.25f).SetEase(Ease.InOutSine);
	}
}
