using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour {

	//Should be 1 to load Escape scene
	//Menu needs to be first in build order (0)
	//Escape second (1)
	public void LoadScene(int i)
	{
		SceneManager.LoadScene (i);
	}
	//Call if quitting the application
	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}
