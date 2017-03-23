using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Store : MonoBehaviour {

	public Text currentSpeedLevel;
	public Button increaseSpeedButton;
	public Button decreaseSpeedButton;

	private int _speedLevel;
	private int _currentSpeedLevel; 

	// Use this for initialization
	void Start () {
		_speedLevel = 0;
		_currentSpeedLevel = 0;
		currentSpeedLevel.text = _currentSpeedLevel.ToString ();
		
		//disable plus sign
		disableButton (true);
		//disable minus sign
		disableButton (false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*Hide button (in store menu -> player info menu)*/
	void disableButton(bool plusSign)
	{
		if (plusSign) {
			increaseSpeedButton.enabled = false;
			increaseSpeedButton.GetComponent<Image> ().enabled = false;
			increaseSpeedButton.GetComponentInChildren<Text> ().text = "";
		} 
		else {
			decreaseSpeedButton.enabled = false;
			decreaseSpeedButton.GetComponent<Image> ().enabled = false;
			decreaseSpeedButton.GetComponentInChildren<Text> ().text = "";
		}
	}
	/*Show button (in store menu -> player info menu)*/
	void enableButton(bool plusSign)
	{
		if (plusSign) {
			increaseSpeedButton.enabled = true;
			increaseSpeedButton.GetComponent<Image> ().enabled = true;
			increaseSpeedButton.GetComponentInChildren<Text> ().text = "+";
		} 
		else {
			decreaseSpeedButton.enabled = true;
			decreaseSpeedButton.GetComponent<Image> ().enabled = true;
			decreaseSpeedButton.GetComponentInChildren<Text> ().text = "-";
		}
	}
	/*Buy Speed (The legal kind)*/
	public void BuySpeed()
	{
		/*Buy increased speed*/
		if (UIController.instance.GetCoins() > 0) {
			UIController.instance.SpendCoins ();

			UIController.instance.player.GetComponent<Rigidbody2D> ().mass -= 0.1f;
			_speedLevel++;
			_currentSpeedLevel++;
			currentSpeedLevel.text = _currentSpeedLevel.ToString();

			ButtonCheck ();
		}
	}

	//Increase the mass of the balloon (store menu -> player info menu)
	public void IncreaseSpeed()
	{
		if (_currentSpeedLevel < _speedLevel) {
			_currentSpeedLevel++;
			UIController.instance.player.GetComponent<Rigidbody2D> ().mass -= 0.1f;
			currentSpeedLevel.text = _currentSpeedLevel.ToString ();
			ButtonCheck ();
		}
	}
	/*decrease the mass of the balloon*/
	public void decreaseSpeed()
	{
		if (_currentSpeedLevel > 0) {
			_currentSpeedLevel--;
			UIController.instance.player.GetComponent<Rigidbody2D> ().mass += 0.1f;
			currentSpeedLevel.text = _currentSpeedLevel.ToString ();
		}
		ButtonCheck ();
	}


	void ButtonCheck ()
	{
		if (_currentSpeedLevel == _speedLevel) {
			disableButton (true);
		}
		if (_currentSpeedLevel > 0) {
			enableButton (false);
		}
		if (_currentSpeedLevel < _speedLevel) {
			enableButton (true);
		}
		if (_currentSpeedLevel == 0) {
			disableButton (false);
		}
	}
}
