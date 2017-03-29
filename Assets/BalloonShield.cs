using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonShield : MonoBehaviour {

	public GameObject Daddy;
	public GameObject MyShield;
	private BalloonController BobCat;
	private bool active;
	// Use this for initialization
	void Start () {
		BobCat = Daddy.GetComponent<BalloonController>();
		active = BobCat.getShield();
		if(!active)
		{
			MyShield.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {		  
		active = true;
		if (active) { 
			if (!BobCat.getShield())
			{	 
				active = false;		
				MyShield.SetActive(false);
			}
		}
	}
}
