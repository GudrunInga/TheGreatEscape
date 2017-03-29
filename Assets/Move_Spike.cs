using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Spike : MonoBehaviour {
	public Vector2 heading;
	Rigidbody2D rig;
	public GameObject ignoreWall;
	public Collider2D MyCollider;

	void Start () {
		rig = GetComponent<Rigidbody2D>();
		rig.AddForce(heading);
		foreach (GameObject kid in GameObject.FindGameObjectsWithTag(ignoreWall.tag))
		{
			Physics2D.IgnoreCollision(kid.GetComponent<Collider2D>(), MyCollider);
		}																			
	}

	void OnCollisionEnter2D(Collision2D collision)
	{										  
		heading = new Vector2(-heading.x, -heading.y);
		rig.AddForce(heading);
		rig.AddForce(heading); 
	}
}
