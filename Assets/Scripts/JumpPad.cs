using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

	public float explosionStrength;

	void OnCollisionEnter(Collision collision) {
		collision.rigidbody.AddForce (this.gameObject.transform.up * explosionStrength);
	}
}