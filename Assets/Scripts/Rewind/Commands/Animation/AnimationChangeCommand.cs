using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class AnimationChangeCommand : Command
	{

		private Animator animator;
		private string parameterName;
		private bool state;

		public AnimationChangeCommand (Animator animator, string parameterName, bool state) : base ()
		{
			this.animator = animator;
			this.parameterName = parameterName;
			this.state = state;
		}
		
		public override void Execute ()
		{
			animator.SetBool (parameterName, state);
		}
		
		public override void Undo ()
		{
			animator.SetBool (parameterName, !state);
		}
	}
}
