using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	/*Begin Customize Menu*/
	public GameObject customizeMenu;
	public Text speedText;
	public Button increaseSpeedButton;
	public Button decreaseSpeedButton;
	private int _speedLevel;
	private int _currentSpeedLevel;
	/*End Customize Menu*/

	/*Begin Store Menu*/
	public GameObject storeMenu;
	//Needed for changing values such as mass (to increase speed)
	public GameObject player;
	/*End Store Menu*/

	/*Begin Game Over Menu*/
	//Text box for reason of death
	private Text _killReason;
	//Text box for final score
	private Text _finalScore;
	//Game Over, u dead
	private bool _gameOver;
    //death by laundry
    public bool laundry;
    //death by steel
    public bool steel;
	//For death animation
	private bool dead;
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
		_coins = 0;
		//death by laundry
		laundry = false;
		steel = false;
		if (SceneManager.GetActiveScene ().name != "MainMenu"){
			dead = false;

			if (scoreMenu != null) {
				scoreMenu.SetActive (true);
				_timeAlive = Time.timeSinceLevelLoad;
				_coinsCollectedText = scoreMenu.transform.Find ("CoinsText").GetComponent<Text> ();
				//_timeText = scoreMenu.GetComponentInChildren<Text> ();// transform.Find ("Text");
				_image = scoreMenu.transform.Find ("ImageParent").transform.Find ("Image").GetComponent<Image> ();// transform.Find("Image").GetComponent<RawImage>();
			}

			//Customize speed
			_currentSpeedLevel = 0;
			_speedLevel = 0;
			speedText.text = _currentSpeedLevel.ToString ();
			//disable plus sign
			disableButton (true);
			//disable minus sign
			disableButton (false);

		}
	}
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name != "MainMenu") {
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
	}
	//Should be 1 to load Escape scene
	//Menu needs to be first in build order (0)
	//Escape second (1)
	public void LoadScene(int i)
	{
		if (_paused || _gameOver) {
			_paused = false;
			_gameOver = false;
			dead = false;
			spotLight.enabled = true;
			spotLight1.enabled = true;
			Time.timeScale = 1;
		}
        laundry = false;
        steel = false;
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
        laundry = false;
        steel = false;
		spotLight.enabled = true;
		spotLight1.enabled = true;
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void GameOver(string deathReason)
	{
		if (!dead) { 
		_timeAlive = Time.timeSinceLevelLoad;
		//Debug.Log ("Time " + _timeAlive);
		_gameOver = true;
		_killReason = gameOverMenu.transform.Find ("Panel").transform.Find("DeathReason").gameObject.GetComponent<Text>();
		_finalScore = gameOverMenu.transform.Find ("Panel").transform.Find ("FinalScore").gameObject.GetComponent<Text> ();
		_finalScore.text += _coins.ToString ();

        DeathMessage(deathReason);

		player.GetComponent<BalloonController>().pop();
		StartCoroutine("CoGameOver");
		dead = true;
		}
		//_coinsCollectedText.color = Color.black;
		//scoreMenu.SetActive (false);
	}
	private IEnumerator CoGameOver()
	{
		yield return new WaitForSeconds(1); 
		gameOverMenu.SetActive(true);
		spotLight.enabled = false;
		spotLight1.enabled = false;
		//Stop the game
		Time.timeScale = 0;	 
	}

    private void DeathMessage(string deathReason)
    {
        if (laundry)
        {
            _killReason.text = "Death by laundry....that is bad, maybe you need to cut through it?";
            laundry = false;
        }
        if (steel)
        {
            _killReason.text = "Steel is heavy....who knew?";
            steel = true;
        }
        else if (deathReason == "Spike")
        {
            _killReason.text = "Pointy things are bad for balloons, if only you could get better controls";
        }
        else if (deathReason == "Slow")
        {
            _killReason.text = "You were too slow, maybe go faster next time, if only you could increase your speed";
        }
        else if(deathReason == "Bird")
        {
            _killReason.text = "Birds ain't scared of you, what are birds scared off?";
        }
    }

	public int GetCoins()
	{
		return _coins;
	}
	public void SetCoins()
	{
		++_coins;
	}

	//Store Menu and Customize menu
	public void IncreaseSpeed()
	{
		if (_coins > 0 && storeMenu.activeSelf) {
			--_coins;
			player.GetComponent<Rigidbody2D> ().mass -= 0.1f;
			_speedLevel++;
			_currentSpeedLevel++;
			speedText.text = _currentSpeedLevel.ToString();
			if (_currentSpeedLevel == _speedLevel) {
				disableButton (true);
			}
			if (_currentSpeedLevel > 0) {
				enableButton (false);
			}
			if (_currentSpeedLevel < _speedLevel) {
				enableButton (true);
			}
		}

		else if(customizeMenu.activeSelf){
			if (_currentSpeedLevel < _speedLevel) {
				_currentSpeedLevel++;
				player.GetComponent<Rigidbody2D> ().mass -= 0.1f;
				speedText.text = _currentSpeedLevel.ToString ();
			}
			if (_currentSpeedLevel == _speedLevel) {
				disableButton (true);
			}
			if (_currentSpeedLevel == 0) {
				disableButton (false);
			}
			if (_currentSpeedLevel > 0) {
				enableButton (false);
			}
			if (_currentSpeedLevel < _speedLevel) {
				enableButton (true);
			}
		}
	}

	public void decreaseSpeed()
	{
		if (_currentSpeedLevel > 0) {
			_currentSpeedLevel--;
			player.GetComponent<Rigidbody2D> ().mass += 0.1f;
			speedText.text = _currentSpeedLevel.ToString ();
		}
		if (_currentSpeedLevel == 0) {
			disableButton (false);
			enableButton (true);
		}
	}

	void disableButton(bool plusSign)
	{
		if (plusSign) {
			increaseSpeedButton.enabled = false;
			increaseSpeedButton.GetComponent<Image> ().enabled = false;
			increaseSpeedButton.GetComponentInChildren<Text> ().text = "";
		} 
		else {
			decreaseSpeedButton.enabled = false;
			decreaseSpeedButton.GetComponent<Image> ().enabled = false;
			decreaseSpeedButton.GetComponentInChildren<Text> ().text = "";
		}
	}
	void enableButton(bool plusSign)
	{
		if (plusSign) {
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

}
