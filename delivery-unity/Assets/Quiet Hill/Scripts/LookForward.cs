using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForward : MonoBehaviour {
	public Transform sightStart, sightEnd, sightEndKnife;
	private bool collision = false;
	//private bool collisionPlayer = false;

	public RaycastHit2D hit;

	public BirdmanController myBirdman;
	public PlayerController myPlayer;

	public bool isColliding = false;
	public bool isCollidingEnemy = false;



	// Use this for initialization
	void Start () {
		myBirdman = FindObjectOfType<BirdmanController> ();	
		myPlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {


		if(gameObject.tag == "Birdman"){
			
			collision = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Player"));

			if (collision && myPlayer.isAlive) {
				isColliding = true;
				myBirdman.engage ();



			} else {
				isColliding = false;
			}


		}else if(gameObject.tag == "Player"){
			if (myPlayer.weaponType == 4){
				hit = Physics2D.Linecast (sightStart.position, sightEndKnife.position, 1 << LayerMask.NameToLayer ("Birdman"));
			}
			else{
			hit = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Birdman"));
			}
			if (hit.collider != null) {
				isCollidingEnemy = true;
				myBirdman = hit.transform.gameObject.GetComponent<BirdmanController>();
			
					/*if (myPlayer.transform.position.x > myBirdman.transform.position.x) {
						myPlayer.GetComponent<Rigidbody2D>().AddForce(transform.right * 20, ForceMode.Impulse);
						//myPlayer.Rigidbody2D.AddForce(transform.right * 20f, ForceMode.Impulse);
					}
					else {
						transform.localScale = new Vector3 (1f,1f,1f);
					}*/

				//Debug.Log ( isCollidingEnemy );
			} else {
				 isCollidingEnemy = false;
			}
		}






	}

	private void OnDrawGizmos () {

		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);
	}

	public void hitbox(){

		if (isColliding) {
			//Debug.Log ("Hurt Player");
			myPlayer.hurtPlayer ();
		}

	}

	public void hitboxPlayer(){
		
		if (isCollidingEnemy) {
			//Debug.Log ("Hurt Enemy");
		 	myBirdman.hurtBirdman ();
		}

	}


}
