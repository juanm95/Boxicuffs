using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;
	public GameObject opponent;
	public Rigidbody rb;
	private Vector3 offset;
	private Transform follow;
	public float CameraHeightOffGround;
	public float CameraDistanceFromPlayer;
	public float smooth;

	private AudioSource source;

	public AudioClip backgroundMusic;

	void Awake() {

		source = GetComponent<AudioSource> ();
		source.volume = 0.4F;
		source.Play ();

	}

	// Use this for initialization
	void Start () {
		follow = player.transform;
		rb = player.GetComponent<Rigidbody> ();
	}

	// Gauranteed to run after all objects are processed. This ensures that we capture any player movement.
	void LateUpdate () {
		if ((player.GetComponent ("Boxer") as Boxer).jumped) {
			CameraDistanceFromPlayer = 6.0f;
			CameraHeightOffGround = 6.0f;
		}
		Vector3 followUp = new Vector3 (follow.up.x, Mathf.Abs(follow.up.y), follow.up.z);
//		Vector3 targetPosition = - CameraDistanceFromPlayer * rb.velocity.normalized;
//		targetPosition.y = CameraHeightOffGround;
//		Debug.Log (targetPosition);
		Vector3 targetPosition = follow.position + followUp * CameraHeightOffGround - (opponent.transform.position - follow.position).normalized * CameraDistanceFromPlayer;
//		transform.position = Vector3.Lerp(targetPosition + follow.position, Time.deltaTime * smooth);
		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		transform.LookAt (opponent.transform);
	}
}