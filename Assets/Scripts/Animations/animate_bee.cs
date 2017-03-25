using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate_bee : MonoBehaviour {
	public float animationSpeed; 
	private animateData left_wing;
	private animateData right_wing;
	public Vector3 up;
	private bool active;

	// Use this for initialization
	void Start()
	{
		active = true;				
		left_wing = new animateData();
		right_wing = new animateData();			 

		left_wing.model = transform.Find("Body/lwing");
		left_wing.direction = 1;
		left_wing.animationSpeed = animationSpeed;
		left_wing.progress = 0;
		left_wing.to = Quaternion.Euler(-10, 0, 0);
		left_wing.from = Quaternion.Euler(90, 0, 0);

		right_wing.model = transform.Find("Body/rwing");
		right_wing.direction = 1;
		right_wing.animationSpeed = animationSpeed;
		right_wing.progress = 0;
		right_wing.to = Quaternion.Euler(10, 0, 0);
		right_wing.from = Quaternion.Euler(-90, 0, 0);
		transform.GetChild(0).gameObject.SetActive(true);

	}

	// Update is called once per frame
	void Update()
	{
		if (!active)
		{
			return;
		}				 
		left_wing.animate();
		right_wing.animate();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("MainCamera"))
		{
			active = true;
			transform.GetChild(0).gameObject.SetActive(true);
		}
		if (other.gameObject.CompareTag("Player"))
		{
			UIController.instance.GameOver("Bee");
		}
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
			model.localRotation = Quaternion.Lerp(model.localRotation, to, Time.deltaTime * animationSpeed);
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
