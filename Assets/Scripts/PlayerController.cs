using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float shipSpeed = 1.0f, crossSpeed = 1.0f;
	public float turboTime;
	public float roll = 3f, pitch = 3f;
	public float health, maxHealth = 100, scrap = 0;
	
	public GameObject aimPos, explosion;
	public GameObject[] scrapShotPrefabs;
	
	private Rigidbody2D rigidbody;
	private bool turbo = false;
	
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update(){
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.z = 0;
		aimPos.transform.position = mousePosition;
	
		if(Input.GetButton ("Fire1")){
			foreach(Turret t in GetComponentsInChildren<Turret>()){
				t.fireButton = true;
			}
		}else{
			foreach(Turret t in GetComponentsInChildren<Turret>()){
				t.fireButton = false;
			}
		}
		if(Input.GetButtonDown ("Turbo") && scrap>=3 && !turbo){
			StartCoroutine(Turbo());
		}
		if(Input.GetButtonDown ("Fire2") && scrap>=2){
			scrap-=2;
			Instantiate(scrapShotPrefabs[Random.Range(0,scrapShotPrefabs.Length)], transform.GetChild(1).transform.position, Quaternion.identity);
		}
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//MOVE SHIP
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0) * shipSpeed;
		rigidbody.velocity = movement;
		rigidbody.position = new Vector2(Mathf.Clamp (rigidbody.position.x, -24, 3),
										Mathf.Clamp (rigidbody.position.y, -12, 12));
		transform.GetChild (0).rotation = Quaternion.Euler ((rigidbody.velocity.y * -roll)+90, (rigidbody.velocity.x * -pitch)+180, 0.0f);
		
		//Rotate Turrets
		foreach(Turret t in GetComponentsInChildren<Turret>()){
			t.AimAtPoint(aimPos.transform.position);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "EnemyShot"){
			health -= 1;
			other.GetComponent<Shot>().Kill();
		}else if(other.gameObject.tag == "EnemyMissile"){
			health -= 3;
			other.GetComponent<Missile>().Kill();
		}else if(other.gameObject.tag == "EnemyShell"){
			health -= 3;
			other.GetComponent<Shot>().Kill ();
		}else if(other.gameObject.tag == "Wreck"){
			if(!(scrap >= 10)){
				scrap += 1;
				Destroy(other.gameObject);
			}
		}
		if(health <= 0){
			StartCoroutine(Die());
		}
	}
	
	IEnumerator Die(){
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();
		Instantiate(explosion, transform.position, Quaternion.identity);
		//Destroy(gameObject);
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetComponent<PlayerController>().enabled = false;
		transform.GetComponent<BoxCollider2D>().enabled = false;
		yield return null;
	}
	
	IEnumerator Turbo(){
		turbo = true;
		scrap-=3;
		foreach(Turret t in GetComponentsInChildren<Turret>()){
			t.ToggleTurbo(true);
		}
		yield return new WaitForSeconds(turboTime);
		foreach(Turret t in GetComponentsInChildren<Turret>()){
			t.ToggleTurbo(false);
		}
		turbo = false;
		yield return null;
	}
}
