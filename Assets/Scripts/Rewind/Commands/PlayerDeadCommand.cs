using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class PlayerDeadCommand : Command
	{
		private IKillable killable;
		
		private Vector2? undoDir;
	
		public PlayerDeadCommand (Transform character, Vector3 dir, IKillable killable) : base (character, dir)
		{
			this.killable = killable;
		}
	
		public override void Execute ()
		{
			undoDir = -dir;
			killable.Dead = true;
			character.Translate (dir, Space.World);
		}
	
		public override void Undo ()
		{
			if (undoDir.HasValue) {
				killable.Dead = false;
				character.Translate (undoDir.Value, Space.World);
			}
		}
	}
}
