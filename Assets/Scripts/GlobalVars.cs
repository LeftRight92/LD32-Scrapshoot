using UnityEngine;
using System.Collections;

public class GlobalVars : MonoBehaviour{

	public int score = 0, wave = 1;
	
	void Awake(){
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel("Menu");
	}
}
