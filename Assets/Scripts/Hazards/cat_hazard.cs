using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class cat_hazard : MonoBehaviour {
	public AudioSource catScared;
	public AudioSource catPop;

	public GameObject repellant;
	private bool inside;
	// Use this for initialization
	void Start () {
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
				catScared.Play ();

				Destroy(gameObject);
			}
			else
			{		 
				catPop.Play ();
				UIController.instance.GameOver("Cat");
			}										
		}
	}
				
}
