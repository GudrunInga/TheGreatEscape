using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive_Controller : MonoBehaviour {
	private bool active;
	public GameObject bees;
	// Use this for initialization
	void Start () {
		active = false;
	}

	// Update is called once per frame
	void Update()
	{		
		if (!active && GetComponentInChildren<Renderer>().isVisible)
		{
			active = true;
			Instantiate(bees);
		}
	}
}
