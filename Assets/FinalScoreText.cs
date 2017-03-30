using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreText : MonoBehaviour {
	public Text text;
	private HighScoreScrollList _highScoreScrollList;

	public void Setup(HighScore_Manager.ScoreData textLine, HighScoreScrollList highScoreList){
		//text.text = textLine.coins.ToString ();
		//text.text += textLine.run.ToString ();
		//text.text += textLine.score.ToString ();
		//text.text += textLine.time.ToString ();
		text.text = textLine.ToString ();

		_highScoreScrollList = highScoreList;
	}
}
