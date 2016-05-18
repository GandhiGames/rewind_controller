using UnityEngine;
using System.Collections;

namespace RewindController
{
	[RequireComponent(typeof(PlayerAnimation))]
	public class PlayerPhysics : Physics
	{

		private Player player;
		
		private PlayerAnimation playerAnimation;

		// Use this for initialization
		public override void Start ()
		{
			base.Start ();
	
			playerAnimation = GetComponent<PlayerAnimation> ();

			player = GetComponent<Player> ();			
		}

		public override Vector2 Move (Vector2 moveAmount)
		{

			Vector2 move = base.Move (moveAmount);
			
			
			if (move == Vector2.zero) {
				playerAnimation.DisableAnimation ();
				return move;
			}
			
			if (!playerAnimation) {
				return move;
			}
			
			if (player && player.OnLadder) {
				playerAnimation.Climb ();
				return move;
			} 
			
			if (IsGrounded && deltaX != 0) {
				playerAnimation.Walk ();
	
			} else {
				playerAnimation.Jump ();
			} 
				
			
			return move;
			
		}
		
	}
	

}
