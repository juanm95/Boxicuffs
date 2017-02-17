using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	private Transform follow;
	public float CameraHeightOffGround;
	public float CameraDistanceFromPlayer;
	public float smooth;
	// Use this for initialization
	void Start () {
		follow = player.transform;
	}

	// Gauranteed to run after all objects are processed. This ensures that we capture any player movement.
	void LateUpdate () {
		if ((player.GetComponent ("Boxer") as Boxer).jumped) {
			CameraDistanceFromPlayer = 6.0f;
			CameraHeightOffGround = 6.0f;
		}
		Vector3 followUp = new Vector3 (follow.up.x, Mathf.Abs(follow.up.y), follow.up.z);
		Vector3 targetPosition = follow.position + followUp * CameraHeightOffGround - follow.forward * CameraDistanceFromPlayer;
//		transform.position = targetPosition;
		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		transform.LookAt (follow);
	}
}
