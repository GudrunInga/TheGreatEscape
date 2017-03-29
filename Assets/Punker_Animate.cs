using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punker_Animate : MonoBehaviour {
	public float animationSpeed;
	private animateData head;
	private animateData Guitar;		 
	private bool active;
	public bool startActive = false;

	// Use this for initialization
	void Start()
	{
		active = startActive;
		head = new animateData();
		Guitar = new animateData();			

		head.model = transform.Find("Body/Head");
		head.direction = 1;
		head.animationSpeed = animationSpeed ;
		head.progress = 0;
		head.to = Quaternion.Euler(-35, -20, 0);
		head.from = Quaternion.Euler(30, 20, 5);


		Guitar.model = transform.Find("Body/Guitar");
		Guitar.direction = 1;
		Guitar.animationSpeed = animationSpeed/1.25f;
		Guitar.progress = 0;
		Guitar.to = Quaternion.Euler(0, 10, -30);
		Guitar.from = Quaternion.Euler(3, -20, 15);
		   
		transform.GetChild(0).gameObject.SetActive(startActive);

	}

	// Update is called once per frame
	void Update()
	{
		if (!active)
		{
			return;
		}
		head.animate();
		Guitar.animate();	  
	}
	void OnTriggerEnter2D(Collider2D other)
	{																		   
		if (other.gameObject.CompareTag("MainCamera"))
		{
			active = true;
			transform.GetChild(0).gameObject.SetActive(true);
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
