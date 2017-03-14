using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boxer : MonoBehaviour {
	private GameObject boxer;
	private Rigidbody rb;
	public float turningRadius;
	private Vector3 offset;
	public string leftButton;
	public string rightButton;
	public string backButton;
	public string jumpButton;
	public float strength;
	public float turnStrength;
	public bool jumped = false;
	public bool timeSlow = false;
	private int killDepth = -50;
	private ResetMatch resetMatch;
	public string playerName;
	public float hitboxRemaining = 0;
//	public float damage = 1;

	public AudioClip moveLSound;
	public AudioClip moveRSound;
	public AudioClip hitSound;
	public AudioClip hardHitSound;

	private AudioSource source;

	void Awake() {

		source = GetComponent<AudioSource> ();

	}

	// Use this for initialization
	void Start () {
		boxer = this.gameObject;
		rb = GetComponent<Rigidbody> ();
		resetMatch = GameObject.Find ("SceneControl").GetComponent ("ResetMatch") as ResetMatch;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (rb.velocity);
		hitboxRemaining -= Time.deltaTime;
		if (Input.GetKeyDown(rightButton))
		{
			source.PlayOneShot (moveRSound, 1F); // sound effect
			rb.AddForce (boxer.transform.forward * strength);
			rb.AddTorque (boxer.transform.up * turnStrength);
			rb.AddTorque (boxer.transform.right * -strength);
			hitboxRemaining = .5f;
		}
		if (Input.GetKeyDown(leftButton))
		{
			source.PlayOneShot (moveLSound, 1F); // sound effect
			rb.AddForce (boxer.transform.forward * strength);
			rb.AddTorque (boxer.transform.up * -turnStrength);
			rb.AddTorque (boxer.transform.right * -strength);
			hitboxRemaining = .5f;
		}
		if (Input.GetKeyDown(backButton))
		{
			rb.AddForce (boxer.transform.forward * -strength);
			rb.AddTorque (boxer.transform.right * strength);
		}
		if (Input.GetKeyDown (jumpButton) && ! jumped) 
		{
			rb.AddForce (boxer.transform.up * strength);
			jumped = true;
		}
		if (transform.position.y < killDepth) {
			resetMatch.winner = playerName;
			SceneManager.LoadScene ("PlayerWins");
		}
	}

	void FixedUpdate() {
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, 30f);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "PlayField") {
			jumped = false;
			timeSlow = false;
		} else if (collision.gameObject.name == "JumpPad") {
			jumped = false;
			timeSlow = true;
		} else if (collision.gameObject.name.StartsWith ("Boxer") && hitboxRemaining > 0) {
			Boxer opponent = collision.gameObject.GetComponent<Boxer> ();
//			Debug.Log (opponent.damage);
			opponent.hitboxRemaining = 0;
			collision.rigidbody.AddForce (-collision.relativeVelocity * 10);
			if (collision.relativeVelocity.magnitude > 12) {
				source.PlayOneShot (hardHitSound, 1F); // sound effect
			} else {
				source.PlayOneShot (hitSound, 1F); // sound effect
			}
			//			opponent.damage += 5;
			rb.velocity = new Vector3 (0, 0, 0);
		} else if (collision.gameObject.name.StartsWith ("Rope")) {
			Debug.Log ("Hit that rope");
			rb.AddForce (collision.relativeVelocity * 25);
		}
	}
}
