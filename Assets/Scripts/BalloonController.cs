using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {
	private KeyCode[] keyCodes = {
		 KeyCode.Alpha1,
		 KeyCode.Alpha2,
		 KeyCode.Alpha3,
		 KeyCode.Alpha4,
		 KeyCode.Alpha5,
		 KeyCode.Alpha6,
		 KeyCode.Alpha7,
		 KeyCode.Alpha8,
		 KeyCode.Alpha9,
	 };

	Rigidbody2D rigid;
	Transform transformer;

	public float rotationSpeed, movementspeed;

	public List<GameObject> models;
	private GameObject activeModel;
	public bool useFart;

	// Use this for initialization
	void Start ()
	{
		for (int i = 1; i < models.Count; i++)
		{
			models[i].SetActive(false);
		}
		rigid = GetComponent<Rigidbody2D>();
		transformer = GetComponent<Transform>();
		activeModel = models[0];
	}

	// Update is called once per frame
	void Update() {
		for (int i = 0; i < models.Count; i++)
		{
			if (Input.GetKeyDown(keyCodes[i]))
			{
				activeModel.SetActive(false);
				activeModel = models[i];
				activeModel.SetActive(true);
			}							  
		}

		if (useFart) {
			if (Input.GetKey(KeyCode.A)) {
				rigid.AddTorque(rotationSpeed);
			}
			if (Input.GetKey(KeyCode.D)) {
				rigid.AddTorque(-rotationSpeed);
			}
			if (Input.GetKey(KeyCode.W)) {
				rigid.AddForce(transform.right);
			}
		}
		else {
			if (Input.GetKey(KeyCode.Q)) {
				rigid.AddTorque(rotationSpeed);
			}
			if (Input.GetKey(KeyCode.E)) {
				rigid.AddTorque(-rotationSpeed);
			}
			if (Input.GetKey(KeyCode.A)) {
				rigid.AddForce(Vector2.left);
			}
			if (Input.GetKey(KeyCode.D)) {
				rigid.AddForce(Vector2.right);
			}
			if (Input.GetKey(KeyCode.W)) {
				rigid.AddForce(Vector2.up);
			}
			if (Input.GetKey(KeyCode.S)) {
				rigid.AddForce(Vector2.down);
			}
		}
	}


}
