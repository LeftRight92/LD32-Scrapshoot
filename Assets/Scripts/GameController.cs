using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	enum Ship{
		Fighter,
		FighterWing,
		Bomber,
		BomberPair,
		Frigate
	};
	
	public int waveNumber = 1;
	public float spawnDelay = 1;
	public float spawnRandomness = 1;
	public int wavePoints = 10;
	
	public Transform[] smallSpawns, largeSpawns;
	public GameObject fighterPrefab, bomberPrefab, frigatePrefab;
	
	private bool inWave = false;
	
	// Use this for initialization
	void Start () {
		foreach(Ship s in CreateWave (wavePoints)){
			Debug.Log(s);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!inWave){
			StartCoroutine(RunWave(CreateWave (wavePoints)));
		}
	}
	
	List<Ship> CreateWave(int points){
		List<Ship> wave = new List<Ship>();
		while(points > 0){
			switch(Random.Range (0, 5)){
			case 0:
				wave.Add (Ship.Fighter);
				points -= 1;
				break;
			case 1:
				if(points < 3) break;
				wave.Add (Ship.FighterWing);
				points -= 3;
				break;
			case 2:
				if(waveNumber < 2 || points < 2) break;
				wave.Add (Ship.Bomber);
				points -= 2;
				break;
			case 3:
				if(waveNumber < 2 || points < 3) break;
				wave.Add (Ship.BomberPair);
				points -= 3;
				break;
			case 4:
				if(waveNumber < 3 || points < 5) break;
				wave.Add (Ship.Frigate);
				points -= 5;
				break;
			}
		}
		return wave;
	}
	
	IEnumerator RunWave(List<Ship> wave){
		Debug.Log ("Starting new wave");
		foreach(Ship nextItem in wave){
			inWave = true;
			Transform spawnPos = smallSpawns[Random.Range (0, smallSpawns.Length)];
			switch(nextItem){
			case Ship.Frigate:
				spawnPos = largeSpawns[Random.Range (0, largeSpawns.Length)];
				Instantiate(frigatePrefab, spawnPos.position, spawnPos.rotation);
				break;
			case Ship.Fighter:
				GameObject fighter = Instantiate(fighterPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
				//Chose up/down dep. on position
				break;
			case Ship.Bomber:
				GameObject bomber = Instantiate(bomberPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
				break;
			case Ship.FighterWing:
				GameObject fighter1 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(0,1.1f,0), spawnPos.rotation) as GameObject;
				GameObject fighter2 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(0,-1.1f,0), spawnPos.rotation) as GameObject;
				GameObject fighter3 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(2,2.4f,0), spawnPos.rotation) as GameObject;
				GameObject fighter4 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(2,-2.4f,0), spawnPos.rotation) as GameObject;
				//chose up/down dep. on position
				break;
			case Ship.BomberPair:
				GameObject bomber1 = Instantiate(bomberPrefab, spawnPos.position + new Vector3(0,1.5f,0), spawnPos.rotation) as GameObject;
				GameObject bomber2 = Instantiate(bomberPrefab, spawnPos.position + new Vector3(0,-1.5f,0), spawnPos.rotation) as GameObject;
				//chose up/down dep. on position
				break;
			}
			yield return new WaitForSeconds(spawnDelay + Random.Range (-spawnRandomness, spawnRandomness));
		}
		waveNumber++;
		Debug.Log (GameObject.FindGameObjectsWithTag("EnemyStrike").Length );
		while(GameObject.FindGameObjectsWithTag("EnemyStrike").Length != 0 || GameObject.FindGameObjectsWithTag("EnemyFrigate").Length != 0){
			yield return null;
		}
		Debug.Log ("Wave Exit");
		inWave = false;
	}
}
