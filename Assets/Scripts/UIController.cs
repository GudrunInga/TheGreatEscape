using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {



	/*Begin Game Over Menu*/
	//Text box for reason of death
	private Text _killReason;
	//Text box for final score
	private Text _finalScore;
	//Game Over, u dead
	private bool _gameOver;

	//GameOverCanvas
	public GameObject gameOverMenu;
	/*End Game Over Menu*/

	/*Begin Pause Menu*/
	private bool _paused;

	//PauseMenuCanvas
	public GameObject pauseMenu;
	/*End Pause Menu*/

	/*Begin Game Play UI*/
	//Text box to show coins;
	private Text _coinsCollectedText;
	//Image of the coin
	private Image _image;
	//Text box for the score
	//private Text _timeText;

	//ScoreCanvas
	public GameObject scoreMenu;
	/*Rotate the coin*/
	public float rotateCoinSpeed;
	/*End Game Play UI*/

	//Time player has been alive
	private float _timeAlive;
	//Coins Collected, when player collides with coins the Coin_Rotate script calls an instance of this
	private int _coins;

    //MainCamera SpotLights, turned off when paused or game over
	public Light spotLight;
	public Light spotLight1;

	public static UIController instance;
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		_paused = false;
		_gameOver = false;
		if(scoreMenu != null){
			scoreMenu.SetActive (true);
			_timeAlive = Time.timeSinceLevelLoad;
			_coinsCollectedText = scoreMenu.transform.Find ("CoinsText").GetComponent<Text> ();
			//_timeText = scoreMenu.GetComponentInChildren<Text> ();// transform.Find ("Text");
			_image = scoreMenu.transform.Find("ImageParent").transform.Find("Image").GetComponent<Image>();// transform.Find("Image").GetComponent<RawImage>();
		}
		_coins = 0;
	}
	// Update is called once per frame
	void Update () {
		if (scoreMenu != null) {
			//Rotate the coin image
			_image.transform.Rotate (0, rotateCoinSpeed * Time.deltaTime, 0);
			//Show number of coins on screen
			_coinsCollectedText.text = _coins.ToString ();
			//Show time on screen
			//_timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString ("0.0");

			//Pause/Unpause the game
			if (Input.GetKeyDown (KeyCode.Escape) && !_gameOver) {
				if (_paused) {
					Time.timeScale = 1;
					spotLight.enabled = true;
					spotLight1.enabled = true;
				} else {
					Time.timeScale = 0;
					spotLight.enabled = false;
					spotLight1.enabled = false;
				}
				//Debug.Log("Setting TimeScale to " + Time.timeScale);
				_paused = !_paused;
			}

			//Activate the pause menu
			if (_paused) {
				//Time.timeScale = 0;
				pauseMenu.SetActive (true);
			} 
			//Deactivate the pause menu
			else if (!_paused && pauseMenu != null) {
				pauseMenu.SetActive (false);
			}
		}
	}
	//Should be 1 to load Escape scene
	//Menu needs to be first in build order (0)
	//Escape second (1)
	public void LoadScene(int i)
	{
		if (_paused || _gameOver) {
			_paused = false;
			_gameOver = false;
			spotLight.enabled = true;
			spotLight1.enabled = true;
			Time.timeScale = 1;
		}
		SceneManager.LoadScene (i);
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

	public void UnPause()
	{
		_paused = !_paused;
		spotLight.enabled = true;
		spotLight1.enabled = true;
		Time.timeScale = 1;
	}

	public void RestartScene()
	{
		_paused = false;
		_gameOver = false;
		spotLight.enabled = true;
		spotLight1.enabled = true;
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void GameOver(string deathReason)
	{
		_timeAlive = Time.timeSinceLevelLoad;
		//Debug.Log ("Time " + _timeAlive);
		_gameOver = true;
		_killReason = gameOverMenu.transform.Find ("Panel").transform.Find("DeathReason").gameObject.GetComponent<Text>();
		_finalScore = gameOverMenu.transform.Find ("Panel").transform.Find ("FinalScore").gameObject.GetComponent<Text> ();
		_finalScore.text = _coins.ToString ();
		if (deathReason == "Spike") {
			_killReason.text = "Pointy things are bad for balloons";
		} 
		else if (deathReason == "Slow") {
			_killReason.text = "You were way too slow";
		}
		gameOverMenu.SetActive (true);
        spotLight.enabled = false;
		spotLight1.enabled = false;
		//Stop the game
        Time.timeScale = 0;
		scoreMenu.SetActive (false);
	}

	public int GetCoins()
	{
		return _coins;
	}
	public void SetCoins()
	{
		++_coins;
	}
}
