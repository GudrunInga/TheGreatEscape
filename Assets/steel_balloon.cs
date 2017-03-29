﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steel_balloon : MonoBehaviour {
	bool active;
	BalloonController Dad;
	Rigidbody2D rig;
	void Start () {
		Dad = transform.parent.gameObject.GetComponent<BalloonController>();
		rig = transform.parent.gameObject.GetComponent<Rigidbody2D>();
		active = false;
	}
	void Update()
	{
		if (active)
		{
			Dad.models.Clear();					 
			rig.sharedMaterial = (PhysicsMaterial2D)Resources.Load("Assets/Scripts/Physics Material/Super-heavy");
			rig.gravityScale = 2;
            UIController.instance.steel = true;
		}
		else
		{
			active = true;
		}
	}
}
