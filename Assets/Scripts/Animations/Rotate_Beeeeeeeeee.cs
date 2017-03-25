using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Beeeeeeeeee : MonoBehaviour {
	ConstantForce f;
	public Rigidbody rig;
	public GameObject repellant;
	float tt;
	private Renderer R;
	// Use this for initialization
	void Start ()
	{
		GameObject player = UIController.instance.player;
		foreach (Transform t in player.GetComponentInChildren<Transform>())
		{
			if (t.gameObject.name == repellant.name)
			{
				repellant = t.gameObject;
				break;
			}
		}
		f = gameObject.GetComponent<ConstantForce>();
		f.torque = Random.insideUnitSphere * 5;
		tt = 15;
		R = GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!R.isVisible)
		{
			tt -= Time.deltaTime;
			if(tt < 0)
			{
				Destroy(gameObject);
			}
		}
		f.torque += Random.insideUnitSphere * 2;
		rig.AddForce(new Vector3(0.025f, 0.01f, 0)); 

		if(Vector3.Distance(transform.position,repellant.transform.position) < 3)
		{
			rig.useGravity = true;	 
		}
	}
}
