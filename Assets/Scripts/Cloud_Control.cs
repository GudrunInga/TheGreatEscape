using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Control : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float scale = Random.Range(0.001f, 0.13f);
		transform.localScale = new Vector3(scale, scale, 0.015f);
		GetComponent<Rigidbody2D>().AddForce(Vector2.left *  (float)(120 * (scale - 0.08)));
		if(scale > 0.085)
		{
			transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				-1); 
		}

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("MainCamera"))
		{
			Destroy(gameObject);
		}
	}
}
