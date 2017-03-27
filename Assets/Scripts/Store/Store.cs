using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Store : MonoBehaviour {

    public bool IamTesting;

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

	private static List<bool> _ownedForms;
	private static bool _firstRun = true;

	//To get access decrease increase the acceliration of the camera
	private Moveright moveCameraScript;

	// Use this for initialization
	void Start () {
		_gravityScript = new AssemblyCSharp.StoreGravity(gravity, buyGravity, currentGravityLevel, increaseGravityButton, decreaseGravityButton);
		_speedScript = new AssemblyCSharp.StoreSpeed (speed, buySpeed, currentSpeedLevel, increaseSpeedButton, decreaseSpeedButton);

		moveCameraScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Moveright> ();
		_cameraAccScript = new AssemblyCSharp.StoreCameraAcceleration (cameraAcc, buyCameraAcc, currentCameraAccLevel, increaseCameraAccButton, decreaseCameraAccButton, moveCameraScript);

		DisableAllButtons ();
		GotMoney ();
		if (_firstRun) {
			InitializeForms ();
			_firstRun = false;
		}
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

	void InitializeForms()
	{
		int size = UIController.instance.player.GetComponent<BalloonController> ().models.Count;
		_ownedForms = new List<bool> ();
		_ownedForms.Add(true);
        for (int i = 1; i < size; i++)
        {
            if (!IamTesting) { 
                _ownedForms.Add(false);
            }
            else
            {
                _ownedForms.Add(true);
            }
        }
	}

	public bool OwnsForm(int i)
	{
		return _ownedForms [i];
	}
	
	public void BuyCat()
	{
		if (UIController.instance.GetCoins () >= 10) {
			UIController.instance.SpendCoins (10);
			_ownedForms.Insert (1, true);
		}
	}

	public void BuyDog()
	{
		if (UIController.instance.GetCoins () >= 10) {
			UIController.instance.SpendCoins (10);
			_ownedForms.Insert (2, true);
		}
	}

	public void BuyScissor()
	{
		if (UIController.instance.GetCoins () >= 10) {
			UIController.instance.SpendCoins (10);
			_ownedForms.Insert (3, true);
		}
	}
	public void BuyGas()
	{
		if (UIController.instance.GetCoins () >= 10) {
			UIController.instance.SpendCoins (10);
			_ownedForms.Insert (4, true);
		}
	}

	public void BuySword()
	{
		if (UIController.instance.GetCoins () >= 10) {
			UIController.instance.SpendCoins (10);
			_ownedForms.Insert (5, true);
		}
	}

	public void BuyLead()
	{
		if (UIController.instance.GetCoins () >= 10) {
			UIController.instance.SpendCoins (10);
			_ownedForms.Insert (6, true);
		}
	}


}
