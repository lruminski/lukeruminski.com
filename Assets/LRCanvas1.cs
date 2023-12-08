using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LRCanvas1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	public void Enter()
	{
		transform.DOMove (new Vector3 (0f, 0f, 500f), 11).SetEase (Ease.InOutQuart);//, 1, 1);
		//transform.DOMove(new Vector3(1f, 1f, 500f), 5f);
	}

	void changeSize(float val)
	{
//		GetComponent<Text> ().fontSize = (int) Mathf.Round(val);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
