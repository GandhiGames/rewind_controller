using UnityEngine;
using System.Collections;

public class RewindSettings : MonoBehaviour
{


	public int MaxTimeStepsRecorded = 400;

	private static RewindSettings _instance;
	public static RewindSettings instance {
		get {
			if (!_instance) {
				_instance = FindObjectOfType<RewindSettings> ();
			}
			
			return _instance;
		}
	}


}
