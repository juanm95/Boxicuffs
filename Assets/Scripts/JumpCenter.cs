using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCenter : MonoBehaviour
{

    public float upStrength;
    public float centerStrength;
    public Vector3 center; 


    void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce(this.gameObject.transform.up * upStrength + this.gameObject.transform.TransformDirection(center) * centerStrength);
    }
}