using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecentWork : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}

	public void Enter()
	{
		for (int i = 0; i < transform.childCount; i++) {

			//			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.SetFloat ("_GlowPower", 0);
			//			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.SetColor ("_FaceColor", new Color(0, 0, 0, 0));
			transform.GetChild (i).GetComponent<RectTransform>().DOLocalMoveX(0, 7.5f).SetEase(Ease.OutSine).SetDelay(i*1.25f);
		}
	}

	public void Hide()
	{
		for (int i = 0; i < transform.childCount; i++) {

			//			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.SetFloat ("_GlowPower", 0);
			//			transform.GetChild (i).GetChild (0).GetComponent<TextMeshPro> ().fontMaterial.SetColor ("_FaceColor", new Color(0, 0, 0, 0));
//			transform.GetChild (i).GetComponent<RectTransform>().DOLocalMoveX(0, 4f).SetEase(Ease.InSine);
			transform.GetChild (i).GetComponent<RectTransform>().DOLocalMoveX(1500, 4f).SetEase(Ease.InSine).SetDelay(i*0.6f);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
