using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

	float moveSpeed = 6;
	float gravity = -20;
	Vector3 velocity;
	float jumpvelocity = 8;

	public float jumpHeight = 4;
	public float TimeToJumpApex = .4f;
	float acctimeAirborne = .2f;
	float acctimeGrounded = .1f;
	float velocitysmoothing;

	Controller2D controller;

	void Start () {
		controller = GetComponent<Controller2D> ();
		gravity = -(2 * jumpHeight) / Mathf.Pow (TimeToJumpApex, 2);
		jumpvelocity = Mathf.Abs (gravity) * TimeToJumpApex;
		print("Gravity: " + gravity + " Jump velocity: " + jumpvelocity);

	}
	

	void Update () {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		
		}


		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below) {
			velocity.y = jumpvelocity;
		
		}


		float targetvelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetvelocityX,ref velocitysmoothing, (controller.collisions.below)? acctimeGrounded:acctimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity*Time.deltaTime);
	
	}
}
