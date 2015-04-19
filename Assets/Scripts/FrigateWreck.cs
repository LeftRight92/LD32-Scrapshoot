using UnityEngine;
using System.Collections;

public class FrigateWreck : MonoBehaviour {
	
	private float x, y, z;
	
	// Use this for initialization
	void Start () {
		x = Random.Range(-60,60);
		y = Random.Range(-60,60);
		z = Random.Range(-60,60);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
	}
}
