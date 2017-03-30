using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {			

	public static bool toggleIsInteractive;

	public void SetToggleInteractive(bool active)
	{
		toggleIsInteractive = active;
	}
	public bool IsToggleInteractive()
	{
		return toggleIsInteractive;
	}
    //For Go back button
    private GameObject _lastActiveMenu;

	/*Scripts*/
	private GameOverFunctions _gameOverScript;
	private Store storeScript;
	private PauseFunctions _pauseScript;  

	/*The Player object*/
	public GameObject player;

	/*Begin Main Menu*/
	public GameObject mainMenu;
	/*End Main Menu*/

	/*Begin Game Over Menu*/
	//death by laundry
	public bool laundry;
	//death by steel
	public bool steel;
	//died?	  
	private bool dead;
	//GameOverCanvas
	public GameObject gameOverMenu;
	/*End Game Over Menu*/

	/*Begin Store Menu*/
	public GameObject storeMenu;
	/*End Store Menu*/

	/*Begin Pause Menu*/
	//PauseMenuCanvas
	public GameObject pauseMenu;
	/*End Pause Menu*/

	/*Begin Game Play UI*/
	//Text box to show coins;
	private Text _coinsCollectedText;
	//Image of the coin
	private Image _image;

	//ScoreCanvas
	public GameObject scoreMenu;
	/*Rotate the coin*/
	public float rotateCoinSpeed;
	/*End Game Play UI*/

	//Time player has been alive
	private float _timeAlive;
	// Max level of player.
	private static int _level = 1;
	// Location of player at level
	private static float _spawn_x = 0;
	//Coins Collected, when player collides with coins the Coin_Rotate script calls an instance of this
	private static int _coins;
	private static bool _firstRun = true;

	private static List<bool> _fancyItems;

    //MainCamera SpotLights, turned off when paused or game over
	public Light spotLight;
	public Light spotLight1;

	private int _tempCoins;


	public static UIController instance;
	void Awake()
	{
		instance = this;
	}
	/*public void SetFancyItem(int id){
		Debug.Log ("SET FANCY ITEM");
		if(storeScript.GetStoreOwnedItem(id+6)){
			_fancyItems [id] = true;
		}
	}*/
	void InitializeFancyItems()
	{
		/*_fancyItems = new List<bool> ();
		for (int i = 0; i < 3; i++) {
			_fancyItems.Add (false);
		}*/
	}
	public bool GetFancyItem(int id)
	{
		return _fancyItems [id];
	}
	//This function is working as expected - correctly
	void CheckFancyItems()
	{
		/*for (int i = 0; i < 3; i++) {
			if (_fancyItems [i]) {
				if (i != 2) {
					var obj = player.transform.GetChild (i).gameObject;
					obj.SetActive (!obj.activeSelf);
				} else {
					var obj = player.transform.GetChild (3).gameObject;
					obj.SetActive (!obj.activeSelf);
				}
			}
		}*/
	}
	// Use this for initialization
	void Start() {
		_gameOverScript = gameObject.GetComponent<GameOverFunctions> ();
        _pauseScript = gameObject.GetComponent<PauseFunctions>();
		storeScript = gameObject.GetComponent<Store> ();
		if (_firstRun) {
			_coins = 0;
			Time.timeScale = 0;
			InitializeFancyItems ();
			_firstRun = false;
			toggleIsInteractive = true;
			save(true);
			load();
		}
		else {
			mainMenu.SetActive(false);
			Time.timeScale = 1;
		}
		//death by laundry
		laundry = false;
		steel = false;
		dead = false;

		CheckFancyItems ();

		_timeAlive = Time.timeSinceLevelLoad;
		_tempCoins = 0;
		/*ScoreMenu*/
		_coinsCollectedText = scoreMenu.transform.Find("Coins").transform.Find ("CoinsText").GetComponent<Text> ();
		//_timeText = scoreMenu.GetComponentInChildren<Text> ();// transform.Find ("Text");
		_image = scoreMenu.transform.Find("Coins").transform.Find ("ImageParent").transform.Find ("Image").GetComponent<Image> ();
	}
	// Update is called once per frame
	void Update () {
		//Rotate the coin image
		_image.transform.Rotate (0, rotateCoinSpeed * Time.deltaTime, 0);
		//Show number of coins on screen
		if (GameActive ()) {
			_coinsCollectedText.text = _tempCoins.ToString ();
		} else {
			_coinsCollectedText.text = _coins.ToString ();
		}
		//Show time on screen
		//_timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString ("0.0");
	}

	/*Helper function to turn off/on lights and game*/
	public void TurnOn (bool turnOn)
	{
		if (turnOn) {
			Time.timeScale = 1;
			spotLight.enabled = true;
			spotLight1.enabled = true;
		} 
		else {
			Time.timeScale = 0;
			spotLight.enabled = false;
			spotLight1.enabled = false;
		}
	}
		
	/*Called when Start button in any menu is "clicked"*/
	public void RestartScene()
	{
		//Debug.Log ("LET'S RESTART THE SCENE!");
		_gameOverScript.ResetScene (false, false);
        laundry = false;
        steel = false;
		TurnOn (true);
		save();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	/*Is the game active (no menus)*/
	public bool GameActive()
	{
		return (!_gameOverScript.GetGameOver () && !mainMenu.activeSelf && !storeMenu.activeSelf);
	}

	/*Player has died*/
	public void GameOver(string deathReason)
	{
		if(dead)
		{
			return;
		}
		BalloonController bs = player.GetComponent<BalloonController>();
		if (bs.getShield())
		{
            Debug.Log("Shield saved you");
			bs.setShield(false);
			bs.SafeTime = 4;
			return;
		}
		dead = true;	  
		_coins += _tempCoins;
		_gameOverScript.GameOver (deathReason);
		save();
	}

	/*Moneys...all the moneys*/
	public int GetCoins()
	{
		return _coins;
	}
	//Coins collected during gameplay
	public int GetCoinsCollected()
	{
		return _tempCoins;
	}
	public void AddCoins()
	{
		++_tempCoins;
	}
	public void SpendCoins()
	{
		--_coins;
	}
	public void SpendCoins(int i)
	{
		_coins -= i;
	}

	//Check if player owns the form he is going to change into
	public bool ownsForm(int i)
	{
		return gameObject.GetComponent<Store> ().OwnsForm (i);
	}
		
	public void setLevel(int level, float x)
	{
		Debug.Log("Setting level " + _level + " -> " + level);
		if(_level < level)
		{
			Debug.Log("Set " + level + " at " + x);
			_level = level;
			_spawn_x = x;
		}
	}
	public int getLevel()
	{
		return _level;
	}
	public float get_respawn_x()
	{
		return _spawn_x;
	}

	//Call if quitting the application
	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}

    public void SetActiveCanvas(GameObject canvas)
    {
        _lastActiveMenu = canvas;   
    }

    public void SetLastActiveCanvas()
    {
        if(_lastActiveMenu.name == "PauseMenuCanvas")
        {
            _pauseScript.SetPause();
            spotLight.enabled = true;
            spotLight1.enabled = true;
        }
        _lastActiveMenu.SetActive(true);
    }

    public Store GetStoreScript()
    {
        return storeScript;
    }

	public void SetActiveFancyStuff(int id, bool enabled){
		/*Debug.Log ("Set active fancy stuff");
		var topHat = player.transform.GetChild (0).gameObject;
		var bow = player.transform.GetChild (1).gameObject;
		var cap = player.transform.GetChild (3).gameObject;

		var hatobj = GameObject.FindGameObjectWithTag ("ToggleHat");
		var bowobj = GameObject.FindGameObjectWithTag ("ToggleBow");
		var capobj = GameObject.FindGameObjectWithTag ("ToggleCap");
		//Debug.Log ("ID: " + id + " enabled: " + enabled);
		//topHat
		if (id == 0) {
			if (enabled) {
				if(bowobj != null && bowobj.GetComponent<Toggle>().isOn){
					//Debug.Log ("I am toggling bow off and hat on");
					bowobj.GetComponent<Toggle> ().interactable = false;
					bowobj.GetComponent<Toggle> ().isOn = false;
					bowobj.GetComponent<Toggle> ().interactable = true;
				}
				if (bow.activeSelf) {
					//var obj = GameObject.FindGameObjectWithTag ("ToggleBow"); //.isOn = false;

					bow.SetActive (false);
					_fancyItems [1] = false;
				} 
				if(capobj != null && capobj.GetComponent<Toggle>().isOn){
					//Debug.Log ("I am toggling cap off and hat on");
					capobj.GetComponent<Toggle> ().interactable = false;
					capobj.GetComponent<Toggle> ().isOn = false;
					capobj.GetComponent<Toggle> ().interactable = true;
				}
				if (cap.activeSelf) {
					//var obj = GameObject.FindGameObjectWithTag ("ToggleCap"); //.isOn = false;

					cap.SetActive (false);
					_fancyItems [2] = false;
				}
			}
			topHat.SetActive (enabled);
			_fancyItems [id] = enabled;
		}
		//bow
		else if (id == 1) {
			if (enabled) {
				if(hatobj != null && hatobj.GetComponent<Toggle>().isOn){
					//Debug.Log ("I am toggling hat off and bow on");
					hatobj.GetComponent<Toggle> ().interactable = false;
					hatobj.GetComponent<Toggle> ().isOn = false;
					hatobj.GetComponent<Toggle> ().interactable = true;
				}
				if (topHat.activeSelf) {
					//var obj = GameObject.FindGameObjectWithTag ("ToggleHat");

					topHat.SetActive (false);
					_fancyItems [0] = false;
				} 
				if (capobj != null && capobj.GetComponent<Toggle>().isOn) {
					//Debug.Log ("I am toggling cap off and bow on");
					capobj.GetComponent<Toggle> ().interactable = false;
					capobj.GetComponent<Toggle> ().isOn = false;
					capobj.GetComponent<Toggle> ().interactable = true;
				}
				if (cap.activeSelf ) {
					//var obj = GameObject.FindGameObjectWithTag ("ToggleCap");

					cap.SetActive (false);
					_fancyItems [2] = false;
				}
			}
			bow.SetActive (enabled);
			_fancyItems [id] = enabled;
		}
		//cap
		else if (id == 2) {
			if (enabled) {
				if(hatobj != null && hatobj.GetComponent<Toggle>().isOn){
					//Debug.Log ("I am toggling hat off and cap on");
					hatobj.GetComponent<Toggle> ().interactable = false;
					hatobj.GetComponent<Toggle> ().isOn = false;
					hatobj.GetComponent<Toggle> ().interactable = true;
				}
				if (topHat.activeSelf) {
					topHat.SetActive (false);
					_fancyItems [0] = false;
				} 
					
				if(bowobj != null && bowobj.GetComponent<Toggle>().isOn){
					//Debug.Log ("I am toggling bow off and cap on");
					bowobj.GetComponent<Toggle> ().interactable = false;
					bowobj.GetComponent<Toggle> ().isOn = false;
					bowobj.GetComponent<Toggle> ().interactable = true;
				}
				if (bow.activeSelf) {
					bow.SetActive (false);
					_fancyItems [1] = false;
				}
			}
			cap.SetActive (enabled);
			_fancyItems [id] = enabled;
		}*/
	}

	public void save(bool firstSave = false)
	{
		Store mystore = gameObject.GetComponent<Store>();
		SaveFile S = new SaveFile();
		S.level = _level;
		S.spawn = _spawn_x;
		S.firstRun = _firstRun;
		S.coins = _coins;
		S.gravity = mystore.gravity;
		S.speed = mystore.speed;
		S.cameraAcc = mystore.cameraAcc;
		S.ownedForms = mystore.GetOwnedForms();
		S.ownedItems = mystore.getStoreOwnedItems();
		S.storeFirstRun = mystore.getFirstRun();
		string Json = JsonUtility.ToJson(S);

		string path = Application.persistentDataPath + "/Progress.dat";
		if (firstSave){
			path = Application.persistentDataPath + "/Def.dat";
		}
		if (firstSave)
		{
			Debug.Log(path);
			Debug.Log(Json);
			System.IO.File.WriteAllText(path, Json);
		}
	}

	public void load(bool reset = false)
	{
		string path = Application.persistentDataPath + "/Progress.dat";
		if (reset)
		{
			path = Application.persistentDataPath + "/Def.dat";
		}
		string[] data = System.IO.File.ReadAllLines(path);
		Store mystore = gameObject.GetComponent<Store>();
		foreach (String entry in data)
		{
			SaveFile S = JsonUtility.FromJson<SaveFile>(entry);
			Debug.Log("Loaded " + entry);

			_level = S.level;
			_spawn_x = S.spawn;
			_firstRun = S.firstRun;
			_coins = S.coins;

			mystore.gravity = S.gravity;
			mystore.speed = S.speed;
			mystore.cameraAcc = S.cameraAcc;
			mystore.SetOwnedLists(S.ownedForms, S.ownedItems);
			mystore.setFirst(S.storeFirstRun);		
		}
		if (reset)
		{
			save();
			Debug.Log("Save file reset."); 
		}
		else { 
			Debug.Log("Game Loaded.");
		}
}
		   
	[Serializable]
	private class SaveFile
	{

		public int level;
		public float spawn;
		public bool firstRun;
		public int coins;
		//store
		public float gravity;
		public float speed;
		public float cameraAcc;
		public List<bool> ownedForms;
		public List<bool> ownedItems;	   
		public bool storeFirstRun;
	}
}
