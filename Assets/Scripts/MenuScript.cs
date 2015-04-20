using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void StartGame(){
		Application.LoadLevel("Game");
	}
	
	void Instructions(){
		Application.LoadLevel("Instructions");
	}
	
	void Exit(){
		Application.Quit ();
	}
}
