using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveright : MonoBehaviour {

	
    public float speed;
    private Transform trans;
	// Update is called once per frame
    void Start() {
        trans = GetComponent<Transform>();
    }

	void Update () {
		
        Vector3 movement = new Vector3(speed * Time.deltaTime,0,0);
        trans.position = trans.position + movement;
	}
}
