using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Controller2D))]

public class PlayerController : MonoBehaviour {



	public	float moveSpeed;
	public	float moveSpeedStore;
	public float speedMultiplier;

	public float speedIncreaseMilestone;
	public float speedIncreaseMilestoneStore;

	private float speedMilestoneCount;
	private float speedMilestoneCountStore;

	public float jumpForce;
	public float jumpTime;
	public float jumpTimeCounter;

	private bool stoppedJumping;

	Vector3 velocity;
	public	float jumpvelocity = 8;

	Controller2D controller;

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;
	public	float acctimeAirborne = .4f;
	public	float acctimeGrounded = .2f;
	public	float gravity = -10;
	float velocitysmoothing;
	public float jumpHeight = 5;
	public float TimeToJumpApex = .4f;



	public GameManager theGameManager;

	void Start () {

		controller = GetComponent<Controller2D> ();
		gravity = -(2 * jumpHeight) / Mathf.Pow (TimeToJumpApex, 2);
		jumpvelocity = Mathf.Abs (gravity) * TimeToJumpApex;
		print("Gravity: " + gravity + " Jump velocity: " + jumpvelocity);


		jumpTimeCounter = jumpTime;

		speedMilestoneCount = speedIncreaseMilestone;

		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;
		stoppedJumping = true;
		
	}
	

	void Update () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius,whatIsGround);

		if (transform.position.x > speedMilestoneCount) {
		
			speedMilestoneCount += speedIncreaseMilestone;
			speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier;
		
		}

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;

		}

		//myRigidBody.velocity = new Vector2 (moveSpeed, myRigidBody.velocity.y);

		if (Input.GetMouseButtonDown (0)) {
			if (grounded) {
				//myRigidBody.velocity = new Vector2 (myRigidBody.velocity.x, jumpForce);
				stoppedJumping = false;
			}
		
		}


		if (Input.GetMouseButtonDown (0) && !stoppedJumping) {
			if (jumpTimeCounter > 0) {
			
				//myRigidBody.velocity = new Vector2 (myRigidBody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		
		}

		if (Input.GetMouseButtonDown (0)) {
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
		} 
	


		if (Input.GetMouseButton(0) ) {


			if (controller.collisions.below) {



				velocity.y = jumpvelocity;
			}

		}

		if (transform.position.x > speedMilestoneCount) {

			speedMilestoneCount += speedIncreaseMilestone;
			speedIncreaseMilestone = speedMilestoneCount * speedMultiplier;


			moveSpeed = moveSpeed * speedMultiplier;

		}


		float targetvelocityX = 1 * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetvelocityX,ref velocitysmoothing, (controller.collisions.below)? acctimeGrounded:acctimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity*Time.deltaTime);

	}


	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "killbox") {

			theGameManager.RestartGame();

			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;

			speedIncreaseMilestone = speedIncreaseMilestoneStore;
		}

	}
}
