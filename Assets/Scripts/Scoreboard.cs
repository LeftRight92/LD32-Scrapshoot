using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoreboard : MonoBehaviour {

	public Text scoreText, waveText;

	// Use this for initialization
	void Start () {
		GlobalVars vars = GameObject.FindGameObjectWithTag("GlobalVars").GetComponent<GlobalVars>();
		scoreText.text = "" + vars.score;
		waveText.text = "" + vars.wave;
	}
}
