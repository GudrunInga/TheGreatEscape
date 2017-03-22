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
	private bool inside;

	// Use this for initialization
	void Start () {
		velocity = flightSpeed;
		rigid = GetComponent<Rigidbody2D>();		   
		afraid = false;
		GameObject player = UIController.instance.player;
		foreach (Transform t in player.GetComponentInChildren<Transform>()) {
			if(t.gameObject.name == repellant.name)
			{
				repellant = t.gameObject;
				break;
			}	
		}
		inside = false;	 
	}
					
	void Update(){
		if (!inside)
		{
			return;
		}		   
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
		if (other.gameObject.CompareTag("MainCamera")) { 
			inside = true;
			rigid.AddForce(Vector2.left * flightSpeed * 10);
		}
		if (other.gameObject.CompareTag("Player"))
		{
			if (repellant.activeInHierarchy)
			{
				Destroy(gameObject);
			}
			else
			{
				UIController.instance.GameOver("Bird");
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

