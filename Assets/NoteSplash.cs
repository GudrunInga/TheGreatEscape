using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSplash : MonoBehaviour {

	public List<Vector2> ThrowVector;
	private float destruction;
	private bool dying;
	// Use this for initialization
	void Start () {
		destruction = 10;
		dying = false;
		int i = 0;
		foreach(Transform T in transform.GetComponentInChildren<Transform>())
		{															   
			foreach (Transform TT in transform.GetComponentInChildren<Transform>())
			{											  
				Physics2D.IgnoreCollision(TT.gameObject.GetComponent<Collider2D>(), T.gameObject.GetComponent<Collider2D>());
			}
			Rigidbody2D Rig = T.gameObject.GetComponent<Rigidbody2D>();
			Rig.velocity = ThrowVector[i];
			i = (i + 1) % ThrowVector.Count;
		}
	}

	void Update()
	{
		if (dying)
		{
			destruction -= Time.deltaTime;
			if(destruction <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("MainCamera"))
		{
			dying = true;
		}
	}
}
