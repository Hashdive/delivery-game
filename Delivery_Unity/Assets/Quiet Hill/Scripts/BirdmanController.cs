using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdmanController : MonoBehaviour {

	
	//movement & targeting
	private LookForward myLookForward;
	private Transform targetPlayer;
	private EnemySpawnerScript spawner;
	private float walkTimer;
	public float walkDelay = 9;

	
	// animation variables
	private Rigidbody2D myRigidBody;
	private Animator myAnim;
	private bool isWalking = true;
	public bool isAttacking = true;
	public bool isActing = false;
	public bool isAlive = true;
	public GameObject blood;
	
	// speed and health
	public float speed = 0.3f;
	public int health;

	// Use this for initialization
	void Start () {
		
		// get Rigid Body
		myRigidBody = GetComponent<Rigidbody2D> ();

		// get Animator
		myAnim = GetComponent<Animator> ();

		// get  Look Forward Script
		myLookForward = GetComponent<LookForward> ();

		// assign Player
		targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

		// get Enemy Spawner Script
		spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawnerScript>();
		health =  6;
		 
	}
	
	
	// Update is called once per frame
	void Update () {

		// detect movement
		float vel = 0f;
		vel = Mathf.Abs (myRigidBody.velocity.x);

		// animation variables
		myAnim.SetFloat ("speed", vel);
		myAnim.SetBool ("isAttacking", isAttacking);
		myAnim.SetBool ("isActing", isActing);
		myAnim.SetBool ("isAlive", isAlive);

		if (isWalking && !isAttacking) {

			// follow player
			transform.position = Vector3.MoveTowards (transform.position, targetPlayer.position, speed * Time.deltaTime);	
		} else {

			// stay put
			myRigidBody.velocity = new Vector3 (0f, myRigidBody.velocity.y, 0f);
		}

		if (isWalking && !isAttacking) {

			// always face player
			if (transform.position.x > targetPlayer.position.x) {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
			else {
				transform.localScale = new Vector3 (1f,1f,1f);
			}
		}

		// walk delay logic to stagger Birdman following Player
		walkTimer += Time.deltaTime;
		if(walkTimer >= walkDelay){
			switchWalk ();
		}	

	}

	void switchWalk(){
		if (isWalking) {
			isWalking = false;

			// Birdman waits longer than it follows 
			walkTimer = walkDelay - 20;
		} else {
			if(!isAlive){
				Destroy(this.gameObject);
			}
			isWalking = true;
			walkTimer = 0;
		}
	}

	// attack
	public void engage(){
		isAttacking = true;
	}

	// stop attacking
	public void stopAttack(){
		isAttacking = false;
	}

	// check to see if hit by Player 
	public void checkHurt(){
		myLookForward.hitbox ();
	}

	// Birdman hit by Player
	public void hurtBirdman(){
		if (!isAlive) {
			return;
		}
		// create blood & animate
		Instantiate (blood, gameObject.transform.position, gameObject.transform.rotation);	
		isActing = true;
		myAnim.Play ("birdman-hurt");
		
		// damage logic
		int weaponType = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().weaponType;
		if (weaponType == 1 || weaponType == 4){
			health -= 3;
		}
		if (weaponType == 2){
			health -= 6;
		}
		if (weaponType == 3){
			health -= 2;
		}
		
		// Birdman follows player if attacked
		isWalking = true;
		 
		// death
		if (health <= 0) {			 
			kill ();
		}  
	}

	//Birdman dies
	public void kill(){
		
		// animate
		isAlive = false;
		isWalking = false;
		myAnim.Play ("birdman-death");

		// keep in place
		myRigidBody.velocity = new Vector3 (0f, 0f, 0f);
		
		// allow player to walk through dead Birdman
		Destroy(GetComponent<Collider2D>());

		// add extra Birdman to spawner que
		spawner.deadBird();
	}

	// stop hurt animation
	void stopAction () {
		isActing = false;
	}

	// stagger Birdman if contacts Player
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			switchWalk ();	
		}
	}

}
