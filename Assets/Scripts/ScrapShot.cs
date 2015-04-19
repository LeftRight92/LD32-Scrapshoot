using UnityEngine;
using System.Collections;

public class ScrapShot : MonoBehaviour {

	public float speed;
	private float x, y, z;
	
	// Use this for initialization
	void Start () {
		x = Random.Range(-90,90);
		y = Random.Range(-90,90);
		z = Random.Range(-90,90);
		GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
	}
	
	public void Kill(){
		StartCoroutine(Die());
	}
	
	IEnumerator Die(){
		Destroy(gameObject);
		yield return null;
	}
}
