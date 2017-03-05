using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetMatch : MonoBehaviour {

	public string winner;
	public string scene;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("r")) {
			SceneManager.LoadScene (scene);
		}
	}
}
