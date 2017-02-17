using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour {
	public Camera player1;
	public Camera player2;
	// Use this for initialization
	void Start () {
		player1.rect = new Rect (0.0f, 0.0f, 0.5f, 1.0f);
		player2.rect = new Rect (0.5f, 0.0f, 0.5f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
