using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObject;
	public GameObject balloonObject;

	private bool buttonSelected;
	float speed = 1000.0f;
	int i;
	// Use this for initialization
	void Start () {
		eventSystem.SetSelectedGameObject(selectedObject);

		buttonSelected = true;
		i = 1;
		/*if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false) 
		{
			eventSystem.SetSelectedGameObject(selectedObject);

			buttonSelected = true;
		}*/
	}

	// Update is called once per frame
	void Update () 
	{
		if ((Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && i != 3) {
			var move = new Vector3 (balloonObject.transform.position.x, balloonObject.transform.position.y - 0.9f, balloonObject.transform.position.z);
			balloonObject.transform.position = move;
			i++;
		}
		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && i != 1) {
			var move = new Vector3 (balloonObject.transform.position.x, balloonObject.transform.position.y + 0.9f, balloonObject.transform.position.z);
			balloonObject.transform.position = move;
			i--;
		}
	}

	private void OnDisable()
	{
		buttonSelected = false;
	}
}