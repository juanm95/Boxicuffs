using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public float healthScale;
    public Boxer boxerScript;
    public GameObject moustache;
    public Vector3 origMoustacheScale;

    // Use this for initialization
    void Start()
    {
        GameObject boxerParent = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        boxerScript = boxerParent.GetComponent<Boxer>();
        moustache = boxerParent.transform.FindChild("moustacheObj").gameObject;
        origMoustacheScale = moustache.transform.localScale;
    }
    // Update is called once per frame
        void Update () {
            SetHealth();
	}

    void SetHealth()
    {
        healthScale = boxerScript.health / 100;
        gameObject.transform.localScale = new Vector3(healthScale, 1, 1);
        ShrinkMoustache();
    }

    void ShrinkMoustache()
    {
        moustache.transform.localScale =  origMoustacheScale * healthScale;
    }

}
