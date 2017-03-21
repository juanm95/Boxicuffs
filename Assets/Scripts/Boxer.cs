using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    private float HEALTH_DROP_MAGNITUDE = 0.5f;
    private bool zLocked = true;
    private float knockdownTimer = 7.0f;
    private bool onGround;
    private GameObject healthBar;
    private GameObject countdownTimerGameObj;
    private CountdownTimer countdownScript;
//	public float damage = 1;

	public AudioClip moveLSound;
	public AudioClip moveRSound;
	public AudioClip hitSound;
	public AudioClip hardHitSound;
	private float healthLoss;
	public float lastSpeed;
	private ParticleSystem hair;

	private AudioSource source;

	void Awake() {

		source = GetComponent<AudioSource> ();
		hair = GetComponentInChildren<ParticleSystem>();

	}

	// Use this for initialization
	void Start () {
		boxer = this.gameObject;
		rb = GetComponent<Rigidbody> ();
		resetMatch = GameObject.Find ("SceneControl").GetComponent ("ResetMatch") as ResetMatch;
        health = 100;
        startHealth = health;
		healthLoss = 0;
        healthBar = gameObject.transform.FindChild("Cube/HealthBarCanvas").gameObject;
        countdownTimerGameObj = gameObject.transform.FindChild("Cube/CountdownTimer").gameObject;
        countdownScript = countdownTimerGameObj.GetComponent<CountdownTimer>();
        healthBar.SetActive(true);
        countdownTimerGameObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		health -= healthLoss;
        if (health < 0)
        {
            health = 0;
        }
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
        if (health <= 0)
        {
            if (zLocked) Dizzy();
            RecoveryCountdown();
        }
		lastSpeed = rb.velocity.magnitude;
	}
    void Dizzy()
    {
        gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        zLocked = false;
        healthBar.SetActive(false);
        countdownTimerGameObj.SetActive(true);
    }
    void RecoveryCountdown()
    {
        float countdownTime = countdownScript.currTime;
        Debug.Log(onGround);
        Debug.Log(zLocked);
        Debug.Log(detectIfUpright());
        Debug.Log(countdownTime);
        if (onGround && !zLocked && detectIfUpright() && countdownTime <= 4.0f && gameObject.GetComponent<Rigidbody>().velocity.magnitude < 5.0f)
        {
            Debug.Log("Has recovered");
            gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Quaternion currRot = gameObject.transform.rotation;
            Quaternion newRot = currRot;
            newRot[0] = 0;
            newRot[2] = 0;
            gameObject.transform.rotation = Quaternion.Slerp(currRot, newRot, 1);
            zLocked = true;
            Recovery();
            healthBar.SetActive(true);
            countdownTimerGameObj.SetActive(false);
        }
        if (!zLocked && countdownTime <= 0.0f)
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
        SceneManager.LoadScene("BarScenePlayerWins");
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("floor") || collision.gameObject.name.StartsWith("RingBase"))
        {
            onGround = false;
        }
    }


    void OnCollisionEnter(Collision collision) {
		jumped = false;
		if (collision.gameObject.name.StartsWith ("BoxerBody") && hitboxRemaining > 0) {
			Debug.Log (playerName + ": " + collision.relativeVelocity);
			Boxer opponent = collision.gameObject.GetComponent<Boxer> ();
			if (lastSpeed > opponent.lastSpeed) { // If tie, no hit.
				hair.Play();
				Debug.Log(playerName + ": " + lastSpeed + ", " + opponent.playerName + ": " + opponent.lastSpeed);
				opponent.hitboxRemaining = 0;
				opponent.GetComponent<Rigidbody> ().AddExplosionForce (collision.impulse.magnitude * 8.0f, collision.transform.position, 10.0f, collision.impulse.magnitude * 300.0f);
				gameObject.transform.GetComponent<Rigidbody> ().AddExplosionForce (80.0f, collision.transform.position, 10.0f, collision.impulse.magnitude);
				if (collision.relativeVelocity.magnitude > 12) {
					source.PlayOneShot (hardHitSound, 1F); // sound effect
				} else {
					source.PlayOneShot (hitSound, 1F); // sound effect
				}
				rb.velocity = new Vector3 (0, 0, 0);
				opponent.health = opponent.health - collision.relativeVelocity.magnitude * HEALTH_DROP_MAGNITUDE;
				if (opponent.health < 0)
					opponent.health = 0;
			}
		} else if (collision.gameObject.name.StartsWith ("Rope")) {
			rb.AddForce (collision.relativeVelocity * 25);
            if (rb.velocity.magnitude > 1)
            {
                hitboxRemaining = 0.5f;
            }
		}
		if (collision.gameObject.name.StartsWith ("floor")) {
			strength = 450;
			healthLoss = .15f;
            onGround = true;
		} else if (collision.gameObject.name.StartsWith ("RingBase")) {
			strength = 200;
			healthLoss = 0;
            onGround = true;
		}
	}

    bool detectIfUpright()
    {
        return transform.up.y > 0.8;
    }

    void Recovery()
    {
        health = startHealth - 33;
        startHealth = health;
        if (health < 0)
        {
            PlayerLoss();
        }
    }
}
