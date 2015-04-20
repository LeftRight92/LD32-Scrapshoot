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
	
	public bool topSlotFree = true, bottomSlotFree = true;
	
	public int waveNumber = 1;
	public int score;
	public float spawnDelay = 3;
	public float spawnRandomness = 1;
	public int wavePoints = 10;
	public int increment = 5;
	
	public Transform[] smallSpawns, largeSpawns;
	public GameObject fighterPrefab, bomberPrefab, frigatePrefab;
	
	private bool inWave = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!inWave){
			StartCoroutine(RunWave(CreateWave (wavePoints)));
//			List<Ship> frigateWave = new List<Ship>();
//			frigateWave.Add(Ship.Frigate);
//			frigateWave.Add(Ship.Frigate);
//			frigateWave.Add(Ship.Frigate);
//			frigateWave.Add(Ship.Frigate);
//			StartCoroutine(RunWave(frigateWave));
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
	
	public void clearLane(string lane){
		StartCoroutine(WaitToClear(lane));
	}
	
	public void GameOver(){
		StartCoroutine(EndGame());
	}
	
	IEnumerator EndGame(){
		yield return new WaitForSeconds(3);
		GlobalVars vars = GameObject.FindGameObjectWithTag("GlobalVars").GetComponent<GlobalVars>();
		vars.score = score;
		vars.wave = waveNumber;
		Application.LoadLevel ("GameOver");
	}
	
	IEnumerator WaitToClear(string lane){
		yield return new WaitForSeconds(3f);
		if(lane == "top"){
			topSlotFree = true;
		}else if (lane == "bottom"){
			bottomSlotFree = true;
		}
	}
	
	IEnumerator RunWave(List<Ship> wave){
		string waveS = "Starting new wave: ";
		foreach(Ship s in wave){
			waveS += s + ", ";
		}
		//Debug.Log (waveS);
		foreach(Ship nextItem in wave){
			inWave = true;
			Transform spawnPos = smallSpawns[Random.Range (0, smallSpawns.Length)];
			switch(nextItem){
			case Ship.Frigate:
				if(!topSlotFree && !bottomSlotFree) break;
				if(topSlotFree && bottomSlotFree) spawnPos = largeSpawns[Random.Range (0, largeSpawns.Length)];
				else if (topSlotFree) spawnPos = largeSpawns[0];
				else if (bottomSlotFree) spawnPos = largeSpawns[1];
				GameObject frigate = Instantiate(frigatePrefab, spawnPos.position, spawnPos.rotation) as GameObject;
				frigate.GetComponent<EnemyFrigate>().lane = spawnPos==largeSpawns[0]?"top":"bottom";
				if(spawnPos==largeSpawns[0]) topSlotFree = false;
				else bottomSlotFree = false;
				break;
			case Ship.Fighter:
				GameObject fighter = Instantiate(fighterPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
				if(spawnPos.tag == "SpawnSouth") fighter.GetComponent<EnemyFighter>().moveUp = true;
				if(spawnPos.tag == "SpawnCentre") fighter.GetComponent<EnemyFighter>().moveUp = Random.Range (0,1) == 1;
				//Chose up/down dep. on position
				break;
			case Ship.Bomber:
				GameObject bomber = Instantiate(bomberPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
				if(spawnPos.tag == "SpawnNorth") bomber.GetComponent<EnemyBomber>().moveUp = true;
				if(spawnPos.tag == "SpawnCentre") bomber.GetComponent<EnemyBomber>().moveUp = Random.Range (0,1) == 1;
				break;
			case Ship.FighterWing:
				GameObject fighter1 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(0,1.1f,0), spawnPos.rotation) as GameObject;
				GameObject fighter2 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(0,-1.1f,0), spawnPos.rotation) as GameObject;
				GameObject fighter3 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(2,2.4f,0), spawnPos.rotation) as GameObject;
				GameObject fighter4 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(2,-2.4f,0), spawnPos.rotation) as GameObject;
				fighter3.GetComponent<EnemyFighter>().movingBarrier += 2;
				fighter4.GetComponent<EnemyFighter>().movingBarrier += 2;
				if(spawnPos.tag == "SpawnSouth"){
					fighter1.GetComponent<EnemyFighter>().moveUp = true;
					fighter2.GetComponent<EnemyFighter>().moveUp = true;
					fighter3.GetComponent<EnemyFighter>().moveUp = true;
					fighter4.GetComponent<EnemyFighter>().moveUp = true;
				}else if(spawnPos.tag == "SpawnCentre"){
					fighter1.GetComponent<EnemyFighter>().moveUp = true;
					fighter3.GetComponent<EnemyFighter>().moveUp = true;
				}
				break;
			case Ship.BomberPair:
				GameObject bomber1 = Instantiate(bomberPrefab, spawnPos.position + new Vector3(0,1.5f,0), spawnPos.rotation) as GameObject;
				GameObject bomber2 = Instantiate(bomberPrefab, spawnPos.position + new Vector3(0,-1.5f,0), spawnPos.rotation) as GameObject;
				if(spawnPos.tag == "SpawnNorth"){
					bomber1.GetComponent<EnemyBomber>().moveUp = true;
					bomber2.GetComponent<EnemyBomber>().moveUp = true;
				}else if(spawnPos.tag == "SpawnCentre"){
					bomber1.GetComponent<EnemyBomber>().moveUp = true;
				}
				break;
			}
			yield return new WaitForSeconds(spawnDelay + Random.Range (-spawnRandomness, spawnRandomness));
		}
		waveNumber++;
		wavePoints += increment;
		//spawnDelay -= Mathf.Max(0.1f * spawnDelay, 0.4f);
		//spawnRandomness -= 0.1f * spawnRandomness;
		while(GameObject.FindGameObjectsWithTag("EnemyStrike").Length != 0){
//			if(GameObject.FindGameObjectsWithTag("EnemyStrike").Length == 0 && GameObject.FindGameObjectsWithTag("EnemyFrigate").Length != 0){
//				Transform spawnPos = smallSpawns[Random.Range (0, smallSpawns.Length)];
//				GameObject fighter1 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(0,1.1f,0), spawnPos.rotation) as GameObject;
//				GameObject fighter2 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(0,-1.1f,0), spawnPos.rotation) as GameObject;
//				GameObject fighter3 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(2,2.4f,0), spawnPos.rotation) as GameObject;
//				GameObject fighter4 = Instantiate(fighterPrefab, spawnPos.position + new Vector3(2,-2.4f,0), spawnPos.rotation) as GameObject;
//				fighter3.GetComponent<EnemyFighter>().movingBarrier += 2;
//				fighter4.GetComponent<EnemyFighter>().movingBarrier += 2;
//				if(spawnPos.tag == "SpawnSouth"){
//					fighter1.GetComponent<EnemyFighter>().moveUp = true;
//					fighter2.GetComponent<EnemyFighter>().moveUp = true;
//					fighter3.GetComponent<EnemyFighter>().moveUp = true;
//					fighter4.GetComponent<EnemyFighter>().moveUp = true;
//				}else if(spawnPos.tag == "SpawnCentre"){
//					fighter1.GetComponent<EnemyFighter>().moveUp = true;
//					fighter3.GetComponent<EnemyFighter>().moveUp = true;
//				}
//			}
			yield return null;
		}
		//Debug.Log ("Wave Exit");
		inWave = false;
	}
}
