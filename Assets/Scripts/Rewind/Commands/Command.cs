using UnityEngine;
using System.Collections;

namespace RewindController
{
	/// <summary>
	/// Base abstract command. All commands (e.g. movement, attack) should inherit form this class.
	/// By recording a characters commands they can be played in reverse (i.e. rewound).
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
		public abstract void Execute ();
				
		// Used by the RewindHandler with rewinding.
		public abstract void Undo ();
	}
}
