﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnExit : MonoBehaviour {

	
    void OnTriggerExit2D (Collider2D other)
    {
		//UIController handles what happens when player dies
		if (other.tag == "Player") {
			UIController.instance.GameOver ("Slow");
		}
    }
}
