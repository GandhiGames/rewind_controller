using UnityEngine;
using System.Collections;

namespace RewindController
{
	/// <summary>
	/// Concrete class to handle the ability to 'rewind' a characters action. Attach this to any character to provide
	/// the ability to rewind their actions.
	/// </summary>
	public class CharacterRewindHandler : RewindHandler
	{
	
		private GameObject rewindImage;
		
		public override void Start ()
		{
			base.Start ();
			
			rewindImage = GameObject.FindGameObjectWithTag ("RewindArrow");
			if (rewindImage)
				rewindImage.SetActive (false);
			
			RegisterHandler ();
		}
		
		private void RegisterHandler ()
		{
			var controller = RewindSceneController.instance;
			
			if (!controller) {
				Debug.LogError ("Scene should have 'RewindSceneController' script");
				return;
			}
			
			controller.RegisterRewindHandler (this);
			
		}
		
		/// <summary>
		/// Checks if it is ok to undo and calls the most recent commands undo method. 
		/// The command list is traversed from most recent to first.
		/// When current command is less than 0 the complete flag is set to true.
		/// This will stop the rewind process and enable the character to run new commands.
		/// </summary>
		protected override void Execute ()
		{

			if (rewindImage)
				rewindImage.SetActive (true);
			
			if (CancelRequested ()) {
				Reset ();
				
				if (rewindImage)
					rewindImage.SetActive (false);
				return;
			}
		
			foreach (var command in commands[currentGroup].Reverse ()) {
				command.Undo ();
			}
				
			commands.RemoveAt (currentGroup);
			currentGroup--;
				
			if (currentGroup < 0) {
				Reset ();
				return;
			}
		}
		
		private void Reset ()
		{
			ResetFlags ();
			
			if (currentGroup < 0) {
				currentGroup = 0;
				commands.Add (new CommandGroup ());
			}
		}
		
		private void ResetFlags ()
		{
			Complete = true;
			cancelRewind = false;
			RewindRequired = false;
			canRewind = true;
		}
		
			
	}
}
