using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuButtonPress : MonoBehaviour {

    public Color inactive;
    public Color active;
    public string key;

    private Image background;

	// Use this for initialization
	void Start () {
        background = gameObject.GetComponent<Image>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            background.color = active;
        }
        if (Input.GetKeyUp(key))
        {
            background.color = inactive;
        }
    }

}
