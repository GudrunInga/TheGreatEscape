using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public static UIController instance;
	void Awake()
	{
		instance = this;
	}

	//Text box for reason of death
	private Text _killReason;
	//Text box to show coins;
	public Text coins;
	//Text box for the score
	public Text score;
	//Text box for final score
	private Text _finalScore;
	//Game Over, u dead
	private bool gameOver;
	//Time player has been alive
	private float _timeAlive;
	//Temporary variable (for now) for testing purposes (game over ui)
    public int moneyCoins;
    //GameOverCanvas
    public GameObject gameOverMenu;

    public bool paused;
    //PauseMenuCanvas

	public GameObject pauseMenu;

    //MainCamera SpotLights
	public Light spotLight;
	public Light spotLight1;

	// Use this for initialization
	void Start () {
		paused = false;
		gameOver = false;
		_timeAlive = Time.timeSinceLevelLoad;


		//Debug.Log ("Start time " + _timeAlive.ToString("0.00"));
	}
	// Update is called once per frame
	void Update () {
		coins.text = moneyCoins.ToString();
		score.text = "Time: " + Time.timeSinceLevelLoad.ToString ("0.0");
		if (Input.GetKeyDown (KeyCode.Escape) && !gameOver) {
			if(paused) {
				Time.timeScale = 1;
				spotLight.enabled = true;
				spotLight1.enabled = true;
			}
			else {
				Time.timeScale = 0;
				spotLight.enabled = false;
				spotLight1.enabled = false;
			}
			//Debug.Log("Setting TimeScale to " + Time.timeScale);
			paused = !paused;
		}

		if (paused) {
			//Time.timeScale = 0;
			pauseMenu.SetActive (true);
		} 
		else if (!paused && pauseMenu != null) {
			pauseMenu.SetActive (false);
		}
	}
	//Should be 1 to load Escape scene
	//Menu needs to be first in build order (0)
	//Escape second (1)
	public void LoadScene(int i)
	{
		if (paused || gameOver) {
			paused = false;
			gameOver = false;
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
		paused = !paused;
		spotLight.enabled = true;
		spotLight1.enabled = true;
		Time.timeScale = 1;
	}

	public void RestartScene()
	{
		paused = false;
		gameOver = false;
		spotLight.enabled = true;
		spotLight1.enabled = true;
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void GameOver(string deathReason)
	{
		_timeAlive = Time.timeSinceLevelLoad;
		//Debug.Log ("Time " + _timeAlive);
		gameOver = true;
		_killReason = gameOverMenu.transform.Find ("Panel").transform.Find("DeathReason").gameObject.GetComponent<Text>();
		_finalScore = gameOverMenu.transform.Find ("Panel").transform.Find ("FinalScore").gameObject.GetComponent<Text> ();
		_finalScore.text += moneyCoins.ToString ();
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
	}
}
