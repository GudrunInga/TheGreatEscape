using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Controller : MonoBehaviour {

	public float flightSpeed;
	public float escapeSpeed;
	public GameObject avoider;
	private float velocity;
	private Rigidbody2D rigid;
	private bool afraid;

	// Use this for initialization
	void Start () {
		velocity = flightSpeed;
		rigid = GetComponent<Rigidbody2D>();
		rigid.AddForce(Vector2.left * flightSpeed*10);
		afraid = false;
	}
					
	void Update(){
		Debug.Log("1.");				   
		if (!afraid)
		{
			Debug.Log("2");
			if (avoider.activeSelf)	 
			{												
				afraid = true;
				Transform T = GetComponent<Transform>();
				T.localScale = new Vector3(-T.localScale.x, T.localScale.y, T.localScale.z);
				rigid.AddForce(Vector2.right * flightSpeed * 10);
				rigid.AddForce(Vector2.right * escapeSpeed * 10);
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
