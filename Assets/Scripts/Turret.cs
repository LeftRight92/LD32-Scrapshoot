using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public bool fireButton, delay;
	private bool firing, leftIsNext = false;
	public float shotDelay, shellSpeed;
	public float normalShotDelay, normalShellSpeed, turboShotDelay, turboShellSpeed;
	private Vector3 aimPos;
	private GameObject left, right;
	
	public GameObject shellPrefab;
	

	// Use this for initialization
	void Start () {
		left = transform.GetChild(0).GetChild(0).gameObject;
		right = transform.GetChild(0).GetChild(1).gameObject;
		ToggleTurbo(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(fireButton && !firing){
			StartCoroutine(fire());
		}
	}
	
	public void ToggleTurbo(bool turbo){
		if(turbo){
			shotDelay = turboShotDelay;
			shellSpeed = turboShellSpeed;
		}else{
			shotDelay = normalShotDelay;
			shellSpeed = normalShellSpeed;
		}
	}
	
	public void AimAtPoint(Vector3 point){
		aimPos = point;
		transform.LookAt(point, Vector3.forward);
	
	}
	
	IEnumerator fire(){
		firing = true;
		if(delay) yield return new WaitForSeconds(shotDelay/2);
		while(fireButton){
			GameObject current = leftIsNext? left: right;
			
			current.GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<AudioSource>().Play();
			GameObject clone = Instantiate(shellPrefab, current.transform.position, Quaternion.Euler(0,0,90)) as GameObject;
			clone.GetComponent<Rigidbody2D>().velocity = (aimPos - current.transform.position).normalized * shellSpeed;
			yield return new WaitForSeconds(0.1f);
			
			current.GetComponent<SpriteRenderer>().enabled = false;
			yield return new WaitForSeconds(shotDelay - 0.1f);
			
			leftIsNext = !leftIsNext;
		}
		firing = false;
		yield return null;
	}
}
