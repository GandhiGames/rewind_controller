using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class FlipCommand : Command
	{
		private IFlippable flip;

		public FlipCommand (Transform character, IFlippable flip) : base (character)
		{
			this.flip = flip;
		}
		
		public override void Execute ()
		{
			Flip ();
	
		}
		
		public override void Undo ()
		{
			Flip ();
		}
		
		private void Flip ()
		{
			Vector3 theScale = character.localScale;
			theScale.x *= -1;
			character.localScale = theScale;
			flip.FacingRight = !flip.FacingRight;
		}
	}
}
