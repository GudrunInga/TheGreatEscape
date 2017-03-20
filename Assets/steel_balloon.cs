using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steel_balloon : MonoBehaviour {
	bool active;
	BalloonController Dad;
	Rigidbody2D rig;
	void Start () {
		Dad = transform.parent.gameObject.GetComponent<BalloonController>();
		rig = transform.parent.gameObject.GetComponent<Rigidbody2D>();
	}
	void Update()
	{
		if (active)
		{
			Dad.models.Clear();
			rig.gravityScale = 2;
		}
		else
		{
			active = true;
		}
	}
}
