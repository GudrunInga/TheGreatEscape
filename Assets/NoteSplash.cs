using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSplash : MonoBehaviour {

	public List<Vector2> ThrowVector;
	// Use this for initialization
	void Start () {
		int i = 0;
		foreach(Transform T in transform.GetComponentInChildren<Transform>())
		{
			Debug.Log("Mr. " + T.gameObject.name);
			foreach (Transform TT in transform.GetComponentInChildren<Transform>())
			{
				Debug.Log("Loves" + TT.gameObject.name);
				Physics2D.IgnoreCollision(TT.gameObject.GetComponent<Collider2D>(), T.gameObject.GetComponent<Collider2D>());
			}
			Rigidbody2D Rig = T.gameObject.GetComponent<Rigidbody2D>();
			Rig.velocity = ThrowVector[i];
			i = (i + 1) % ThrowVector.Count;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("MainCamera"))
		{
			Destroy(gameObject);
		}
	}
}
