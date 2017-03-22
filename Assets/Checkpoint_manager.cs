using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_manager : MonoBehaviour {

	public Moveright moverScript;
	public float CheckpointDistance;
	private float DistanceTravelled;
	private int level;
	// Use this for initialization
	void Start () {
		DistanceTravelled = 0;
		UIController inst = UIController.instance;			   
		level = inst.getLevel();
		if(level > 1) { 
			for(int i = 1; i < level; i++)
			{
				CheckpointDistance = Mathf.CeilToInt(CheckpointDistance * (1 + level / 20));
				moverScript.add_accel(moverScript.acceleration); 
			}
			float lastpos = transform.position.x;
			Vector3 newvec= new Vector3(inst.get_respawn_x(), transform.position.y, transform.position.z);
			transform.position = newvec;
			newvec = new Vector3(inst.player.transform.position.x + (transform.position.x - lastpos), inst.player.transform.position.y, inst.player.transform.position.z);
			inst.player.transform.position = newvec;
		}
	}
	
	// Update is called once per frame
	void Update () {
		DistanceTravelled += moverScript.speed * Time.deltaTime;
		if (DistanceTravelled >= CheckpointDistance)
		{
			UIController inst = UIController.instance;
			DistanceTravelled = 0;
			CheckpointDistance = Mathf.CeilToInt(CheckpointDistance * ( 1 + level/20));
			moverScript.add_accel(moverScript.acceleration);
			level++;
			inst.setLevel(level, transform.position.x);
			Debug.Log("starting level " + level);
		}
	}
}
