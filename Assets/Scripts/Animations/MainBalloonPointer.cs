﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBalloonPointer : MonoBehaviour {

	public Animator menuSelect;
	int position, lastPosition;

	// Use this for initialization
	void Start () {
		position = lastPosition = 5;
	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && position != 0) {
			position--;
			//Debug.Log(position);
		}
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && position != 5) {
			position++;
			//Debug.Log(position);
		}

		if (lastPosition != position) {
			switch (position) {
				case 5:
					AnimTrigger("Start");
					break;
				case 4:
					AnimTrigger("Controls");
					break;
				case 3:
					AnimTrigger("Store");
					break;
				case 2:
					AnimTrigger("High");
					break;
				case 1:
					AnimTrigger("Quit");
					break;
				case 0:
					AnimTrigger("Reset");
					break;
			}
			lastPosition = position;
		}
	}

	void AnimTrigger(string triggerName) {
		foreach (AnimatorControllerParameter p in menuSelect.parameters)
			if (p.type == AnimatorControllerParameterType.Trigger)
				menuSelect.ResetTrigger(p.name);
		menuSelect.SetTrigger(triggerName);
	}

	private void OnDisable() {
		position = 4;
	}
}
