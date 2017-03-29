using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBird_behaviour : MonoBehaviour {

	public float minimum_x;
	public float maximum_x;
	public float flightSpeed;
	private Rigidbody2D rigid;
	private int dir;
	// Use this for initialization
	void Start (){
		dir = 0;
		rigid = GetComponent<Rigidbody2D>(); 
		rigid.AddForce(Vector2.right * flightSpeed * 10);
		if(maximum_x < minimum_x)
		{
			float t = maximum_x;
			maximum_x = minimum_x;
			minimum_x = t;
		}					  
	}
	
	// Update is called once per frame
	void Update () {
		if(dir == 0 && transform.position.x > maximum_x)
		{
			dir = 1; 
			rigid.AddForce(Vector2.left * flightSpeed * 20);
		}
		if (dir == 1 && transform.position.x < minimum_x)
		{
			dir = 1;
			rigid.AddForce(Vector2.left * flightSpeed * 20);
		}
	}
}
