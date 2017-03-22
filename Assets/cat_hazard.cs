using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class cat_hazard : MonoBehaviour {

	public GameObject repellant;
	private bool inside;
	// Use this for initialization
	void Start () {
		var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == repellant.name);
		repellant = objects.ElementAt(0);	 
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("MainCamera"))
		{
			inside = true;
		}

		if (other.gameObject.CompareTag("Player"))
		{
			if(repellant.activeInHierarchy)
			{
				Destroy(gameObject);
			}
			else
			{		 
				UIController.instance.GameOver("Cat");
			}										
		}
	}
				
}
