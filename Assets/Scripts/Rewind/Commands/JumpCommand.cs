using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class JumpCommand : Command
	{
		private Rigidbody2D rigidBody;
		
		public JumpCommand (Transform character, Rigidbody2D rigidBody) : base (character)
		{
			this.rigidBody = rigidBody;
		}
		
		public override void Execute ()
		{
			
			rigidBody.AddForce (new Vector2 (0f, 400));
		}
		
		public override void Undo ()
		{
			rigidBody.AddForce (new Vector2 (0f, 400));
		}
	}
}
