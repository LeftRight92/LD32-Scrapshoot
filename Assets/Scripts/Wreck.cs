using UnityEngine;
using System.Collections;

public class Wreck : MonoBehaviour {

	private float x, y, z;

	// Use this for initialization
	void Start () {
		x = Random.Range(-90,90);
		y = Random.Range(-90,90);
		z = Random.Range(-90,90);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.GetChild(0).Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
	}
}
