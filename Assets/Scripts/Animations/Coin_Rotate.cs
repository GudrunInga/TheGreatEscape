using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Rotate : MonoBehaviour {
	Transform T;
	public float rotateSpeed;
	public AudioSource bading;
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
			bading.Play();
			gameObject.GetComponent<Transform>().position = new Vector3(1000, 1000, 1000);
            //gameObject.SetActive(false);
			UIController.instance.AddCoins();
            //Debug.Log("Coin " + UIController.instance.tempScore);
		}
	}
}
