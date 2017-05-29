using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class StoreSpeed
	{
		private int _speedLevel;
		private int _currentLevel;

		private static float _speed;
		private Button _buySpeed;
		private Text _currentSpeedText;
		private Button _increaseSpeedButton;
		private Button _decreaseSpeedButton;

		public StoreSpeed (float speed, Button buy, Text text, Button increase, Button decrease)
		{
			_speed = speed;
			_buySpeed = buy;
			_currentSpeedText = text;
			_increaseSpeedButton = increase;
			_decreaseSpeedButton = decrease;
			_speedLevel = 0;
			_currentLevel = 0;

			_currentSpeedText.text += _currentLevel.ToString ();
		}

		public void Buy()
		{
			if (UIController.instance.GetCoins() > 0) {
				UIController.instance.SpendCoins ();

                Debug.Log("mass " + UIController.instance.player.GetComponent<Rigidbody2D>().mass);
				UIController.instance.player.GetComponent<Rigidbody2D> ().mass -= _speed;
                Debug.Log("mass after change " + UIController.instance.player.GetComponent<Rigidbody2D>().mass);
				_speedLevel++;
				_currentLevel++;
				_currentSpeedText.text = "Speed Level: " + _currentLevel.ToString();
			}
			ButtonCheck ();
		}

		public void DisableBuy()
		{
			_buySpeed.enabled = false;
			_buySpeed.GetComponent<Image> ().enabled = false;
			_buySpeed.GetComponentInChildren<Text> ().text = "";
		}

		public void EnableBuy()
		{
			_buySpeed.enabled = true;
			_buySpeed.GetComponent<Image> ().enabled = true;
			//Remove text in button
			_buySpeed.GetComponentInChildren<Text> ().text = "+";
		}
		public void Increase()
		{
			if (_currentLevel < _speedLevel) {
				_currentLevel++;
				UIController.instance.player.GetComponent<Rigidbody2D> ().mass -= _speed;
				_currentSpeedText.text = "Speed Level: " + _currentLevel.ToString ();
			}
			ButtonCheck ();
		}

		public void Decrease()
		{
			if (_currentLevel > 0) {
				_currentLevel--;
				UIController.instance.player.GetComponent<Rigidbody2D> ().mass += _speed;
				_currentSpeedText.text = "Speed Level: " + _currentLevel.ToString ();
			}
			ButtonCheck ();
		}

		public void DisableAllButtons()
		{
			DisableButton (true);
			DisableButton (false);
		}

		void EnableButton(bool sign)
		{
			if (sign) {
				_increaseSpeedButton.enabled = true;
				_increaseSpeedButton.GetComponent<Image> ().enabled = true;
				_increaseSpeedButton.GetComponentInChildren<Text> ().text = "+";
			} 
			else {
				_decreaseSpeedButton.enabled = true;
				_decreaseSpeedButton.GetComponent<Image> ().enabled = true;
				_decreaseSpeedButton.GetComponentInChildren<Text> ().text = "-";
			}
		}

		void DisableButton(bool sign)
		{
			if (sign) {
				_increaseSpeedButton.enabled = false;
				_increaseSpeedButton.GetComponent<Image> ().enabled = false;
				//Remove text in button
				_increaseSpeedButton.GetComponentInChildren<Text> ().text = "";
			} else {
				_decreaseSpeedButton.enabled = false;
				_decreaseSpeedButton.GetComponent<Image> ().enabled = false;
				//Remove text in button
				_decreaseSpeedButton.GetComponentInChildren<Text> ().text = "";
			}
		}

		void ButtonCheck()
		{
			if (_currentLevel == _speedLevel) {
				DisableButton (true);
			}
			if (_currentLevel > 0) {
				EnableButton (false);
			}
			if (_currentLevel < _speedLevel) {
				EnableButton (true);
			}
			if (_currentLevel == 0) {
				DisableButton (false);
			}
		}


	}
}

