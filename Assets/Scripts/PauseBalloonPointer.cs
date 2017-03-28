using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBalloonPointer : MonoBehaviour {

	public Animator pauseSelect;
	int position;
	int lastPosition;

	// Use this for initialization
	void Start() {
		position = lastPosition = 4;
	}

	// Update is called once per frame
	void Update() {
		if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && position != 0) {
			position--;
			//Debug.Log(position);
		}
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && position != 4) {
			position++;
			//Debug.Log(position);
		}

		if (lastPosition != position) {
			switch (position) {
				case 4:
					AnimTrigger("PContinue");
					break;
				case 3:
					AnimTrigger("PRestart");
					break;
				case 2:
					AnimTrigger("PStore");
					break;
				case 1:
					AnimTrigger("PQuit");
					break;
				case 0:
					AnimTrigger("PExit");
					break;
			}
			lastPosition = position;
		}
	}

	void AnimTrigger(string triggerName) {
		foreach (AnimatorControllerParameter p in pauseSelect.parameters)
			if (p.type == AnimatorControllerParameterType.Trigger)
				pauseSelect.ResetTrigger(p.name);
		pauseSelect.SetTrigger(triggerName);
	}

	private void OnDisable() {
		position = 4;
	}
}
