using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetName : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ResetMatch reset = GameObject.Find ("SceneControl").GetComponent ("ResetMatch") as ResetMatch;
		gameObject.GetComponent<TextMesh> ().text = reset.winner + " wins";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
