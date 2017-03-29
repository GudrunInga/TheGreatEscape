using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteControl : MonoBehaviour
{

	public GameObject Repellant;
	// Use this for initialization
	void Start()
	{
		GameObject player = UIController.instance.player;
		foreach (Transform t in player.GetComponentInChildren<Transform>())
		{
			if (t.gameObject.name == Repellant.name)
			{
				Repellant = t.gameObject;
				break;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (Repellant.activeInHierarchy)
			{
				Destroy(GetComponent<Collider2D>());
				Destroy(GetComponent<Collider2D>());	
			} 
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("MainCamera"))
		{
			Destroy(gameObject);
		}
	}
}
