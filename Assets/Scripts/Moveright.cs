using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveright : MonoBehaviour {

	
    public float speed;
	public float acceleration;
	private Transform trans;
	private float realAcceleration;
	// Update is called once per frame
	void Start() {
        trans = GetComponent<Transform>();
		realAcceleration = acceleration;		 
	}

	void Update () {			  
        Vector3 movement = new Vector3(speed * Time.deltaTime,0,0);
        trans.position = trans.position + movement;
		speed += realAcceleration * Time.deltaTime;
		if (UIController.instance.steel)
		{
			realAcceleration = 1.2f;
		}
		if(UIController.instance.player.transform.position.x < transform.position.x - 40)
		{					   
			UIController.instance.GameOver("Slow");
		}
	}
	public void add_accel(float acc)
	{
		realAcceleration += acc;
	}

	public void decrease_accel(float acc)
	{
		realAcceleration -= acc;
	}
}
