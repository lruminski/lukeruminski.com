using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class moveCam : MonoBehaviour {

	public UnityEvent enter;
	public Transform sun;

	void Awake()
	{
		sun.position = new Vector3 (0, -375, 500);
	}

	// Use this for initialization
	void Start () {
	}

	public void EnterClicked()
	{
		enter.Invoke ();

		transform.DOMove (new Vector3 (0, -125, -10), 11).SetDelay (6).SetEase (Ease.InOutQuart);

		sun.DOMove (new Vector3 (0, -185, 500), 11).SetDelay (6).SetEase (Ease.InOutQuart);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
