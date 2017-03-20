using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Laundry_Controller : MonoBehaviour {
	public GameObject repellant;
	public GameObject heavyballoon;
	public GameObject shirt;
	public GameObject pants;
	private bool cut;
	// Use this for initialization
	void Start () {
		cut = false;
		var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == repellant.name);
		repellant = objects.ElementAt(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") && !cut)
		{
			cut = true;
			if (repellant.activeInHierarchy){
				foreach (Transform child in gameObject.transform.GetChild(0).GetComponentInChildren<Transform>())
				{
					Debug.Log(child.gameObject.name);
					Rigidbody kidRigid = child.gameObject.AddComponent<Rigidbody>();
					Vector3 vec = new Vector3(Random.Range(-13f, 13f), Random.Range(3f, 6f));
					Quaternion rot = Random.rotation;
					kidRigid.velocity = vec;
					kidRigid.SetDensity(0);
					kidRigid.drag = 0.2f;
					kidRigid.rotation = rot;
				}
			}
			else {
				pants.SetActive(false);
				shirt.SetActive(false);
				BalloonController balloon = other.gameObject.transform.root.gameObject.GetComponent<BalloonController>();
				balloon.setgrav(0.11f);
				balloon.forceModel(heavyballoon);
			}
		}
	}
}
