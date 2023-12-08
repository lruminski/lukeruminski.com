using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class fadein : MonoBehaviour {

	void Awake()
	{
		GetComponent<Text> ().material.color = new Color (0, 0, 0, 0);
	}

	public void Enter () {
		GetComponent<Text> ().material.DOColor(Color.white, "_Color", 5).SetDelay(7);
	}
	
}
