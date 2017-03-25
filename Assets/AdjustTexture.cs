using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTexture : MonoBehaviour { 

	// Use this for initialization
	void Start () {										 
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.x/5, transform.localScale.y/5);
	}
}
