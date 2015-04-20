using UnityEngine;
using System.Collections;

public class EnemyStrike : MonoBehaviour {

	public float speed, accel, health, roll;
	public float shootingBarrier, movingBarrier;
	public bool moveUp;
	public GameObject[] wrecks;
	public GameObject explosion;

	private Rigidbody2D rigidbody;
	
	// Use this for initialization
	protected virtual void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = new Vector2(-speed, 0);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
	
	protected virtual void FixedUpdate() {
		if(transform.position.x < movingBarrier){
			rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, new Vector2(-1,moveUp?1:-1).normalized * speed, accel);
		}
		transform.rotation = Quaternion.Euler ((rigidbody.velocity.y * -roll), 180f, 180f);
	}
	
	IEnumerator Die(){
		GameObject clone = Instantiate(wrecks[Random.Range (0,wrecks.Length)],transform.position, Quaternion.identity) as GameObject;
		clone.GetComponent<Rigidbody2D>().velocity = rigidbody.velocity;
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
		yield return null;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "PlayerShot"){
			health--;
			other.GetComponent<Shot>().Kill ();
			if(health<=0){
				GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().score += 300;
			 	StartCoroutine(Die());
		 	}
		}else if(other.gameObject.tag == "ScrapShot"){
			other.GetComponent<ScrapShot>().Kill();
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().score += 400;
			StartCoroutine(Die());
		}
			
	}
}
