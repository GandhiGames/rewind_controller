using UnityEngine;
using System.Collections;

namespace RewindController
{
	/// <summary>
	/// Perform no command.
	/// </summary>
	public class IdleCommand : Command
	{			
		public IdleCommand () : base ()
		{
		}
		
		public override void Execute ()
		{

		}

		public override void Undo ()
		{

		}
	}
}
