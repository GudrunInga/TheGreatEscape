using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour {

	public static UIController instance;
	void Awake()
	{
		instance = this;
	}

    //Temporary variable for testing purposes (game over ui)
    public int tempScore;
    //GameOverCanvas
    public GameObject gameOverMenu;

    public bool paused;
    //PauseMenuCanvas

	public GameObject pauseMenu;
    //MainCamera SpotLight
	public Light spotLight;
	public Light spotLight1;

	// Use this for initialization
	void Start () {
		paused = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
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
		if (paused) {
			paused = !paused;
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
		paused = !paused;
		spotLight.enabled = true;
		spotLight1.enabled = true;
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void GameOver(string deathReason)
	{
		if (deathReason == "Spike") {
			Debug.Log ("Pointy things are bad for balloons");
		} 
		else if (deathReason == "Slow") {
			Debug.Log ("You were way too slow");
		}

        spotLight.enabled = false;
		spotLight1.enabled = false;
        Time.timeScale = 0;

	}
}
