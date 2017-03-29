using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punk_Controller : MonoBehaviour {
	public float Jam_Timer;
	public GameObject Notes_n_Groove;
	private float jammies;
	public bool startActive = false;
	private bool active;
	public GameObject Repellant;
	public GameObject LiveModel;
	public GameObject DeadModel;
	// Use this for initialization
	void Start () {
		jammies = Jam_Timer;
		active = startActive;
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
	
	// Update is called once per frame
	void Update () {
		if (!active)
		{
			return;
		}
		jammies -= Time.deltaTime;
		if(jammies <= 0)
		{
			GameObject Jam = Instantiate(Notes_n_Groove);
			Jam.transform.position = transform.position;
			jammies = Jam_Timer;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{														 
		if(other.gameObject.CompareTag("MainCamera"))
		{
			active = true;									 
		}
		if (other.gameObject.CompareTag("Player"))
		{
			if(active && Repellant.activeInHierarchy)
			{
				active = false;
				LiveModel.SetActive(false);
				DeadModel.SetActive(true);
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
