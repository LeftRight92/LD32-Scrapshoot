using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public UIBar health, scrap;
	public Text healthText, scrapText, waveText, scoreText;
	private PlayerController player;
	private GameController game;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void Update () {
		health.amount = player.health/player.maxHealth;
		scrap.amount = player.scrap/10;
		healthText.text = "Health: " + player.health + "/" + player.maxHealth;
		scrapText.text = "Scrap: " + player.scrap + "/10";
		scoreText.text = "Score: " + game.score;
		waveText.text = "Wave: " + game.waveNumber;
	}
}
