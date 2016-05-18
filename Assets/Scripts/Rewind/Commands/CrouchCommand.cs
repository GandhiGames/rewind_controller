using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class CrouchCommand : Command
	{
		private bool crouch;
		private Animator animator;

		public CrouchCommand (bool crouch, Animator animator) : base ()
		{
			this.crouch = crouch;
			this.animator = animator;
		}
		
		public override void Execute ()
		{
			animator.SetBool ("duck", crouch);
		}
		
		public override void Undo ()
		{
			animator.SetBool ("duck", !crouch);
		}
	}
}
