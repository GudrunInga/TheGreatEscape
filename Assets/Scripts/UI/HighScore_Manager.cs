using System;
using System.Collections.Generic;
using UnityEngine; 

public class HighScore_Manager : MonoBehaviour { 
	//Singleton pattern
	public static HighScore_Manager instance()
	{	  
		return me;
	}	   
	private static HighScore_Manager me = null;

	//Highscore variables
	public int slots;	   
	private List<ScoreData> scores;

	//Start
	void Start () {
		me = gameObject.GetComponent<HighScore_Manager>();
		scores = new List<ScoreData>();
		for(int i = 0; i < slots; i++)
		{
			scores.Add(new ScoreData());
		}
		sortList();
		load();
	}

	public void save()
	{				
		string path = Application.persistentDataPath + "/List.dat";
		string text = "";
		Debug.Log(scores.Count);			   
		foreach (ScoreData sc in scores)
		{
			string json = JsonUtility.ToJson(sc);	  
			text += json + "\n";
			Debug.Log(json);
		}
		Debug.Log(Application.persistentDataPath);
		System.IO.File.WriteAllText(path, text);
	}	  

	public void load()
	{
		string path = Application.persistentDataPath + "/List.dat";
		string[] data = System.IO.File.ReadAllLines(path);
		foreach (String entry in data)
		{
			ScoreData newEntry = JsonUtility.FromJson<ScoreData>(entry);
			scores.Add(newEntry);
			Debug.Log(entry);
			Debug.Log(newEntry.ToString());
		}
		sortList();
		Debug.Log("loading done.");
	}

	private void sortList()
	{
		scores.Sort();
		while (scores.Count > slots)
		{
			scores.RemoveAt(scores.Count - 1);
		}
	}
	public void addScore(int score, int run, int coins, float time)
	{
		ScoreData newscore = new ScoreData(score, run, coins, time);
		scores.Add(newscore);
		scores.Sort();
		while(scores.Count > slots)
		{
			scores.RemoveAt(scores.Count - 1);
		}
	}
	public int getScore(int position)
	{
		if(position >= slots)
		{
			Debug.Log("getScore error: position " + position + " is out of range [0," + (slots - 1) + "]");
			return -1;
		}
		return scores[position].score;
	}
	public int getRun(int position)
	{
		if (position >= slots)
		{
			Debug.Log("getRun error: position " + position + " is out of range [0," + (slots - 1) + "]");
			return -1;
		}
		return scores[position].run;
	}
	public int getCoins(int position)
	{
		if (position >= slots)
		{
			Debug.Log("getCoins error: position " + position + " is out of range [0," + (slots - 1) + "]");
			return -1;
		}
		return scores[position].coins;
	}
	public float getTime(int position)
	{
		if (position >= slots)
		{
			Debug.Log("getTime error: position " + position + " is out of range [0," + (slots - 1) + "]");
			return -1;
		}			  
		return scores[position].time;
	}
	
	public List<ScoreData> get_scores()
	{
		return scores;
	}
	[Serializable]
	public class ScoreData : IComparable<ScoreData>
	{
		public int score;
		public int run;
		public int coins;
		public float time;
		public ScoreData(int Score = -1, int Run = -1, int Coins = -1, float duration = -1)
		{
			score = Score;
			run = Run;
			coins = Coins;
			time = duration;
		}
		public int CompareTo(ScoreData item)
		{       // A null value means that this object is greater.
			if (item == null)
			{
				return -1;
			}
			else {
				return -score.CompareTo(item.score);
			}
		}

		public override string ToString()
		{ 
			if(score == -1)
			{
				return "No score! (yet)";
			}
			string text = "";
			text += score.ToString() + " Points!   ";
			text += "[Run #" + run + " | " + coins + " coins collected |" + time + " seconds survived]";
			return text;
		}
	} 
}
