using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBalloonPointer : MonoBehaviour {

	public Animator gameOverSelect;
	int position, lastPosition;

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
					AnimTrigger("GRestart");
					break;
				case 3:
					AnimTrigger("GHelp");
					break;
				case 2:
					AnimTrigger("GStore");
					break;
				case 1:
					AnimTrigger("GMenu");
					break;
				case 0:
					AnimTrigger("GExit");
					break;
			}
			lastPosition = position;
		}
	}

	void AnimTrigger(string triggerName) {
		foreach (AnimatorControllerParameter p in gameOverSelect.parameters)
			if (p.type == AnimatorControllerParameterType.Trigger)
				gameOverSelect.ResetTrigger(p.name);
		gameOverSelect.SetTrigger(triggerName);
	}

	private void OnDisable() {
		position = 4;
	}
}
