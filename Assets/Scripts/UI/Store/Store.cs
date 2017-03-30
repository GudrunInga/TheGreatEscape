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
	public ShopScrollList shopScrollList;

	private static List<bool> _ownedForms;
	private static bool _firstRun = true;
	private static List<bool> _storeOwnedItems;
	private static List<bool> _ownedHats;

	//To get access decrease increase the acceliration of the camera
	private Moveright moveCameraScript;

	public GameObject contentOfPlayerInfo;

	// Use this for initialization
	void Start () {
		_gravityScript = new AssemblyCSharp.StoreGravity(gravity, buyGravity, currentGravityLevel, increaseGravityButton, decreaseGravityButton);
		_speedScript = new AssemblyCSharp.StoreSpeed (speed, buySpeed, currentSpeedLevel, increaseSpeedButton, decreaseSpeedButton);

		moveCameraScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Moveright> ();
		_cameraAccScript = new AssemblyCSharp.StoreCameraAcceleration (cameraAcc, buyCameraAcc, currentCameraAccLevel, increaseCameraAccButton, decreaseCameraAccButton, moveCameraScript);

		DisableAllButtons ();
		GotMoney ();

		if (_firstRun) {
			_storeOwnedItems = new List<bool> ();
			InitializeStoreItems ();
			InitializeForms ();
			InitializeHats ();

			_firstRun = false;
		} else {
			shopScrollList.RefreshDisplay ();
		}
		InitializeButtons ();

		//Debug.Log ("I am in Store Start " + _firstRun);
	
		//InitializeItems ();
		//Debug.Log ("Owned forms: " + _ownedForms.Count);
		//Debug.Log ("Store Owned Forms: " + _storeOwnedItems.Count);
	}
	void InitializeStoreItems()
	{
		for (int i = 0; i < ItemManager.instance.items.Count; i++) {
			_storeOwnedItems.Add (false);
		}
	}
	void InitializeButtons()
	{
		for (int i = 0; i < _storeOwnedItems.Count; i++) {
			if (_storeOwnedItems [i]) {
				
				var item = ItemManager.instance.items [i];
				//Debug.Log ("Store Item: " + i + " Item: " + item.itemName);
				Button button = item.text.GetComponentInChildren<Button> ();
				item.text.text = "BOUGHT!";
				button.GetComponent<Button> ().interactable = false;
			}
		}
	}

	void InitializeItems()
	{
		for (int i = 0; i < ItemManager.instance.items.Count; i++) {
			if (_storeOwnedItems [i]) {
				switch (i) {
					case 0:
						BuyCat(true);
						break;
					case 1:
						BuyDog (true);
						break;
					case 2:
						BuyScissors (true);
						break;
					case 3:
						BuyGas (true);
						break;
					case 4:
						BuySword (true);
						break;
					case 5:
						BuySteel (true);
						break;
					case 6:
						BuyTopHat (true);
						break;
					case 7:
						BuyBow (true);
						break;
					case 8:
						BuyCap (true);
						break;
				}
			}

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

	void InitializeHats()
	{
		_ownedHats = new List<bool> ();
		for (int i = 0; i < 3; i++) {
			_ownedHats.Add (false);
		}
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
				_storeOwnedItems [i-1] = true;
            }
        }
	}

	public bool OwnsForm(int i)
	{
		return _ownedForms [i];
	}
		
	public void BuyCat(bool buyForFree = false)
	{
		var item = ItemManager.instance.items [0];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 0);
	}

	public void BuyDog(bool buyForFree = false)
	{
		var item = ItemManager.instance.items [1];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 1);
	}

	public void BuyScissors(bool buyForFree = false)
	{
		var item = ItemManager.instance.items [2];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 2);
	}

	public void BuyGas(bool buyForFree = false)
	{
		var item = ItemManager.instance.items [3];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 3);
	}

	public void BuySword(bool buyForFree = false)
	{
//		Debug.Log ("Size of this bloody list is: " + ItemManager.instance.items.Count);
		var item = ItemManager.instance.items [4];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 4);
	}

	public void BuySteel(bool buyForFree = false)
	{
		var item = ItemManager.instance.items [5];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 5);
	}

	public void BuyTopHat(bool buyForFree = false)
	{
		var item = ItemManager.instance.items [6];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 6);
	}

	public void BuyBow(bool buyForFree = false)
	{
		var item = ItemManager.instance.items [7];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 7);
	}

	public void BuyCap(bool buyForFree = false)
	{
		var item = ItemManager.instance.items [8];
		BuyItem (item.text, buyForFree, item.cost, item.icon, item.itemName, 8);
	}

	void BuyItem (Text text, bool buyForFree, int cost, Sprite sprite, string itemName, int formID)
	{
		if ((UIController.instance.GetCoins () >= cost || IamTesting)) {
			if (!buyForFree) {
				UIController.instance.SpendCoins (cost);
			}

			if (formID < 6) {
				_ownedForms [formID + 1] = true;
				if (!_storeOwnedItems [formID]) {
					DisableBuyButton (text, sprite, formID);
					_storeOwnedItems [formID] = true;
				}
			} else {
				_ownedHats [formID - 6] = true;
				if (!_storeOwnedItems [formID]) {
					DisableBuyButton (text, sprite, formID);
					_storeOwnedItems [formID] = true;
				}
			}
		}
		GotMoney();
	}

	void DisableBuyButton (Text text, Sprite sprite, int id)
	{
		text.text = "BOUGHT!";
		//Disable the button
		Button button = text.GetComponentInChildren<Button> ();
		button.GetComponent<Button> ().interactable = false;
		if (sprite.name != "Shield_icon") {
			//Debug.Log ("AddToPlayerinfo" + " Sprite: " + sprite.name + " ID " + id);
			AddToPlayerInfo (sprite, id);
		}
	}

    private void AddToPlayerInfo(Sprite sprite, int id)
    {
		//Debug.Log ("ID " + id);
		ItemBought item = new ItemBought ();
		item.itemID = id;
		item.bought = true;
		shopScrollList.AddItem (item);// .itemList.Add(item);
        shopScrollList.RefreshDisplay();
    }

    public void BuyShield(Text text)
    {
        BalloonController bg = UIController.instance.player.GetComponent<BalloonController>();
        if (bg.hasShield)
        {
            Debug.Log("You already have a fucking shield");
        }
        else if(UIController.instance.GetCoins() >= 5 || IamTesting)
        {
            UIController.instance.SpendCoins(5);
            bg.setShield(true);
            Debug.Log("Shield is here? " + bg.hasShield + " " + bg.getShield());
            text.text = "BOUGHT!";
            //Disable the button
            Button button = text.GetComponentInChildren<Button>();
            button.GetComponent<Button>().interactable = false;
        }
        GotMoney();
    }


    public void SetOwned(int i, bool enabled)
    {
		if (i < 6 && UIController.instance.IsToggleInteractive()) {
			_ownedForms [i + 1] = enabled;
		} else if(i >= 6 && UIController.instance.IsToggleInteractive()){
			_ownedHats [i - 6] = enabled;
		}
    }

    public List<bool> GetOwnedForms()
    {
        return _ownedForms;
    }
	public bool GetOwnedFormByID(int id)
	{
		return _ownedForms [id];
	}
	public bool GetOwnedHatByID(int id)
	{
		return _ownedHats [id - 6];
	}
	public bool GetStoreOwnedItem(int id)
	{
		return _storeOwnedItems [id];
	}

	public List<bool> getStoreOwnedItems()
	{
		return _storeOwnedItems;
	}
	public bool getFirstRun()
	{
		return _firstRun;
	}
}
