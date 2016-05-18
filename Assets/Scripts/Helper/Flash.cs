using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class Flash : MonoBehaviour
{

	private Image image;
	private const float enabledTime = 1f;
	private const float disabledTime = 0.4f;
	private float currentTime = 0f;
	
	// Use this for initialization
	void Start ()
	{
		image = GetComponent<Image> ();

	}

	
	void Update ()
	{
		currentTime += Time.deltaTime;
	
		if (currentTime < enabledTime) {
			image.enabled = true;
		} else {
			image.enabled = false;
			
			if (currentTime > (enabledTime + disabledTime)) {
				currentTime = 0f;
			}
		}
	}
}
