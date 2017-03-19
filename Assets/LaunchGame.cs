using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LaunchGame : MonoBehaviour {
    public string sceneName;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadLevel);//
    }

    void LoadLevel()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
