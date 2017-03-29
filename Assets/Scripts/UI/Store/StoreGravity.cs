using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
public class StoreGravity{
	private float _gravity;
	private Button _buyButton;
	private Text _currentGravityText;
	private Button _increaseButton;
	private Button _decreaseButton;
	private int _gravityLevel;
	private int _currentGravityLevel;

	public StoreGravity(float gravity, Button buy, Text currentGravityText, Button increase, Button decrease)
	{
		_gravity = gravity;
		_buyButton = buy;
		_currentGravityText = currentGravityText;
		_increaseButton = increase;
		_decreaseButton = decrease;
		_gravityLevel = 0;
		_currentGravityLevel = 0;

		_currentGravityText.text = "Gravity Level: " + _currentGravityLevel.ToString ();
	}
	public void BuyGravity()
	{
		if (UIController.instance.GetCoins () > 0) {
			UIController.instance.SpendCoins ();

			UIController.instance.player.GetComponent<Rigidbody2D> ().gravityScale -= _gravity;
			_gravityLevel++;
			_currentGravityLevel++;
			_currentGravityText.text = "Gravity Level: " + _currentGravityLevel.ToString();
			ButtonCheck ();
		}
	}

	public void DisableAllGravityButtons()
	{
		DisableButton (true);
		DisableButton (false);
	}

	public void DisableBuy()
	{
		_buyButton.enabled = false;
		_buyButton.GetComponent<Image> ().enabled = false;
		_buyButton.GetComponentInChildren<Text> ().text = "";
	}

	public void EnableBuy()
	{
		_buyButton.enabled = true;
		_buyButton.GetComponent<Image> ().enabled = true;
		//Remove text in button
		_buyButton.GetComponentInChildren<Text> ().text = "+";
	}

	/*Increase the mass of the balloon*/
	public void IncreaseGravity()
	{
		if (_currentGravityLevel < _gravityLevel) {
			_currentGravityLevel++;
			UIController.instance.player.GetComponent<Rigidbody2D> ().gravityScale += _gravity;
			_currentGravityText.text = "Gravity Level: " + _currentGravityLevel.ToString ();
		}
		ButtonCheck ();
	}
	/*decrease the mass of the balloon*/
	public void DecreaseGravity()
	{
		if (_currentGravityLevel > 0) {
			_currentGravityLevel--;
			UIController.instance.player.GetComponent<Rigidbody2D> ().gravityScale -= _gravity;
			_currentGravityText.text = "Gravity Level: " + _currentGravityLevel.ToString ();
		}
		ButtonCheck ();
	}

	void DisableButton(bool sign)
	{
		if (sign) {
			_increaseButton.enabled = false;
			_increaseButton.GetComponent<Image> ().enabled = false;
			_increaseButton.GetComponentInChildren<Text> ().text = "";
		} else {
			_decreaseButton.enabled = false;
			_decreaseButton.GetComponent<Image> ().enabled = false;
			_decreaseButton.GetComponentInChildren<Text> ().text = "";
		}
	}
		
	void EnableButton(bool sign)
	{
		if (sign) {
			_increaseButton.enabled = true;
			_increaseButton.GetComponent<Image> ().enabled = true;
			_increaseButton.GetComponentInChildren<Text> ().text = "+";
		} else {
			_decreaseButton.enabled = true;
			_decreaseButton.GetComponent<Image> ().enabled = true;
			_decreaseButton.GetComponentInChildren<Text> ().text = "-";
		}
	}

	void ButtonCheck()
	{
		
		if (_currentGravityLevel == _gravityLevel) {
			DisableButton (true);
		}
		if (_currentGravityLevel > 0) {
			EnableButton (false);
		}
		if (_currentGravityLevel < _gravityLevel) {
			EnableButton (true);
		}
		if (_currentGravityLevel == 0) {
			DisableButton (false);
		}
	}
}
}