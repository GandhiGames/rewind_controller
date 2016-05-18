using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RewindController
{
	[RequireComponent(typeof(AudioSource))]
	public class RewindSceneController : MonoBehaviour
	{
		public AudioClip RewindSound;

		private List<CharacterRewindHandler> rewindHandlers = new List<CharacterRewindHandler> ();
		
		private static RewindSceneController _instance;
		public static RewindSceneController instance {
			get {
				if (!_instance) {
					_instance = GameObject.FindObjectOfType<RewindSceneController> ();
				}
				
				return _instance;
			}
		}
		
		public void RewindStarted ()
		{
			if (!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play ();
		}
		
		public void RewindStopped ()
		{
			if (GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Stop ();
		}
		
	
		public void PauseRewindAbility ()
		{
			foreach (var rewind in rewindHandlers) {
				rewind.PauseRewindAbility ();
			}	
		}
		
		public void ExecuteInSeconds (float seconds)
		{
			Invoke ("Execute", seconds);
		}
		
		public void Execute ()
		{	
			RewindStarted ();
			foreach (var rewind in rewindHandlers) {
				rewind.RequestRewind ();
			}
		}
		
		public void CancelInSeconds (float seconds)
		{
			Invoke ("Cancel", seconds);
		}
		
		public void Cancel ()
		{		
			RewindStopped ();	
			foreach (var rewind in rewindHandlers) {
				rewind.CancelRewind ();
			}
		}
		
		public void RegisterRewindHandler (CharacterRewindHandler rewind)
		{
			rewindHandlers.Add (rewind);
		}
	
	}
}
