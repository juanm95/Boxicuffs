using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour {

	private Animator anim;
	private float waitTime;
	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		waitTime = Random.value * 2;
	}

	// Update is called once per frame
	void Update () {
		waitTime -= Time.deltaTime;
		if (waitTime < 0) {
			anim.SetBool ("started", true);
		}
	}
}
