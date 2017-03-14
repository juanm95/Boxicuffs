using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public float healthScale;
    public Boxer boxerScript;

    // Use this for initialization
    void Start()
    {
        GameObject boxerParent = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        boxerScript = boxerParent.GetComponent<Boxer>();
    }
    // Update is called once per frame
        void Update () {
            SetHealth();
	}

    void SetHealth()
    {
        healthScale = boxerScript.health / 100;
        gameObject.transform.localScale = new Vector3(healthScale, 1, 1);
    }

}
