using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]

public class Controller2D : MonoBehaviour {


	public LayerMask collisionMask;
	const float skinWidth = 0.015f;
	public int horizontalraycount = 4;
	public int verticalraycount = 4;

	float horizontalrayspacing;
	float verticalrayspacing;

	BoxCollider2D collider;
	RaycastOrigins raycastOrigins;
	public CollisionsInfo collisions;

	void Start () {
	

		collider = GetComponent<BoxCollider2D> ();
		CalculateRaySpacing ();

	}



	void HorizontalCollisions(ref Vector3 velocity){

		float directionX = Mathf.Sign (velocity.x); 
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;

		for(int i=0; i<horizontalraycount; i++ )
		{
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomleft : raycastOrigins.bottomright;
			rayOrigin += Vector2.up * (horizontalrayspacing * i );
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX,rayLength, collisionMask);
			Debug.DrawRay(rayOrigin,Vector2.right * directionX * rayLength,Color.red);

			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}

		}

	}
	void VerticalCollisions(ref Vector3 velocity){

		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

		for(int i=0; i<verticalraycount; i++ )
		{
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomleft : raycastOrigins.topleft;
			rayOrigin += Vector2.right * (verticalrayspacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY,rayLength, collisionMask);

			Debug.DrawRay(rayOrigin , Vector2.up * directionY * rayLength,Color.red);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}

		}

	}

	void UpdateRaycastOrigins(){
	
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		raycastOrigins.bottomleft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomright = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topleft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topright = new Vector2 (bounds.max.x, bounds.max.y);
	}


	void CalculateRaySpacing()
	{
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalraycount = Mathf.Clamp (horizontalraycount,2,int.MaxValue);
		verticalraycount = Mathf.Clamp (verticalraycount,2,int.MaxValue);

		horizontalrayspacing = bounds.size.y / (horizontalraycount-1);
		verticalrayspacing = bounds.size.y / (verticalraycount-1);
	}


	public void Move(Vector3 velocity){
		UpdateRaycastOrigins ();
		collisions.Reset ();
		if (velocity.x != 0) {

			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}
			transform.Translate (velocity);
		}
	

	struct RaycastOrigins{
		public Vector2 topleft,topright;
		public Vector2 bottomleft,bottomright;
	}

	public struct CollisionsInfo{
		public bool above,below;
		public bool left,right;

		public void Reset(){
		above = below = false;
		left = right = false;
	}
	}
}
