using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		VideoPlayer videoPlayer = GetComponent<VideoPlayer> ();
		videoPlayer.url = "Assets/StreamingAssets/test.mp4";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
