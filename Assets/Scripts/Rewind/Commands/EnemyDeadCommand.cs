using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class EnemyDeadCommand : Command
	{

		private IKillable killable;
		private Animator animator;
		
		public EnemyDeadCommand (Transform character, IKillable killable, Animator animator) : base (character)
		{
			this.killable = killable;
			this.animator = animator;
		}
		
		public override void Execute ()
		{
			animator.SetBool ("dead", true);
			killable.Dead = true;
		}
		
		public override void Undo ()
		{
			animator.SetBool ("dead", false);
			killable.Dead = false;
		}
	}
}
