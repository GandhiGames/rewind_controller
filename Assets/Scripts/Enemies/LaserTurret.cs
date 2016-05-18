using UnityEngine;
using System.Collections;

namespace RewindController
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(CharacterRewindHandler))]
	public class LaserTurret : MonoBehaviour
	{

		public GameObject LaserPrefab;
 
		public float ShootSpeed = 2.5f;

		public Vector2 ShootDirection = new Vector2 (-1, 0);
	
		public Sprite ShootSprite;
	
	
		private SpriteRenderer rend;
		private float currentShootSpeed = 0f;
		
		private CharacterRewindHandler rewind;
 
		// Use this for initialization
		void Start ()
		{
			if (!LaserPrefab) {
				Debug.LogError ("Please set laser prefab");
			} else {
				if (ShootSprite != null) {
					rend = GetComponent<SpriteRenderer> ();
				}
				
				rewind = GetComponent<CharacterRewindHandler> ();
			}
		}
		
		void Update ()
		{
		
			if (!rewind.Complete) {
	
				currentShootSpeed -= Time.deltaTime;
				if (currentShootSpeed < 0)
					currentShootSpeed = 0f;
				return;
			}
		
			currentShootSpeed += Time.deltaTime;
			
			if (currentShootSpeed > ShootSpeed) {
				currentShootSpeed = 0f;
				rewind.AddCommand (new ShootLaserCommand (transform, ShootDirection, LaserPrefab, rend, ShootSprite), true);
				
			} else {
				rewind.AddCommand (new IdleCommand (), false);
			}
		}
	

	
		
	}
}
