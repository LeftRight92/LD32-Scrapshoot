using UnityEngine;
using System.Collections;

public class EnemyFrigate : MonoBehaviour {
	
	private GameObject player;
	private Rigidbody2D rigidbody;
	private bool shooting = false;
	
	public float speed, health, roll;
	public float shootingBarrier, movingBarrier;
	public GameObject[] wrecks;
	public string lane;
	
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = new Vector2(-speed, 0);
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update(){
		if(!shooting && transform.position.x < shootingBarrier) Shoot();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.position.x < movingBarrier){
			rigidbody.velocity = Vector2.zero;
		}
		foreach(Turret t in GetComponentsInChildren<Turret>()){
			t.AimAtPoint(player.transform.position);
		}
	}
	
	void Shoot(){
		shooting = true;
		foreach(Turret t in GetComponentsInChildren<Turret>()){
			t.fireButton = true;
		}
	}
	
	IEnumerator Die(){
		GameObject clone = Instantiate(wrecks[Random.Range (0,wrecks.Length)],transform.position, Quaternion.identity) as GameObject;
		clone.GetComponent<Rigidbody2D>().velocity = Vector2.right * 3.0f;
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().clearLane(lane);
		Destroy(gameObject);
		yield return null;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "ScrapShot"){
			health--;
			other.GetComponent<ScrapShot>().Kill ();
			if(health<=0){
				StartCoroutine(Die());
				GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().score += 1000;
			}
		}
	}
}
