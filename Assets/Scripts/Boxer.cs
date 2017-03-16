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
    public float health;
    private float startHealth;
    private float HEALTH_DROP_MAGNITUDE = 0.25f;
    private bool zLocked = true;
    private float knockdownTimer = 7.0f;
//	public float damage = 1;

	public AudioClip moveLSound;
	public AudioClip moveRSound;
	public AudioClip hitSound;
	public AudioClip hardHitSound;
	private float healthLoss;

	private AudioSource source;

	void Awake() {

		source = GetComponent<AudioSource> ();

	}

	// Use this for initialization
	void Start () {
		boxer = this.gameObject;
		rb = GetComponent<Rigidbody> ();
		resetMatch = GameObject.Find ("SceneControl").GetComponent ("ResetMatch") as ResetMatch;
        health = 100;
        startHealth = health;
		healthLoss = 0;
	}
	
	// Update is called once per frame
	void Update () {
		health -= healthLoss;
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
            PlayerLoss();
		}
        if (health <= 0 && zLocked)
        {
            gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            zLocked = false;
            Invoke("RecoveryCountdown", knockdownTimer);
          
            
        }
        
	}

    void RecoveryCountdown()
    {
        if (detectIfUpright() && !zLocked)
        {
            gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Quaternion currRot = gameObject.transform.rotation;
            Quaternion newRot = currRot;
            newRot[0] = 0;
            newRot[2] = 0;
            gameObject.transform.rotation = Quaternion.Slerp(currRot, newRot, 1);
            zLocked = true;
            Recovery();
        }
        if (!zLocked)
            {
            PlayerLoss();
            }
    }

	void FixedUpdate() {
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, 30f);
	}


    void PlayerLoss()
    {
        resetMatch.winner = playerName;
        SceneManager.LoadScene("PlayerWins");
    }


	void OnCollisionEnter(Collision collision) {
		jumped = false;
		if (collision.gameObject.name.StartsWith ("BoxerBody") && hitboxRemaining > 0) {
			Boxer opponent = collision.gameObject.GetComponent<Boxer> ();
//			Debug.Log (opponent.damage);
			opponent.hitboxRemaining = 0;
			//collision.rigidbody.AddForce (-collision.relativeVelocity * 10);
            opponent.GetComponent<Rigidbody>().AddExplosionForce(80.0f, collision.transform.position, 2.0f, 1.0f);
//			opponent.damage += 5;
			collision.rigidbody.AddForce (-collision.relativeVelocity * 10);
			if (collision.relativeVelocity.magnitude > 12) {
				source.PlayOneShot (hardHitSound, 1F); // sound effect
			} else {
				source.PlayOneShot (hitSound, 1F); // sound effect
			}
			//			opponent.damage += 5;
			rb.velocity = new Vector3 (0, 0, 0);
            opponent.health = opponent.health - collision.relativeVelocity.magnitude * HEALTH_DROP_MAGNITUDE;
            if (opponent.health < 0) opponent.health = 0;
		} else if (collision.gameObject.name.StartsWith ("Rope")) {
			rb.AddForce (collision.relativeVelocity * 25);
            if (rb.velocity.magnitude > 1)
            {
                hitboxRemaining = 0.5f;
            }
		}
		if (collision.gameObject.name.StartsWith ("floor")) {
			strength = 400;
			healthLoss = .5f;
		} else if (collision.gameObject.name.StartsWith ("RingBase")) {
			strength = 200;
			healthLoss = 0;
		}
	}

    bool detectIfUpright()
    {
        return transform.up.y > 0.6;
    }

    void Recovery()
    {
        health = startHealth - 25;
        startHealth = health;
        if (health <= 0)
        {
            PlayerLoss();
        }
    }
}
