using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour {
	
	public bool paused;
	public GameObject pauseMenu;
	public Light spotLight;
	// Use this for initialization
	void Start () {
		paused = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("Escape");
			if(paused) {
				Time.timeScale = 1;
				spotLight.enabled = true;
			}
			else {
				Time.timeScale = 0;
				spotLight.enabled = false;
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
		Time.timeScale = 1;
	}

	public void RestartScene()
	{
		paused = !paused;
		spotLight.enabled = true;
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
