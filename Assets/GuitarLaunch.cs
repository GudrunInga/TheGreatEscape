using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarLaunch : MonoBehaviour {

	public Vector2 ThrowVector;
	// Use this for initialization
	void Awake() {	
		Rigidbody2D Rig = gameObject.GetComponent<Rigidbody2D>();
		Rig.velocity = ThrowVector;
	}
	   
}
