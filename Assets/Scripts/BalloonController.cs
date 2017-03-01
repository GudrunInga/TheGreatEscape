using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {

	SpriteRenderer renderer;
	Rigidbody2D rigid;
	Transform transformer;

	public float rotationSpeed, movementspeed;

	public Sprite image1, image2, image3;
	public bool useFart;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
		rigid = GetComponent<Rigidbody2D>();
		transformer = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.Alpha1)) {
			renderer.sprite = image1;
		}

		if (Input.GetKey(KeyCode.Alpha2)) {
			renderer.sprite = image2;
		}

		if (Input.GetKey(KeyCode.Alpha3)) {
			renderer.sprite = image3;
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
		}
	}
}
