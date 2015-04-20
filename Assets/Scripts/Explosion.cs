using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	private float rot_z;
	// Use this for initialization
	void Start () {
		rot_z = Random.Range (-60,60);
		StartCoroutine(Kill());
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,rot_z);
		transform.localScale += Vector3.one * 0.5f * Time.deltaTime;
	}
	
	IEnumerator Kill(){
		yield return new WaitForSeconds(0.2f);
		GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(2);
		Destroy (gameObject);
	}
}
