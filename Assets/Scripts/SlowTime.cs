using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour {
	public Boxer Player1;
	public Boxer Player2;

	// Use this for initialization
	void Start () {
		Debug.Log ("Fixed Delta Time: ");
		Debug.Log (Time.fixedDeltaTime);
//		Time.timeScale = 0.5F;
//		Time.fixedDeltaTime = 0.01F;
	}

	// Update is called once per frame
	void Update () {
//		if (Player1.timeSlow || Player2.timeSlow) {
//			Time.timeScale = 0.5f;
//			Time.fixedDeltaTime = 0.01f;
//		} else {
//			Time.timeScale = 1.0f;
//			Time.fixedDeltaTime = 0.02f;
//		}
	}
}
