using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bird_Controller : MonoBehaviour {

	public float flightSpeed;
	public float escapeSpeed;
	public GameObject repellant;
	private float velocity;
	private Rigidbody2D rigid;
	private bool afraid;

	// Use this for initialization
	void Start () {
		velocity = flightSpeed;
		rigid = GetComponent<Rigidbody2D>();
		rigid.AddForce(Vector2.left * flightSpeed*10);
		afraid = false;
		var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == repellant.name);
		repellant = objects.ElementAt(0);
	}
					
	void Update(){			   
		if (!afraid)
		{					
			if (repellant.activeInHierarchy)
			{					 
				afraid = true;
				Transform T = GetComponent<Transform>();
				T.localScale = new Vector3(-T.localScale.x, T.localScale.y, T.localScale.z);
				rigid.AddForce(Vector2.right * flightSpeed * 10);
				rigid.AddForce(Vector2.right * escapeSpeed * 10);
			}
		}
		else
		{	
			if (!repellant.activeInHierarchy)
			{
				afraid = false;
				Transform T = GetComponent<Transform>();
				T.localScale = new Vector3(-T.localScale.x, T.localScale.y, T.localScale.z);
				rigid.AddForce(Vector2.left * flightSpeed * 10);
				rigid.AddForce(Vector2.left * escapeSpeed * 10);
			}
		}
	}	
					
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{														 
			UIController.instance.GameOver("Bird");		
		}
	}
}
