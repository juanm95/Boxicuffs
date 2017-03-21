using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {
    public TextMesh display;
    public float currTime = 7.0f;
    public Boxer boxerScript;
    public GameObject playerCam;
	// Use this for initialization
	void Start () {
        GameObject boxerParent = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        playerCam = boxerParent.transform.parent.FindChild("Camera").gameObject;
        boxerScript = boxerParent.GetComponent<Boxer>();
        display = gameObject.GetComponent<TextMesh>();

	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.up = new Vector3(0, 0, 0);
        gameObject.transform.LookAt(playerCam.transform);
        gameObject.transform.Rotate(0, 180, 0);
		if (gameObject.activeInHierarchy)
        {
            currTime = currTime - Time.deltaTime;
            if (currTime <= 0.0f) {
                currTime = 0;
             }
            if (currTime < 3.0f)
            {
                display.fontStyle = FontStyle.BoldAndItalic;
            }
            display.text = currTime.ToString("N2");
        }
       
	}
}
