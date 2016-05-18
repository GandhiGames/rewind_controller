using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{

	public float Time = 5f;
	
	// Use this for initialization
	void Start ()
	{
		Invoke ("DestroyThis", Time);
	}
	
	private void DestroyThis ()
	{
		Destroy (gameObject);
	}
}
