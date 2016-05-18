using UnityEngine;
using System.Collections;

namespace RewindController
{
	/// <summary>
	/// Movement command.
	/// </summary>
	public class MovementWithPhysicsCommand : Command
	{
		private Vector2? undoDir;
		private Physics physics;
		private Vector2 instanceDir;
		public Vector2 Direction {
			get {
				return instanceDir;
			}
		}

		public MovementWithPhysicsCommand (Transform character, Vector3 dir, Physics physics) : base (character, dir)
		{
			this.physics = physics;

			instanceDir = dir;
		}

		public override void Execute ()
		{
		
			if (instanceDir == Vector2.zero)
				return;
			
			/*Debug.Log (instanceDir);
			if (instanceDir.x == 0) {
				UpdateAnimation ("walk", false);
				
				if (instanceDir.y == 0) {
					UpdateAnimation ("jump", false);
					return;
				}
			} */
		
			undoDir = -physics.Move (instanceDir);

			/*if (instanceDir.y > 0)
				UpdateAnimation ("jump", true);
			else if (instanceDir.x != 0)
				UpdateAnimation ("walk", true); */
			
		}

		public override void Undo ()
		{
			if (undoDir.HasValue) {
				character.Translate (undoDir.Value, Space.World);
			}
		}
		
		public void MultiplyByDeltaTime (float deltaTime)
		{
			instanceDir = dir;
			instanceDir *= deltaTime;
		}
		
		/*private void UpdateAnimation (string name, bool active)
		{
			if (animator) {
				animator.SetBool (name, active);
			}
		} */

		

	}
}
