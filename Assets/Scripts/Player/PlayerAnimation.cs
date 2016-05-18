using UnityEngine;
using System.Collections;

namespace RewindController
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CharacterRewindHandler))]
	public class PlayerAnimation : MonoBehaviour
	{
		private Animator animator;
		private CharacterRewindHandler rewind;
		
		private Command disableWalkCommand;
		private Command disableJumpCommand;
		private Command disableClimbCommand;
		private Command disableHurtCommand;
		
		
		private Command enableWalkCommand;
		private Command enableJumpCommand;
		private Command enableClimbCommand;
		private Command enableHurtCommand;

		// Use this for initialization
		void Start ()
		{
			animator = GetComponent<Animator> ();
			rewind = GetComponent<CharacterRewindHandler> ();
			InitAnimationCommands ();
		}
		

		
		private void InitAnimationCommands ()
		{
			disableWalkCommand = new AnimationChangeCommand (animator, "walk", false);
			disableJumpCommand = new AnimationChangeCommand (animator, "jump", false);
			disableClimbCommand = new AnimationChangeCommand (animator, "climb", false);
			disableHurtCommand = new AnimationChangeCommand (animator, "dead", false);
			
			enableWalkCommand = new AnimationChangeCommand (animator, "walk", true);
			enableJumpCommand = new AnimationChangeCommand (animator, "jump", true);
			enableClimbCommand = new AnimationChangeCommand (animator, "climb", true);
			enableHurtCommand = new AnimationChangeCommand (animator, "dead", true);
			
		}
	
		public void Jump ()
		{
			DisableAnimation ();
			rewind.AddCommand (enableJumpCommand, true);
		}
		
		public void Walk ()
		{
			DisableAnimation ();
			rewind.AddCommand (enableWalkCommand, true);
		}
		
		public void Climb ()
		{
			DisableAnimation ();
			rewind.AddCommand (enableClimbCommand, true);
		}
		
		public void Hurt ()
		{
			DisableAnimation ();
			rewind.AddCommand (enableHurtCommand, true);
		}
		
		public void DisableAnimation ()
		{
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("walk"))
				rewind.AddCommand (disableWalkCommand, true);
				
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("jump"))
				rewind.AddCommand (disableJumpCommand, true);
				
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("climb"))
				rewind.AddCommand (disableClimbCommand, true);

			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("hurt"))
				rewind.AddCommand (disableHurtCommand, true);			
		}
	}
}
