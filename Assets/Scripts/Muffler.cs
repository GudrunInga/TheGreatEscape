using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muffler : MonoBehaviour {

	public bool act;
	// Use this for initialization
	void Start () {
		act = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (act)
		{
			if (!UIController.instance.GameActive())
			{
				act = false;
				GetComponent<AudioLowPassFilter>().cutoffFrequency = 350;
			}
		}
		else
		{		
			if (UIController.instance.GameActive())
			{
				act = true;
				GetComponent<AudioLowPassFilter>().cutoffFrequency = 5000;
			} 
		}
	}
}
