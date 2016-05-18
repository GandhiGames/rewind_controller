using UnityEngine;
using System.Collections;

namespace RewindController
{
	[RequireComponent(typeof(CharacterRewindHandler))]
	[RequireComponent(typeof(Physics))]
	public class Slime : MonoBehaviour, IKillable, IFlippable
	{
		public float MoveSpeed = 2f;
		public int MovesInEachDirection = 200;
		
		public bool Dead { get; set; }
	
		private CharacterRewindHandler rewind;
		private Physics physics;
		
		public bool FacingRight { get; set; }
		
		private int currentStep = 0;
		
		private Animator animator;
		
		private MovementWithPhysicsCommand[] movementCommands;
		
		// Use this for initialization
		void Start ()
		{
			rewind = GetComponent<CharacterRewindHandler> ();
			physics = GetComponent<Physics> ();
			animator = GetComponent<Animator> ();
			
			movementCommands = new MovementWithPhysicsCommand[MovesInEachDirection * 2];
			
			for (int i = 0; i < movementCommands.Length / 2; i++) {
				movementCommands [i] = new MovementWithPhysicsCommand (transform, new Vector2 (1, -1) * MoveSpeed, physics);
			}
			
			for (int i = movementCommands.Length / 2; i < movementCommands.Length; i++) {
				movementCommands [i] = new MovementWithPhysicsCommand (transform, new Vector2 (-1, -1) * MoveSpeed, physics);
			}
			
		}
	
		// Update is called once per frame
		void Update ()
		{
		
			if (!rewind.Complete) {
				currentStep--;
				if (currentStep < 0)
					currentStep = movementCommands.Length - 1;
				return;
			}
			
			if (Dead) {
				rewind.AddCommand (new IdleCommand (), false);
				return;
			}
		
			var command = movementCommands [currentStep];
			command.MultiplyByDeltaTime (Time.deltaTime);
			
			rewind.AddCommand (command, true);
			currentStep = (currentStep + 1) % movementCommands.Length;

			HandleSpriteFlip (command.Direction.x);
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
					rewind.AddCommand (new SlimeDeadCommand (transform, this, animator), true);
					player.JumpedOnEnemy ();
				} else {
					player.Dead = true;
				}
			}
		}
	}
}
