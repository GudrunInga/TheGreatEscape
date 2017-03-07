using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Rotate : MonoBehaviour {
	Transform T;
	public float rotateSpeed;
	// Use this for initialization
	void Start () {
		T = GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {
		T.Rotate(0, rotateSpeed * Time.deltaTime, 0);
	}

	void OnTriggerEnter2D(Collider2D other)
	{											
		if (other.gameObject.CompareTag("Player"))
		{									  
			gameObject.SetActive(false);
		}
	}
}
