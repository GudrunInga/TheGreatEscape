using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Store : MonoBehaviour {

	//To make easier to set how much increase/decrease between levels
	public float gravity;
	public float speed;
	public float cameraAcc;


	//Possible temporary variable
	public Button buySpeed;
	public Button buyGravity;
	public Button buyCameraAcc;

	public Text currentSpeedLevel;
	public Text currentGravityLevel;
	public Text currentCameraAccLevel;

	public Button increaseSpeedButton;
	public Button decreaseSpeedButton;
	public Button increaseGravityButton;
	public Button decreaseGravityButton;
	public Button increaseCameraAccButton;
	public Button decreaseCameraAccButton;

	private int _speedLevel;
	private int _currentSpeedLevel;

	private int _gravityLevel;
	private int _currentGravityLevel;

	private int _cameraAccLevel;
	private int _currentCameraAccLevel;

	//To get access decrease increase the acceliration of the camera
	private Moveright moveCameraScript;

	// Use this for initialization
	void Start () {
		moveCameraScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Moveright> ();
		InitializeVariables ();
		DisableAllButtons ();
		GotMoney ();
	}
		
	/*Buy Speed (The legal kind)*/
	public void BuySpeed()
	{
		/*Buy increased speed*/
		if (UIController.instance.GetCoins() > 0) {
			UIController.instance.SpendCoins ();

			UIController.instance.player.GetComponent<Rigidbody2D> ().mass -= speed;
			_speedLevel++;
			_currentSpeedLevel++;
			currentSpeedLevel.text = "Speed Level: " + _currentSpeedLevel.ToString();

			ButtonCheck ("speed");
		}
		GotMoney ();
	}
	//Decrease the gravity of the balloon
	public void BuyGravity()
	{
		if (UIController.instance.GetCoins () > 0) {
			UIController.instance.SpendCoins ();

			UIController.instance.player.GetComponent<Rigidbody2D> ().gravityScale -= gravity;
			_gravityLevel++;
			_currentGravityLevel++;
			currentGravityLevel.text = "Gravity Level: " + _currentGravityLevel.ToString();
			ButtonCheck ("gravity");
		}
		GotMoney ();
	}

	//Decrease the speed of the camera
	public void BuyCameraAcc()
	{
		if (UIController.instance.GetCoins () > 0) {
			UIController.instance.SpendCoins ();

			moveCameraScript.decrease_accel (cameraAcc);
			_cameraAccLevel++;
			_currentCameraAccLevel++;
			currentCameraAccLevel.text = "Camera Level: " + _currentCameraAccLevel.ToString ();

			ButtonCheck ("camera");
		}
		GotMoney ();
	}

	//Increase the mass of the balloon (store menu -> player info menu)
	public void IncreaseSpeed()
	{
		if (_currentSpeedLevel < _speedLevel) {
			_currentSpeedLevel++;
			UIController.instance.player.GetComponent<Rigidbody2D> ().mass -= speed;
			currentSpeedLevel.text = "Speed Level: " + _currentSpeedLevel.ToString ();

		}
		ButtonCheck ("speed");
	}
	/*decrease the mass of the balloon*/
	public void DecreaseSpeed()
	{
		if (_currentSpeedLevel > 0) {
			_currentSpeedLevel--;
			UIController.instance.player.GetComponent<Rigidbody2D> ().mass += speed;
			currentSpeedLevel.text = "Speed Level: " + _currentSpeedLevel.ToString ();
		}
		ButtonCheck ("speed");
	}
	/*Increase the mass of the balloon*/
	public void IncreaseGravity()
	{
		if (_currentGravityLevel < _gravityLevel) {
			_currentGravityLevel++;
			UIController.instance.player.GetComponent<Rigidbody2D> ().mass -= gravity;
			currentGravityLevel.text = "Gravity Level: " + _currentGravityLevel.ToString ();
		}
		ButtonCheck ("gravity");
	}
	/*decrease the mass of the balloon*/
	public void DecreaseGravity()
	{
		if (_currentGravityLevel > 0) {
			_currentGravityLevel--;
			UIController.instance.player.GetComponent<Rigidbody2D> ().mass += gravity;
			currentGravityLevel.text = "Gravity Level: " + _currentGravityLevel.ToString ();
		}
		ButtonCheck ("gravity");
	}
	/*Increase the acceliration of the camera*/
	public void IncreaseCameraAcc()
	{
		if (_currentCameraAccLevel < _cameraAccLevel) {
			_currentCameraAccLevel++;
			moveCameraScript.increase_accel (cameraAcc);
			currentCameraAccLevel.text = "Camera Level: " + _currentCameraAccLevel.ToString ();

		}
		ButtonCheck ("camera");
	}
	/*Decrease the acceliration of the camera*/
	public void DecreaseCameraAcc()
	{
		if (_currentCameraAccLevel > 0) {
			_currentCameraAccLevel--;
			moveCameraScript.decrease_accel (cameraAcc);
			currentCameraAccLevel.text = "Camera Level: " + _currentCameraAccLevel.ToString ();
		}
		ButtonCheck ("camera");
	}

/*HELPER FUNCTIONS*/
	/*Hide all buttons (only called at the start of the game*/
	void DisableAllButtons()
	{
		DisableButton ("speed", true);
		DisableButton ("speed", false);
		DisableButton ("gravity", true);
		DisableButton ("gravity", false);
		DisableButton ("camera", true);
		DisableButton ("camera", false);

	}
	/*Hide button (in store menu -> player info menu)*/
	void DisableButton(string button, bool sign)
	{
		if (button == "speed") {
			if (sign) {
				increaseSpeedButton.enabled = false;
				increaseSpeedButton.GetComponent<Image> ().enabled = false;
				//Remove text in button
				increaseSpeedButton.GetComponentInChildren<Text> ().text = "";
			} else {
				decreaseSpeedButton.enabled = false;
				decreaseSpeedButton.GetComponent<Image> ().enabled = false;
				//Remove text in button
				decreaseSpeedButton.GetComponentInChildren<Text> ().text = "";
			}
		} else if (button == "gravity") {
			if (sign) {
				increaseGravityButton.enabled = false;
				increaseGravityButton.GetComponent<Image> ().enabled = false;
				//Remove text in button
				increaseGravityButton.GetComponentInChildren<Text> ().text = "";
			} else {
				decreaseGravityButton.enabled = false;
				decreaseGravityButton.GetComponent<Image> ().enabled = false;
				//Remove text in button
				decreaseGravityButton.GetComponentInChildren<Text> ().text = "";
			}
		} else if (button == "camera") {
			if (sign) {
				increaseCameraAccButton.enabled = false;
				increaseCameraAccButton.GetComponent<Image>().enabled = false;
				increaseCameraAccButton.GetComponentInChildren<Text>().text = "";
			} else{
				decreaseCameraAccButton.enabled = false;
				decreaseCameraAccButton.GetComponent<Image>().enabled = false;
				decreaseCameraAccButton.GetComponentInChildren<Text>().text = "";
			}
		}
	}

	/*Show button (in store menu -> player info menu)*/
	void EnableButton(string button, bool sign)
	{
		if (button == "speed") {
			if (sign) {
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
		else if (button == "gravity") {
			if (sign) {
				increaseGravityButton.enabled = true;
				increaseGravityButton.GetComponent<Image> ().enabled = true;
				increaseGravityButton.GetComponentInChildren<Text> ().text = "+";
			} 
			else {
				decreaseGravityButton.enabled = true;
				decreaseGravityButton.GetComponent<Image> ().enabled = true;
				decreaseGravityButton.GetComponentInChildren<Text> ().text = "-";
			}
		}
		else if (button == "camera") {
			if (sign) {
				increaseCameraAccButton.enabled = true;
				increaseCameraAccButton.GetComponent<Image>().enabled = true;
				increaseCameraAccButton.GetComponentInChildren<Text>().text = "+";
			} else{
				decreaseCameraAccButton.enabled = true;
				decreaseCameraAccButton.GetComponent<Image>().enabled = true;
				decreaseCameraAccButton.GetComponentInChildren<Text>().text = "-";
			}
		}
	}
	/*Should button be shown, e.g. if at max speed level the plus button should not be available*/
	void ButtonCheck (string type)
	{
		if (type == "speed") {
			if (_currentSpeedLevel == _speedLevel) {
				DisableButton ("speed", true);
			}
			if (_currentSpeedLevel > 0) {
				EnableButton ("speed", false);
			}
			if (_currentSpeedLevel < _speedLevel) {
				EnableButton ("speed", true);
			}
			if (_currentSpeedLevel == 0) {
				DisableButton ("speed", false);
			}
		} else if (type == "gravity") {
			if (_currentGravityLevel == _gravityLevel) {
				DisableButton ("gravity", true);
			}
			if (_currentGravityLevel > 0) {
				EnableButton ("gravity", false);
			}
			if (_currentGravityLevel < _gravityLevel) {
				EnableButton ("gravity", true);
			}
			if (_currentGravityLevel == 0) {
				DisableButton ("gravity", false);
			}
		} else if (type == "camera") {
			if (_currentCameraAccLevel == _cameraAccLevel) {
				DisableButton ("camera", true);
			}
			if (_currentCameraAccLevel > 0) {
				EnableButton ("camera", false);
			}
			if (_currentCameraAccLevel < _cameraAccLevel) {
				EnableButton ("camera", true);
			}
			if (_currentCameraAccLevel == 0) {
				DisableButton ("camera", false);
			}
		}
	}

	/*disable button if no more money*/
	public void GotMoney()
	{
		if (UIController.instance.GetCoins () == 0) {
			DisableBuy ();
		} else {
			EnableBuy();
		}
	}
	void DisableBuy()
	{
		buySpeed.enabled = false;
		buySpeed.GetComponent<Image> ().enabled = false;
		buySpeed.GetComponentInChildren<Text> ().text = "";

		buyGravity.enabled = false;
		buyGravity.GetComponent<Image> ().enabled = false;
		buyGravity.GetComponentInChildren<Text> ().text = "";

		buyCameraAcc.enabled = false;
		buyCameraAcc.GetComponent<Image> ().enabled = false;
		buyCameraAcc.GetComponentInChildren<Text> ().text = "";
	}
	void EnableBuy()
	{
		buySpeed.enabled = true;
		buySpeed.GetComponent<Image> ().enabled = true;
		//Remove text in button
		buySpeed.GetComponentInChildren<Text> ().text = "+";

		buyGravity.enabled = true;
		buyGravity.GetComponent<Image> ().enabled = true;
		//Remove text in button
		buyGravity.GetComponentInChildren<Text> ().text = "+";

		buyCameraAcc.enabled = true;
		buyCameraAcc.GetComponent<Image> ().enabled = true;
		buyCameraAcc.GetComponentInChildren<Text> ().text = "+";
	}
	void InitializeVariables()
	{
		_speedLevel = 0;
		_currentSpeedLevel = 0;
		currentSpeedLevel.text += _currentSpeedLevel.ToString ();

		_gravityLevel = 0;
		_currentGravityLevel = 0;
		currentGravityLevel.text += _currentGravityLevel.ToString();

		_cameraAccLevel = 0;
		_currentCameraAccLevel = 0;
		currentCameraAccLevel.text += _currentCameraAccLevel.ToString ();
	}
}
