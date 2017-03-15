using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate_bird : MonoBehaviour {
	public float animationSpeed;
	private animateData head;
	private animateData left_wing;
	private animateData right_wing;	

	// Use this for initialization
	void Start () {
		head = new animateData();
		left_wing = new animateData();
		right_wing = new animateData();

		head.model = transform.Find("Body/head");
		head.direction = 1;
		head.animationSpeed = animationSpeed / 3;
		head.progress = 0;
		head.to = Quaternion.Euler(-30, -10, -10);
		head.from = Quaternion.Euler(30, 10, 10);


		left_wing.model = transform.Find("Body/lWing");
		left_wing.direction = 1;
		left_wing.animationSpeed = animationSpeed;
		left_wing.progress = 0;
		left_wing.to = Quaternion.Euler(-60, 10, 0);
		left_wing.from = Quaternion.Euler(+65, -10, 0);

		right_wing.model = transform.Find("Body/rWing");
		right_wing.direction = 1;
		right_wing.animationSpeed = animationSpeed;
		right_wing.progress = 0;
		right_wing.to = Quaternion.Euler(+65, -10, 0);
		right_wing.from = Quaternion.Euler(-60, +10, 0);

	}
	
	// Update is called once per frame
	void Update () {
		head.animate(); 
		left_wing.animate();
		right_wing.animate();
	}

	private class animateData
	{
		public Transform model;
		public float progress;
		public Quaternion to;
		public Quaternion from;
		public float animationSpeed;
		public int direction;
		public void animate()
		{
			model.rotation = Quaternion.Lerp(model.rotation, to, Time.deltaTime * animationSpeed);
			progress += Time.deltaTime * animationSpeed * direction;
			if (progress > 1 || progress < 0)
			{
				progress = Mathf.Clamp(progress, 0, 1);
				Quaternion temp = from;
				from = to;
				to = temp;
				direction *= -1;
			}
		}
	};
}

