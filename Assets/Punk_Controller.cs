using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punk_Controller : MonoBehaviour {

	public float Jam_Timer;		

	public GameObject Repellant;
	public GameObject LiveModel;
	public GameObject DeadModel; 	 
	public GameObject Notes_n_Groove;	

	public bool startActive = false;

	private float jammies;			
	private bool active;	 
	public AudioSource E,G,D,A,C;
	private List<AudioSource> Chords;
	private int current; 



	// Use this for initialization
	void Start () {
		jammies = Jam_Timer;
		active = startActive;
		GameObject player = UIController.instance.player;
		Chords = new List<AudioSource>();

		Chords.Add(E); Chords.Add(G); Chords.Add(D); Chords.Add(A);
		Chords.Add(E); Chords.Add(G); Chords.Add(D); Chords.Add(A);	  
		Chords.Add(C); Chords.Add(D); Chords.Add(A);
		current = 0;

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
			Chords[current].Play();
			current = (current + 1) % Chords.Count;			
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
