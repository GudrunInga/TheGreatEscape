using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBalloonPointer : MonoBehaviour {

	public Animator menuSelect;
	int position;

	// Use this for initialization
	void Start () {
		position = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && position != 0) {
			position--;
			Debug.Log(position);
		}
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && position != 3) {
			position++;
			Debug.Log(position);
		}
		menuSelect.ResetTrigger("Start");
		menuSelect.ResetTrigger("Controls");
		menuSelect.ResetTrigger("Store");
		menuSelect.ResetTrigger("Quit");
		switch (position) {
			case 3:
				menuSelect.SetTrigger("Start");
				break;
			case 2:
				menuSelect.SetTrigger("Controls");
				break;
			case 1:
				menuSelect.SetTrigger("Store");
				break;
			case 0:
				menuSelect.SetTrigger("Quit");
				break;
		}
	}

	private void OnDisable() {
		position = 3;
	}
}
