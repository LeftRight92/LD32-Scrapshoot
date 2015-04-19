using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	
	private GameObject player;
	private float trackingStartTime;
	private Rigidbody2D rigidbody;
	public float trackingDelay, force;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rigidbody = GetComponent<Rigidbody2D>();
		trackingStartTime = Time.time + trackingDelay;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Time.time > trackingStartTime){
			Quaternion rotation = Quaternion.LookRotation
				((player.transform.position - transform.position), transform.TransformDirection(Vector3.up));
			transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			//if(transform.position.y > player.transform.position.y) transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z - 180);
		}
		rigidbody.AddForce(transform.right * force);
	}
	
	public void Kill(){
		StartCoroutine(Die());
	}
	
	IEnumerator Die(){
		Destroy(gameObject);
		yield return null;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "PlayerShot"){
			other.GetComponent<Shot>().Kill ();
			StartCoroutine(Die ());
		}
	}
}
