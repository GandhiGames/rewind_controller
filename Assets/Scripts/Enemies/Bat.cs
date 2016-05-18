using UnityEngine;
using System.Collections;

namespace RewindController
{
	[RequireComponent(typeof(CharacterRewindHandler))]
	[RequireComponent(typeof(Animator))]
	public class Bat : MonoBehaviour, IFlippable, IKillable
	{
		public Transform Player;
		
		public bool FacingRight { get; set; }
		public bool Dead { get; set; }
	
		private float SightRadius = 10f;
		private float MovementSpeed = 5f;
		private bool firstSightOfPlayer = true;
		
		private CharacterRewindHandler rewind;
		
		private Animator animator;

		// Use this for initialization
		void Start ()
		{
			rewind = GetComponent<CharacterRewindHandler> ();
			animator = GetComponent<Animator> ();
			
		}
	
		// Update is called once per frame
		void Update ()
		{
		
			if (!rewind.Complete) {
				firstSightOfPlayer = true;
				return;
			}
			
			if (Dead) {
				var gravity = new Vector2 (0, -1) * 16f;
				rewind.AddCommand (new MovementCommand (transform, gravity * Time.deltaTime), true);
			}
		
			var heading = Player.position - transform.position;
			var distance = heading.magnitude;
			var dir = heading / distance;
		
			if (distance < SightRadius) {
			
				if (firstSightOfPlayer) {
					rewind.AddCommand (new AnimationChangeCommand (animator, "playerInSight", true), true);
					firstSightOfPlayer = false;
				}
			
				var move = dir * MovementSpeed;
				
				rewind.AddCommand (new MovementCommand (transform, move * Time.deltaTime), true);
				HandleSpriteFlip (move.x);
			} else {
				rewind.AddCommand (new IdleCommand (), false);
			}
		}
		
		private void HandleSpriteFlip (float moveDirection)
		{
			
			if (moveDirection < 0 && FacingRight) {
				rewind.AddCommand (new FlipCommand (transform, this), true);
			} else if (moveDirection > 0 && !FacingRight) {
				rewind.AddCommand (new FlipCommand (transform, this), true);
			}
		}
		
		void OnTriggerEnter2D (Collider2D other)
		{
			if (!rewind.Complete || Dead)
				return;
			
			if (other.gameObject.CompareTag ("Player")) {
			
				var player = other.gameObject.GetComponent<Player> ();
			
				if (other.bounds.min.y >= transform.position.y) {
					rewind.AddCommand (new EnemyDeadCommand (transform, this, animator), true);
					player.JumpedOnEnemy ();
				} else {
					player.Dead = true;
				}
			}
		}
	}
}
