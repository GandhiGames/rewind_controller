using UnityEngine;
using System.Collections;

namespace RewindController
{
	[RequireComponent(typeof(CharacterRewindHandler))]
	public class Laser : MonoBehaviour
	{

		public float MovementSpeed = 20f;
		
		public Vector2 Direction { get; set; }
		
		private CharacterRewindHandler rewind;


		// Use this for initialization
		void Start ()
		{
			rewind = GetComponent<CharacterRewindHandler> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (!rewind.Complete)
				return;
				
			var force = Direction * MovementSpeed;
				
			rewind.AddCommand (new MovementCommand (transform, force * Time.deltaTime), true);
		}
		
		void OnTriggerEnter2D (Collider2D other)
		{
			if (!rewind.Complete)
				return;
			
			if (other.gameObject.CompareTag ("Player")) {
				
				var player = other.gameObject.GetComponent<Player> ();
				
				player.Dead = true;
				
			}
		}
	}
}
