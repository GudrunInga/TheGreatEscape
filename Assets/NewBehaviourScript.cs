using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	public AudioSource victory;

	void OnTriggerEnter2D(Collider2D other)
	{										  
		if (other.gameObject.CompareTag("Player"))
		{							  
			victory.Play();
			Destroy(other.gameObject);
		}
	}
}
