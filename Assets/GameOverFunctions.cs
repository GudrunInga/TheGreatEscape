using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFunctions : MonoBehaviour {
	//For death animation
	private bool _dead;
	//Text box for reason of death
	private Text _killReason;
	//Text box for coins collected during play
	private Text _coinsCollected;
	//Text box for coins owned
	private Text _coinsOwned;
	//Text box for final score
	private Text _finalScore;
	//Game Over, u dead
	private bool _gameOver;
	//Game over menu
	private GameObject gameOverMenu;
	//Player
	private BalloonController _balloonController;

	// Use this for initialization
	void Start () {
		_gameOver = false;
		//For balloon death animation
		_dead = false;
		//Laundry death
		UIController.instance.laundry = false;
		//Steel balloon death
		UIController.instance.steel = false;
	}
	public void ResetScene(bool gameOver, bool dead)
	{
		_gameOver = gameOver;
		_dead = dead;
	}
	public bool GetGameOver()
	{
		return _gameOver;
	}
	public void GameOver(string deathReason)
	{
		if (!_dead) {
			_gameOver = true;
			_SetKillReason ();
			_SetCoinsCollected ();
			_SetCoinsOwned ();
			_SetFinalScore ();
			_DeathMessage (deathReason);
			_BalloonDeath ();
			_dead = false;
		}
	}

	private void _SetKillReason()
	{
		_killReason = UIController.instance.gameOverMenu.transform.Find ("Panel").transform.Find("DeathReason").gameObject.GetComponent<Text>();
	}

	private void _SetCoinsCollected ()
	{
		_coinsCollected = UIController.instance.gameOverMenu.transform.Find ("Panel").transform.Find ("CoinsCollected").gameObject.GetComponent<Text> ();
		_coinsCollected.text += UIController.instance.GetCoinsCollected().ToString();
	}
	private void _SetCoinsOwned()
	{
		_coinsOwned = UIController.instance.gameOverMenu.transform.Find ("Panel").transform.Find ("CoinsOwned").gameObject.GetComponent<Text> ();
		_coinsOwned.text += UIController.instance.GetCoins().ToString();

	}
	private void _SetFinalScore()
	{
		_finalScore = UIController.instance.gameOverMenu.transform.Find ("Panel").transform.Find ("FinalScore").gameObject.GetComponent<Text> ();
		//Change the score, need to add more to it than that
		//maybe this should be coins collected?
		_finalScore.text += UIController.instance.GetCoins().ToString();
	}
		
	//Start animating the balloon popping
	private void _BalloonDeath()
	{
		UIController.instance.player.GetComponent<BalloonController> ().pop ();
		StartCoroutine ("CoGameOver");
	}
	//Show the game over menu, stop play and turn off the spotlights
	private IEnumerator CoGameOver()
	{
		yield return new WaitForSeconds(1); 
		UIController.instance.gameOverMenu.SetActive(true);
		UIController.instance.TurnOn (false);
	}
	//Different message for each death reason
	private void _DeathMessage(string deathReason)
	{
		if (UIController.instance.laundry)
		{
			_killReason.text = "Death by laundry....that is bad, maybe you need to cut through it?";
			UIController.instance.laundry = false;
		}
		if (UIController.instance.steel) {
			_killReason.text = "Steel is heavy....who knew?";
			UIController.instance.steel = true;
		} 
		else if (deathReason == "Spike") {
			_killReason.text = "Pointy things are bad for balloons, if only you could get better controls";
		} 
		else if (deathReason == "Slow") {
			_killReason.text = "You were too slow, maybe go faster next time, if only you could increase your speed";
		} 
		else if (deathReason == "Bird") {
			_killReason.text = "Birds ain't scared of you, what are birds scared off?";
		} 
		else if (deathReason == "Cat") {
			_killReason.text = "Cats ain't afraid of you";
		}
	}
}
