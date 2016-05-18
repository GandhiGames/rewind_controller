using UnityEngine;
using System.Collections;

namespace RewindController
{
	[RequireComponent(typeof(CharacterRewindHandler))]
	[RequireComponent(typeof(PlayerPhysics))]
	[RequireComponent(typeof(PlayerAnimation))]
	public class Player : MonoBehaviour, IKillable, IClimbable, IFlippable
	{
		public float gravity = 20;
		public float walkSpeed = 8;
		public float runSpeed = 12;
		public float acceleration = 30;
		public float jumpHeight = 12;
		public float RunningJumpHeight = 15f;
		public float ClimbSpeed = 5;
		
		public float currentSpeed;
		public float targetSpeed;
		private Vector2 amountToMove;
		
		private CharacterRewindHandler rewind;
		private PlayerPhysics physics;
		
		public bool FacingRight { get; set; }
		
		// Ladder.
		public bool OnLadder { get; set; }
		private bool touchedLadder = false;
		
		// Dead.
		private bool dead = false;
		public bool Dead { 
			get {
				return dead;
			} 
			set {
				dead = value;
				
				if (dead)
					onDeadRewindInvoked = false;
			} 
		}
		private bool onDeadRewindInvoked = false;
		
		// Animation
		private PlayerAnimation playerAnimation;
		
		private RewindSceneController rewindScene;
		
		
		// Use this for initialization
		void Start ()
		{
			FacingRight = true;
			
			rewind = GetComponent<CharacterRewindHandler> ();
			physics = GetComponent<PlayerPhysics> ();
			playerAnimation = GetComponent<PlayerAnimation> ();
			rewindScene = GameObject.FindObjectOfType<RewindSceneController> ();
		}
	
	

	
		// Update is called once per frame
		void Update ()
		{
			if (!rewind.Complete) {
				return;
			}
				
			if (Dead) {
			
				if (!onDeadRewindInvoked) {
					playerAnimation.Hurt ();
					
					rewindScene.PauseRewindAbility ();
					rewindScene.ExecuteInSeconds (0.4f);
					rewindScene.CancelInSeconds (2.5f);
					
					onDeadRewindInvoked = true;
				}
				
				CalculateDeadVelocity ();
				rewind.AddCommand (new PlayerDeadCommand (transform, amountToMove * Time.deltaTime, this), true);
				
				return;
			}
			
			if (BelowStage ()) {
				Dead = true;
				return;
			}
				
			if (OkToClimb ()) {
				HandleClimbing ();
			}  
			
			if (OkToJump ()) {
				CalculateJumpingVelocity ();
			}
			
			if (IsAlive ()) {
				CalculateWalkingVelocity ();
			}
			
			if (OkToApplyGravity ()) {
				CalculateGravity ();
			}
			
			
			Move ();
			
			HandleSpriteFlip ();
			
		}
		

		private bool BelowStage ()
		{
			return transform.position.y < 0f;
		}
		
		private void Move ()
		{
			amountToMove.x = currentSpeed;
				
			rewind.AddCommand (new MovementWithPhysicsCommand (transform, amountToMove * Time.deltaTime, physics), true);
		}
		
		private void CalculateGravity ()
		{
			amountToMove.y -= gravity * Time.deltaTime;
		}
		
		private void CalculateWalkingVelocity ()
		{
			float speed = (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
			
			targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
			currentSpeed = IncrementTowards (currentSpeed, targetSpeed, acceleration);
		}
		
		private void CalculateJumpingVelocity ()
		{
			amountToMove.y = 0;
			
			if (Input.GetButton ("Jump")) {
				amountToMove.y += (Input.GetButton ("Run")) ? RunningJumpHeight : jumpHeight;
			} 
		}
		
		private void CalculateDeadVelocity ()
		{
			amountToMove.y -= 1f;
		}
		
		private void HandleClimbing ()
		{
			if (!OnLadder) {				
				if (HasRequestedClimb ()) {
					rewind.AddCommand (new ClimbLadderCommand (this, true), true);
				}
			}
			
			if (OnLadder) {
				CalculateClimbingVelocity ();
			}
		}
		
		private void CalculateClimbingVelocity ()
		{
			amountToMove.y = 0;
				
			targetSpeed = Input.GetAxisRaw ("Vertical") * ClimbSpeed;
			amountToMove.y += targetSpeed;	
		}
		
		private bool HasRequestedClimb ()
		{
			return Input.GetAxisRaw ("Vertical") != 0;
		}
		
		private bool OkToJump ()
		{
			return physics.IsGrounded && !OnLadder && IsAlive ();
		}
		
		private bool IsAlive ()
		{
			return !Dead;
		}
		
		private bool OkToClimb ()
		{
			return touchedLadder && IsAlive ();
		}
		
		private bool OkToApplyGravity ()
		{
			return !OnLadder;
		}
		
		public void JumpedOnEnemy ()
		{
			amountToMove.y += 12;
		}
		
		private void HandleSpriteFlip ()
		{
			float moveDirection = Input.GetAxisRaw ("Horizontal");
			if (moveDirection < 0 && FacingRight) {
				rewind.AddCommand (new FlipCommand (transform, this), true);
			} else if (moveDirection > 0 && !FacingRight) {
				rewind.AddCommand (new FlipCommand (transform, this), true);
			}
		}
		
		private float IncrementTowards (float n, float target, float acc)
		{
			if (n == target) {
				return n;	
			} else {
				float dir = Mathf.Sign (target - n); // must n be increased or decreased to get closer to target
				n += acc * Time.deltaTime * dir;
				return (dir == Mathf.Sign (target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
			}
		}
		
		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.CompareTag ("Ladder")) {
				touchedLadder = true;
			} 
		}
		
		void OnTriggerExit2D (Collider2D other)
		{

			if (other.gameObject.CompareTag ("Ladder")) {
				touchedLadder = false;
				
				if (rewind.Complete && OnLadder)
					rewind.AddCommand (new ClimbLadderCommand (this, false), true);
			}
		}
	}
}
