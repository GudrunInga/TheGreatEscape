using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_spike : MonoBehaviour
{
	Transform T;
	public float rotateSpeed;
	public AudioSource bad;
	// Use this for initialization
	void Start()
	{
		T = GetComponent<Transform>();

	}

	// Update is called once per frame
	void Update()
	{
		T.Rotate(0, 0, rotateSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("I am collided");

		Debug.Log("I am become " + other.tag);
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("I am Scary");
			bad.Play();
			Destroy(other.gameObject);
		}
	}
}
