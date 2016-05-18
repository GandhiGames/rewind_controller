using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class SlimeDeadCommand : EnemyDeadCommand
	{

		public SlimeDeadCommand (Transform character, IKillable killable, Animator animator) : base (character, killable, animator)
		{
		}
		
		public override void Execute ()
		{
			base.Execute ();
			character.position = new Vector2 (character.position.x, character.position.y - 0.4f);
		}
		
		public override void Undo ()
		{
			base.Undo ();
			character.position = new Vector2 (character.position.x, character.position.y + 0.4f);
		}
	}
}
