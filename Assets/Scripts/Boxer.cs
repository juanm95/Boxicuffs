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

	// Use this for initialization
	void Start () {
		boxer = this.gameObject;
		rb = GetComponent<Rigidbody> ();
		resetMatch = GameObject.Find ("SceneControl").GetComponent ("ResetMatch") as ResetMatch;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(rightButton))
		{
			rb.AddForce (boxer.transform.forward * strength);
			rb.AddTorque (boxer.transform.up * turnStrength);
			rb.AddTorque (boxer.transform.right * -strength);
		}
		if (Input.GetKeyDown(leftButton))
		{
			rb.AddForce (boxer.transform.forward * strength);
			rb.AddTorque (boxer.transform.up * -turnStrength);
			rb.AddTorque (boxer.transform.right * -strength);
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
		Debug.Log (transform.position.y);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "PlayField") {
			jumped = false;
			timeSlow = false;
		}
		if (collision.gameObject.name == "JumpPad") {
			jumped = false;
			timeSlow = true;
		}
	}
}
