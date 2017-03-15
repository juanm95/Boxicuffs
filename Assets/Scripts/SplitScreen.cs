using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour {
	public Camera left;
	public Camera right;
	// Use this for initialization
	void Start () {
		left.rect = new Rect (0.0f, 0.0f, 0.5f, 1.0f);
		right.rect = new Rect (0.5f, 0.0f, 0.5f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
