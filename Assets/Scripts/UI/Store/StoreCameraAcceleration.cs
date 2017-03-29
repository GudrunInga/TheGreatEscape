using System;

namespace AssemblyCSharp
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class StoreCameraAcceleration
	{
		private float _cameraAcc;
		private Button _buy;
		private Text _currentLevelText;
		private Button _increaseButton;
		private Button _decreaseButton;
		private Moveright _moveCamera;

		private int _level;
		private int _currentLevel;

		public StoreCameraAcceleration (float cameraAcc, Button buy, Text text, Button increase, Button decrease, Moveright moveCamera)
		{
			_cameraAcc = cameraAcc;
			_buy = buy;
			_currentLevelText = text;
			_increaseButton = increase;
			_decreaseButton = decrease;
			_moveCamera = moveCamera;

			_level = 0;
			_currentLevel = 0;

			_currentLevelText.text = "Camera Level: " + _currentLevel.ToString ();
		}

		public void Buy()
		{
			if (UIController.instance.GetCoins () > 0) {
				UIController.instance.SpendCoins ();

				_moveCamera.decrease_accel (_cameraAcc);
				_level++;
				_currentLevel++;
				_currentLevelText.text = "Camera Level: " + _currentLevel.ToString ();
			}
			ButtonCheck ();
		}
		public void DisableBuy()
		{
			_buy.enabled = false;
			_buy.GetComponent<Image> ().enabled = false;
			_buy.GetComponentInChildren<Text> ().text = "";
		}

		public void EnableBuy()
		{
			_buy.enabled = true;
			_buy.GetComponent<Image> ().enabled = true;
			_buy.GetComponentInChildren<Text> ().text = "+";
		}

		public void Increase()
		{
			if (_currentLevel < _level) {
				_currentLevel++;
				_moveCamera.increase_accel (_cameraAcc);
				_currentLevelText.text = "Camera Level: " + _currentLevel.ToString ();

			}
			ButtonCheck ();
		}

		public void Decrease()
		{
			if (_currentLevel > 0) {
				_currentLevel--;
				_moveCamera.decrease_accel (_cameraAcc);
				_currentLevelText.text = "Camera Level: " + _currentLevel.ToString ();
			}
			ButtonCheck ();
		}

		public void DisableAllButtons()
		{
			DisableButton (true);
			DisableButton (false);
		}

		void DisableButton(bool sign)
		{
			if (sign) {
				_increaseButton.enabled = false;
				_increaseButton.GetComponent<Image>().enabled = false;
				_increaseButton.GetComponentInChildren<Text>().text = "";
			} else{
				_decreaseButton.enabled = false;
				_decreaseButton.GetComponent<Image>().enabled = false;
				_decreaseButton.GetComponentInChildren<Text>().text = "";
			}
		}

		void EnableButton(bool sign)
		{
			if (sign) {
				_increaseButton.enabled = true;
				_increaseButton.GetComponent<Image>().enabled = true;
				_increaseButton.GetComponentInChildren<Text>().text = "+";
			} else{
				_decreaseButton.enabled = true;
				_decreaseButton.GetComponent<Image>().enabled = true;
				_decreaseButton.GetComponentInChildren<Text>().text = "-";
			}
		}
		void ButtonCheck()
		{
			if (_currentLevel == _level) {
				DisableButton (true);
			}
			if (_currentLevel > 0) {
				EnableButton (false);
			}
			if (_currentLevel < _level) {
				EnableButton (true);
			}
			if (_currentLevel == 0) {
				DisableButton (false);
			}
		}
	}
}

