using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	
	public Sprite dieSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Kill(){
		StartCoroutine(Die());
	}
	
	IEnumerator Die(){
		GetComponent<SpriteRenderer>().sprite = dieSprite;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		yield return new WaitForSeconds(0.1f);
		Destroy (gameObject);
		yield return null;
	}
}
