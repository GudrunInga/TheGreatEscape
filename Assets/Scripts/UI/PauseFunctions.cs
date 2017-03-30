using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunctions : MonoBehaviour {

	private bool _paused;
	// Use this for initialization
	void Start () {
		_paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Pause/Unpause the game
		if (Input.GetKeyDown (KeyCode.Escape) && UIController.instance.GameActive()) {
			if (_paused) {
				UIController.instance.TurnOn (true);
			} else {
                UIController.instance.TurnOn (false);
                Time.timeScale = 0;
			}
			_paused = !_paused;
		}

		//Activate the pause menu
		if (_paused) {
			UIController.instance.pauseMenu.SetActive (true);
		} 
		//Deactivate the pause menu
		else if (!_paused && UIController.instance.pauseMenu != null) {
			UIController.instance.pauseMenu.SetActive (false);
		}
	}

	public void UnPause()
	{
		_paused = !_paused;
		UIController.instance.TurnOn (true);
	}
	public void ResetPause()
	{
		_paused = false;
		UIController.instance.pauseMenu.SetActive (false);
	}

    public void SetPause()
    {
        _paused = !_paused;
    }
}
