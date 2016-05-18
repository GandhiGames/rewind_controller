using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class FollowPlayer : MonoBehaviour
	{

		public Transform Player;
		// Use this for initialization
		void Start ()
		{
			if (!Player) {
				Debug.LogError ("Please set player transform");
			}
		}
	
		// Update is called once per frame
		void LateUpdate ()
		{
			transform.position = new Vector3 (Player.position.x, Player.position.y, transform.position.z);
		}
	}
}
