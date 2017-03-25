using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Store : MonoBehaviour {

	//Gravity
	public float gravity;
	public Button buyGravity;
	public Text currentGravityLevel;
	public Button increaseGravityButton;
	public Button decreaseGravityButton;

	//Speed
	public float speed;
	public Button buySpeed;
	public Text currentSpeedLevel;
	public Button increaseSpeedButton;
	public Button decreaseSpeedButton;

	//Camera Acceleration
	public float cameraAcc;
	public Button buyCameraAcc;
	public Text currentCameraAccLevel;
	public Button increaseCameraAccButton;
	public Button decreaseCameraAccButton;

	private AssemblyCSharp.StoreGravity _gravityScript;
	private AssemblyCSharp.StoreSpeed _speedScript;
	private AssemblyCSharp.StoreCameraAcceleration _cameraAccScript;



	//To get access decrease increase the acceliration of the camera
	private Moveright moveCameraScript;

	// Use this for initialization
	void Start () {
		_gravityScript = new AssemblyCSharp.StoreGravity(gravity, buyGravity, currentGravityLevel, increaseGravityButton, decreaseGravityButton);
		_speedScript = new AssemblyCSharp.StoreSpeed (speed, buySpeed, currentSpeedLevel, increaseSpeedButton, decreaseSpeedButton);

		moveCameraScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Moveright> ();

		_cameraAccScript = new AssemblyCSharp.StoreCameraAcceleration (cameraAcc, buyCameraAcc, currentSpeedLevel, increaseCameraAccButton, decreaseCameraAccButton, moveCameraScript);


		DisableAllButtons ();
		GotMoney ();
	}
		
	/*Buy Speed (The legal kind)*/
	public void BuySpeed()
	{
		_speedScript.Buy ();
		GotMoney ();
	}
	//Decrease the gravity of the balloon
	public void BuyGravity()
	{
		_gravityScript.BuyGravity ();
		GotMoney ();
	}

	//Decrease the speed of the camera
	public void BuyCameraAcc()
	{
		_cameraAccScript.Buy ();
		GotMoney ();
	}

	//Increase the mass of the balloon (store menu -> player info menu)
	public void IncreaseSpeed()
	{
		_speedScript.Increase ();
	}
	/*decrease the mass of the balloon*/
	public void DecreaseSpeed()
	{
		_speedScript.Decrease ();
	}
	/*Increase the mass of the balloon*/
	public void IncreaseGravity()
	{
		_gravityScript.IncreaseGravity ();
	}
	/*decrease the mass of the balloon*/
	public void DecreaseGravity()
	{
		_gravityScript.DecreaseGravity ();
	}
	/*Increase the acceliration of the camera*/
	public void IncreaseCameraAcc()
	{
		_cameraAccScript.Increase ();
	}
	/*Decrease the acceliration of the camera*/
	public void DecreaseCameraAcc()
	{
		_cameraAccScript.Decrease ();
	}

/*HELPER FUNCTIONS*/
	/*Hide all buttons (only called at the start of the game*/
	void DisableAllButtons()
	{
		_gravityScript.DisableAllGravityButtons ();
		_speedScript.DisableAllButtons ();
		_cameraAccScript.DisableAllButtons ();
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
		_speedScript.DisableBuy ();
		_gravityScript.DisableBuy ();
		_cameraAccScript.DisableBuy ();
	}
	void EnableBuy()
	{
		_speedScript.EnableBuy ();
		_gravityScript.EnableBuy ();
		_cameraAccScript.EnableBuy ();
	}

}
