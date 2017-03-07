using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {

	SpriteRenderer render;
	Rigidbody2D rigid;
	Transform transformer;

	public float rotationSpeed, movementspeed;

	public Sprite image1, image2, image3;
	public bool useFart;

	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer>();
		rigid = GetComponent<Rigidbody2D>();
		transformer = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.Alpha1)) {
			render.sprite = image1;
		}

		if (Input.GetKey(KeyCode.Alpha2)) {
			render.sprite = image2;
		}

		if (Input.GetKey(KeyCode.Alpha3)) {
			render.sprite = image3;
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
