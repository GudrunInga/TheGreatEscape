using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	/*Scripts*/
	private GameOverFunctions _gameOverScript;
	//private Store _storeScript;
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

    //MainCamera SpotLights, turned off when paused or game over
	public Light spotLight;
	public Light spotLight1;


	public static UIController instance;
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start() {
		_gameOverScript = gameObject.GetComponent<GameOverFunctions> ();
		//_storeScript = gameObject.GetComponent<Store> ();
		if (_firstRun) {
			_coins = 0;
			Time.timeScale = 0;
			_firstRun = false;
		}
		else {
			mainMenu.SetActive(false);
			Time.timeScale = 1;
		}
		//death by laundry
		laundry = false;
		steel = false;

		_timeAlive = Time.timeSinceLevelLoad;

		/*ScoreMenu*/
		_coinsCollectedText = scoreMenu.transform.Find ("CoinsText").GetComponent<Text> ();
		//_timeText = scoreMenu.GetComponentInChildren<Text> ();// transform.Find ("Text");
		_image = scoreMenu.transform.Find ("ImageParent").transform.Find ("Image").GetComponent<Image> ();
	}
	// Update is called once per frame
	void Update () {
		//Rotate the coin image
		_image.transform.Rotate (0, rotateCoinSpeed * Time.deltaTime, 0);
		//Show number of coins on screen
		_coinsCollectedText.text = _coins.ToString ();
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
		_gameOverScript.ResetScene (false, false);
        laundry = false;
        steel = false;
		TurnOn (true);
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
		_gameOverScript.GameOver (deathReason);
	}

	/*Moneys...all the moneys*/
	public int GetCoins()
	{
		return _coins;
	}
	public void AddCoins()
	{
		++_coins;
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
}
