using UnityEngine;
using System.Collections;

namespace RewindController
{
	/// <summary>
	/// Base abstract command. All commands (e.g. movement, attack) should inherit form this class.
	/// </summary>
	public abstract class Command
	{
		protected Transform character;
		protected Vector2 dir;

        public Command (Transform character = null, Vector3 dir = default(Vector3))
		{
			this.character = character;
			this.dir = dir;
		}

        /// <summary>
        /// Perform the action.
        /// </summary>
		public abstract void Execute ();
				
        /// <summary>
        /// Perform the reverse action.
        /// </summary>
		public abstract void Undo ();
	}
}
