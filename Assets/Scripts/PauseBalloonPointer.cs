using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBalloonPointer : MonoBehaviour {

	public Animator pauseSelect;
	int position;

	// Use this for initialization
	void Start() {
		position = 4;
	}

	// Update is called once per frame
	void Update() {
		if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && position != 0) {
			position--;
			Debug.Log(position);
		}
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && position != 4) {
			position++;
			Debug.Log(position);
		}
		pauseSelect.ResetTrigger("PContinue");
		pauseSelect.ResetTrigger("PRestart");
		pauseSelect.ResetTrigger("PStore");
		pauseSelect.ResetTrigger("PQuit");
		pauseSelect.ResetTrigger("PExit");
		switch (position) {
			case 4:
				pauseSelect.SetTrigger("PContinue");
				break;
			case 3:
				pauseSelect.SetTrigger("PRestart");
				break;
			case 2:
				pauseSelect.SetTrigger("PStore");
				break;
			case 1:
				pauseSelect.SetTrigger("PQuit");
				break;
			case 0:
				pauseSelect.SetTrigger("PExit");
				break;
		}
	}

	private void OnDisable() {
		position = 4;
	}
}
