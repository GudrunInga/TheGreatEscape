using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCollected : MonoBehaviour {

	private Text _coinsCollected;
	// Use this for initialization
	void Start () {
		Debug.Log ("I have collected: " + UIController.instance.GetCoins ());
		_coinsCollected = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		_coinsCollected.text = UIController.instance.GetCoins ().ToString();
	}
}
