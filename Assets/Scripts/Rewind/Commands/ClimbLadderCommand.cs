using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class ClimbLadderCommand : Command
	{

		private IClimbable climb;
		private bool climbing;

		public ClimbLadderCommand (IClimbable climb, bool climbing) : base ()
		{
			this.climb = climb;
			this.climbing = climbing;
		}
		
		public override void Execute ()
		{
			climb.OnLadder = climbing;
		}
		
		public override void Undo ()
		{
			climb.OnLadder = !climbing;
		}
	}
}
