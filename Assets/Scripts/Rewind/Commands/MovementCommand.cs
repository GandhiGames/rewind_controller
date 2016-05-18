using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class MovementCommand : Command
	{
		private Vector2? undoDir;

		public MovementCommand (Transform character, Vector2 dir) : base (character, dir)
		{
		}
		
		public override void Execute ()
		{
			if (dir == Vector2.zero)
				return;
			
			undoDir = -dir;
			
			character.Translate (dir, Space.World);
		}

		public override void Undo ()
		{
			if (undoDir.HasValue)
				character.Translate (undoDir.Value, Space.World);
		}
	}
}
