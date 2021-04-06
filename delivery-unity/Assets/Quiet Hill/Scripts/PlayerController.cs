using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	
	public float moveSpeed;
	private Rigidbody2D myRigidbody;
	private Animator myAnim;
	public bool isAttacking;
	public bool isActing;


	public int weaponType;
	public bool isOverItem;
	public GameObject item;

	public GameObject blood;

	private LookForward myLookForward;

	public int health = 3;
	public bool isAlive = true;
	public Vector3 respawnPosition;

 

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();	
		myAnim = GetComponent<Animator> ();
		weaponType = 0; // 0: none, 1: bat, 2: hammer, 3: dagger, 4: gun 
		isOverItem = false;
		isActing = false;
		myLookForward = GetComponent<LookForward> ();
		respawnPosition = transform.position;


	}
	
	// Update is called once per frame
	void Update () {

		//animation
		setAnimVars ();

		//action
		controlPlayer ();

	}

	// set animation variables
	void setAnimVars () {
		
		// detect and assign movement
		float vel = 0f;
		float xVel = Mathf.Abs (myRigidbody.velocity.x);
		float yVel = Mathf.Abs (myRigidbody.velocity.y);
		
		if (xVel > 0f) {
			vel = xVel;
		}
		if (yVel > 0f) {
			vel = yVel;
		}

		// animations variables
		myAnim.SetFloat ("speed", vel);
		myAnim.SetBool ("isAttacking", isAttacking);
		myAnim.SetInteger ("weaponType", weaponType);
		myAnim.SetBool ("isActing", isActing);
	}

	// isolate Player actions
	void controlPlayer () {
		
		// check Player isn't attacking or picking up before moving
		if (!isAttacking && !isActing) {
			movePlayer ();
		}

		// attack or pickup
		playerActions ();
		
	}

	void movePlayer () {

		// grab speed
		float speed = moveSpeed;

		// running is disabled
		if (Input.GetButtonDown ("Jump")){
			speed = 0;
		}

		// not running
		else {
			speed = moveSpeed;
		}

			// horizontal
			if (Input.GetAxisRaw ("Horizontal") > 0f) {
				myRigidbody.velocity = new Vector3 (speed, myRigidbody.velocity.y, 0f);	
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else if (Input.GetAxisRaw ("Horizontal") < 0f) {
				myRigidbody.velocity = new Vector3 (-speed, myRigidbody.velocity.y, 0f);
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			} else {
				myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, 0f);
			}

			//vertical
			if (Input.GetAxisRaw ("Vertical") > 0f) {
				myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, speed / 2, 0f);
			} else if (Input.GetAxisRaw ("Vertical") < 0f) {
				myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, -speed / 2, 0f);
			} else {
				myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, 0f, 0f);
			}

	
	}

	// pickup or attack
	void playerActions () {

		// on button push
		if (Input.GetButtonDown ("Jump")) {
		
		// check if over item
			if (isOverItem) {
				switch (item.tag) 
				{
				case "Bat":
					weaponType = 1;
					break;
				case "Hammer":
					weaponType = 2;
					break; 
				case "Dagger":
					weaponType = 3;
					break; 
				case "Gun":
					weaponType = 4;
					break; 
				
				case "Crate":

					// picup crate to win
					quitGame();
					break; 
				}

				// animate
				isActing = true;

				// remove item
				Destroy(item.gameObject);
			
			// attack
			} else if (weaponType != 0) {
				isAttacking = true;	
			}
		}
	}

	// item detection
	void OnTriggerEnter2D (Collider2D other) {
		
		if (other.tag != "Birdman") {
			isOverItem = true;
			item = other.gameObject;
		}
	}		

	// stop detecting item
	void OnTriggerExit2D (Collider2D other) {
		isOverItem = false;
	}

	// stop pickup
	void stopAction () {
		isActing = false;
		Destroy (item);
	}

	// Player hit by Birdman
	public void hurtPlayer () {
		
		// check if dead
		if(!isAlive){
			return;	
		}
		
		// create blood
		Instantiate (blood, gameObject.transform.position, gameObject.transform.rotation);	
		
		// animate
		isAttacking = false;
		isActing = true;
		myAnim.Play ("player-hurt");
		
		// decrease health
		health --;

		// dies
		if (health <= 0) {
			kill ();	
		}
		
	}

	// Player dies
	private void kill(){
		// animate
		isAlive = false;
		myAnim.Play ("player-death");
		
		//keep Player in place
		myRigidbody.velocity = new Vector3 (0f, 0f, 0f);	
		
		//quit
		quitGame();
	}

	// check to see if hit by birdman
	public void checkHurt(){
		myLookForward.hitboxPlayer ();
	}

	// stop attacking
	void stopAttack () {
		isAttacking = false;
	}

	// quit game
	public void quitGame(){
		#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
     	#else
         Application.Quit();
     	#endif
	}
}
