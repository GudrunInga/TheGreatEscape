﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Laundry_Controller : MonoBehaviour {
	public GameObject repellant;
	public GameObject heavyballoon;
	public GameObject shirt;
	public GameObject pants;
	private bool cut;
	// Use this for initialization
	void Start () {
		cut = false;
		GameObject player = UIController.instance.player;
		foreach (Transform t in player.GetComponentInChildren<Transform>())
		{
			if (t.gameObject.name == repellant.name)
			{
				repellant = t.gameObject;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") && !cut)
		{
			cut = true;	   
			if (repellant.activeInHierarchy){ 
				foreach (Transform child in gameObject.transform.GetChild(0).GetComponentInChildren<Transform>())
				{
					Debug.Log(child.gameObject.name);
					Rigidbody kidRigid = child.gameObject.AddComponent<Rigidbody>();
					Vector3 vec = new Vector3(Random.Range(-9f, 9f), Random.Range(5f, 12f));
					Vector3 rot =new Vector3(Random.Range(-30f, 30f), Random.Range(30f, 30f), Random.Range(-75f, 75f));
					kidRigid.velocity = vec;
					kidRigid.SetDensity(0);
					kidRigid.drag = 0.2f;
					kidRigid.AddTorque(rot);
				}
			}
			else {
				UIController.instance.laundry = true;
				pants.SetActive(false);
				shirt.SetActive(false);
				BalloonController balloon = other.gameObject.transform.root.gameObject.GetComponent<BalloonController>();
				balloon.setgrav(0.07f);
				balloon.forceModel(heavyballoon);
                
			}
		}
	}
}
