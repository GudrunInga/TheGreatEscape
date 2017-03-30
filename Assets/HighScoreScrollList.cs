using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreScrollList : MonoBehaviour {
	// Use this for initialization
	public Transform highContentPanel;
	public SimpleObjectPool textObjectPool;

	void Start () {
		RefreshDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void RefreshDisplay(){
		RemoveText ();
		AddText ();
	}

	private void RemoveText()
	{
		while (textObjectPool.transform.childCount > 0) 
		{
			GameObject toRemove = transform.GetChild(0).gameObject;
			textObjectPool.ReturnObject(toRemove);
		}
	}

	private void AddText()
	{
		var scoreList = HighScore_Manager.instance ().get_scores ();
		foreach (var item in scoreList) {
			GameObject newText = textObjectPool.GetObject();
			newText.transform.SetParent(highContentPanel);

			FinalScoreText scoreText = newText.GetComponent<FinalScoreText>();
			scoreText.Setup(item, this);
			//scoreText.GetComponent<RectTransform>().
			scoreText.transform.localPosition = new Vector3(scoreText.transform.localPosition.x, scoreText.transform.localPosition.y, 0);
			scoreText.transform.localScale = new Vector3(1, 1, 1);

		}
	}
}
