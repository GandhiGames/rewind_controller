using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class Physics : MonoBehaviour
	{

		public LayerMask CollisionMask;
		public Vector2 CollisionRays = new Vector2 (3, 6);
	
		public bool IsGrounded { get; set; }
	
		public bool MovementStopped { get; set; }
	
		protected float deltaX;
	
		private BoxCollider2D boxCollider2D;
		private Vector3 s;
		private Vector3 c;
	
		private Vector3 originalSize;
		private Vector3 originalCentre;
	
		private float colliderScale;
	
		//space between ground and player - helps with raycasting
		private float skin = 0.05f;
		
	
		private Ray2D ray;
		private RaycastHit2D hit;
	
		// Use this for initialization
		public virtual void Start ()
		{
			boxCollider2D = GetComponent<BoxCollider2D> ();
			colliderScale = transform.localScale.x;
			
			s = boxCollider2D.size * colliderScale;
			c = boxCollider2D.offset * colliderScale;
		}
	
		public virtual Vector2 Move (Vector2 moveAmount)
		{
		
			float deltaY = moveAmount.y;
			deltaX = moveAmount.x;
		
			Vector2 p = transform.position;
		
			IsGrounded = false;
			MovementStopped = false;
		
			//collisions above and below
			for (int i = 0; i<CollisionRays.x; i ++) {
				float dir = Mathf.Sign (deltaY);
				float x = (p.x + c.x - s.x / 2) + s.x / (CollisionRays.x - 1) * i; // Left, centre and then rightmost point of collider
				float y = p.y + c.y + s.y / 2 * dir; // Bottom of collider
			
				ray = new Ray2D (new Vector2 (x, y), new Vector2 (0, dir));
				Debug.DrawRay (ray.origin, ray.direction);
			
				var hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Abs (deltaY) + skin, CollisionMask);
			
				if (hit.collider != null) {
					// Get Distance between player and ground
					float dst = Vector3.Distance (ray.origin, hit.point);
				
					// Stop player's downwards movement after coming within skin width of a collider
					if (dst > skin) {
						deltaY = dst * dir - skin * dir;
					} else {
						deltaY = 0;
					}
				
					if (dir <= 0)
						IsGrounded = true;
				
					break;
				
				}
			}
		
		
			//left and right
			for (int i = 0; i<CollisionRays.y; i ++) {
				float dir = Mathf.Sign (deltaX);
				float x = p.x + c.x + s.x / 2 * dir; // Left, centre and then rightmost point of collider
				float y = p.y + c.y - s.y / 2 + s.y / (CollisionRays.y - 1) * i; // Bottom of collider
			
				ray = new Ray2D (new Vector2 (x, y), new Vector2 (dir, 0));
				Debug.DrawRay (ray.origin, ray.direction);
			
				var hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Abs (deltaX) + skin, CollisionMask);
			
				if (hit.collider != null) {
					// Get Distance between player and ground
					float dst = Vector3.Distance (ray.origin, hit.point);
				
					// Stop player's downwards movement after coming within skin width of a collider
					if (dst > skin) {
						deltaX = dst * dir - skin * dir;
					} else {
						deltaX = 0;
					}
				
					// Can jump off side of obstacle.
					//IsGrounded = true;
					MovementStopped = true;
				
					break;
				
				}
			}
		
			if (!IsGrounded && !MovementStopped) {
				Vector3 playerDir = new Vector3 (deltaX, deltaY);
				Vector3 o = new Vector3 (p.x + c.x + s.x / 2 * Mathf.Sign (deltaX), p.y + c.y + s.y / 2 * Mathf.Sign (deltaY));
				ray = new Ray2D (o, playerDir.normalized);
				Debug.DrawRay (ray.origin, ray.direction);
			
				var hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Sqrt (deltaX * deltaX + deltaY * deltaY), CollisionMask);
			
				if (hit.collider != null) {
					IsGrounded = true;
					deltaY = 0;
				}
			}
		
		
			Vector2 move = new Vector2 (deltaX, deltaY);
		
			transform.Translate (move, Space.World);
		
			return move;
		}
	}
}
