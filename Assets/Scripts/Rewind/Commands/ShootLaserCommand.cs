using UnityEngine;
using System.Collections;

namespace RewindController
{
	public class ShootLaserCommand : Command
	{
	
		private SpriteRenderer rend;
		private Sprite defaultSprite;
		private Sprite shootSprite;
		private GameObject laserPrefab;
		private GameObject laser;
	
		public ShootLaserCommand (Transform character, Vector2 shootDir, GameObject laserPrefab,
							SpriteRenderer rend, Sprite shootSprite) : base (character, shootDir)
		{
			this.rend = rend;
			defaultSprite = rend.sprite;
			this.shootSprite = shootSprite;
			this.laserPrefab = laserPrefab;
		}
		
		public override void Execute ()
		{
			rend.sprite = shootSprite;
			
			laser = (GameObject)MonoBehaviour.Instantiate (laserPrefab, character.position, Quaternion.identity);
			
			laser.GetComponent<Laser> ().Direction = dir;

		}
		
		public override void Undo ()
		{
			MonoBehaviour.DestroyImmediate (laser);
		}
		
		public void SetDefaultSprite ()
		{
			rend.sprite = defaultSprite;
		}
		
		public void SetShootSprite ()
		{
			rend.sprite = shootSprite;
		}

	}
}
