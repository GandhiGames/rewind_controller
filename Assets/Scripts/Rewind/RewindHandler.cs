using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RewindController
{
	/// <summary>
	/// Abstract class provides bass functionality fot a concrete rewind class. Stores a list
	/// of commands (i.e. movement/attack). When player presses rewind button, the execute method
	/// is invoked each time step.
	/// </summary>
	public abstract class RewindHandler : MonoBehaviour
	{
		
		private bool rewindComplete = true;
		public bool Complete { get { return rewindComplete; } set { rewindComplete = value; } }
		protected List<CommandGroup> commands = new List<CommandGroup> ();
		
		public bool RewindRequired { get; set; }
		protected bool cancelRewind;
		
		protected int currentGroup = -1;

		protected bool canRewind = true;
		
		private static readonly string SCRIPT_NAME = typeof(RewindHandler).Name;
		
		public virtual void Start ()
		{
			IncreaseGroup ();
		}
		
		public void AddCommand (Command command, bool executeCommand)
		{
			if (!Complete) {
				Debug.LogError (SCRIPT_NAME + ": attempted to add new command while rewind executing. " +
					"All behaviour should be paused while rewinding.");
				return;
			}

			commands [currentGroup].Add (command);
			
			if (executeCommand) {
				command.Execute ();
			}
		}
		
		private void IncreaseGroup ()
		{
			currentGroup++;
			commands.Add (new CommandGroup ());
			
			if (commands.Count > RewindSettings.instance.MaxTimeStepsRecorded) {
				commands.RemoveAt (0);
				currentGroup = RewindSettings.instance.MaxTimeStepsRecorded - 1;
			}
		}
		
		void LateUpdate ()
		{
			if (!RewindRequired && Complete) {
				IncreaseGroup ();
			}
		}

		void Update ()
		{
			if (RewindRequired) {
				Complete = false;
				Execute ();
			} 
			
		
			if (RewindRequested ()) {
				RewindRequired = true;
			} 
		}
	
		protected bool RewindRequested ()
		{
			var shouldRewind = Input.GetButtonDown ("Rewind") && Complete && canRewind;
		
			if (shouldRewind) {
				RewindSceneController.instance.RewindStarted ();
			}
		
			return shouldRewind;
		}
		
		protected bool CancelRequested ()
		{
			var cancel = (Input.GetButtonUp ("Rewind") && canRewind) || cancelRewind;
			
			if (cancel) {
				RewindSceneController.instance.RewindStopped ();
			}
			
			return cancel;
		}
		
		public void CancelRewind ()
		{
			if (RewindRequired && !Complete) {
				cancelRewind = true;
			}
		}
	
		public void RequestRewind ()
		{
			RewindRequired = true;
		}
		
		public void PauseRewindAbility ()
		{
			canRewind = false;
		}

		protected abstract void Execute ();

			
	}
}
