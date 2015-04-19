using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public bool fireButton, delay;
	private bool firing, leftIsNext = false;
	public float shotDelay;
	public float shellSpeed = 3f;
	
	private GameObject left, right;
	
	public GameObject shellPrefab;
	

	// Use this for initialization
	void Start () {
		left = transform.GetChild(0).GetChild(0).gameObject;
		right = transform.GetChild(0).GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(fireButton && !firing){
			StartCoroutine(fire());
		}
	}
	
	public void AimAtPoint(Vector3 point){
		transform.LookAt(point, Vector3.forward);
	
	}
	
	IEnumerator fire(){
		firing = true;
		if(delay) yield return new WaitForSeconds(shotDelay/2);
		while(fireButton){
			GameObject current = leftIsNext? left: right;
			
			current.GetComponent<SpriteRenderer>().enabled = true;
			GameObject clone = Instantiate(shellPrefab, current.transform.position, current.transform.rotation) as GameObject;
			clone.GetComponent<Rigidbody2D>().velocity = (GameObject.FindGameObjectWithTag("Crosshair").transform.position - current.transform.position).normalized * shellSpeed;
			yield return new WaitForSeconds(0.1f);
			
			current.GetComponent<SpriteRenderer>().enabled = false;
			yield return new WaitForSeconds(shotDelay - 0.1f);
			
			leftIsNext = !leftIsNext;
		}
		firing = false;
		yield return null;
	}
}
