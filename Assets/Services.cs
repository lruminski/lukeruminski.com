using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Services : MonoBehaviour {
	Vector3 angle = new Vector3(100, 17, -15);
	Vector3 offsetAngle = new Vector3 (0, 0, 0);
	public Material textMaterial;

	public int contentIdx = 0;

	// Use this for initialization
	void Start () {
		textMaterial.SetFloat ("_GlowPower", 0);
		textMaterial.SetColor ("_FaceColor", new Color(0, 0, 0, 0));

		/*
		for (int i = 0; i < transform.childCount; i++) {

			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial = new Material (textMaterial);
			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.SetFloat ("_GlowPower", 0);
			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.SetColor ("_FaceColor", new Color(0, 0, 0, 0));

//				.SetActive (false);


		}
*/
		Hide ();
		Invoke ("Disable", 3f);
	}

	void Disable()
	{
		gameObject.SetActive (false);
	}

	void OnDestroy()
	{
		Debug.Log ("destroyed");
		for (int i = 0; i < transform.childCount; i++) {

//			DestroyImmediate (transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial);
			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial = textMaterial;
		}
		textMaterial.SetFloat ("_GlowPower", 1);
		textMaterial.SetColor ("_FaceColor", new Color(0, 0, 0, 1));
	}

	// Update is called once per frame
	void Update () {
		offsetAngle += new Vector3 (-30, 0, 0) * Time.deltaTime;
		transform.localRotation = Quaternion.Euler (angle + offsetAngle);
	}

	public void Enter()
	{
		offsetAngle = Vector3.zero;

		for (int i = 0; i < transform.childCount; i++) {

//			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.SetFloat ("_GlowPower", 0);
//			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.SetColor ("_FaceColor", new Color(0, 0, 0, 0));
			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.DOKill ();
			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.DOFade (1, "_FaceColor", 1.6f).SetDelay (i * 1.8f);
			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.DOFloat (1, "_GlowPower", 1.6f).SetDelay (i * 1.8f);
				
//			transform.GetChild (i).gameObject.SetActive (false);
//			Invoke ("EnableContent", 0.2f * i); 
		}

	}

	public void Hide()
	{
		for (int i = 0; i < transform.childCount; i++) {

			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.DOKill ();
			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.DOFloat (0, "_GlowPower", 3f);
			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.DOFade (0, "_FaceColor", 2f);

		}

		Invoke ("Disable", 3f);
	}


	public void EnableContent()
	{

		transform.GetChild (contentIdx).gameObject.SetActive (true);

		contentIdx++;
	}


}
