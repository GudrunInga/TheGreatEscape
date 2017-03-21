using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour {
	public GameObject balloonObject;

	void OnHover()
	{

	}
	void Update()
	{

	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log ("Pointer entered" + gameObject.name);
		balloonObject.transform.position = new Vector3 (balloonObject.transform.position.x, transform.position.y, balloonObject.transform.position.z);
		/*if ((Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && i != 3) {
			var move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
			//var speed = new Vector3 (balloonObject.transform.position.x, balloonObject.transform.position.y + 30f, transform.position.z);
			balloonObject.transform.position += move * speed * Time.deltaTime;
			i++;
		}
		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && i != 1) {
			var move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
			balloonObject.transform.position += move * speed * 0.01f;//Time.deltaTime;
			i--;
		}*/
		//do your stuff when highlighted
	}
	public void OnSelect(BaseEventData data)
	{
		Debug.Log ("Selected");
		balloonObject.transform.position = new Vector3 (balloonObject.transform.position.x, transform.position.y, balloonObject.transform.position.z);


	}

}
