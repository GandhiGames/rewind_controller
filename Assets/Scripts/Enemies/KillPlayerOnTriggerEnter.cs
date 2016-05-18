using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class KillPlayerOnTriggerEnter : MonoBehaviour
	{

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.CompareTag ("Player")) {
				other.gameObject.GetComponent<Player> ().Dead = true;
			}
		}
	}
}
