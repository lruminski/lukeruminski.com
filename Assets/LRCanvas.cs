using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LRCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DOScale(new Vector3(1, 1, 1), 5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
