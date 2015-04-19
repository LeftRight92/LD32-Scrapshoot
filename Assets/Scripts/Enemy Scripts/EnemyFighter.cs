using UnityEngine;
using System.Collections;

public class EnemyFighter : EnemyStrike {

	public GameObject bulletPrefab;
	public float shotDelay, shotSpeed;

	private SpriteRenderer muzzleFlash;
	private bool shooting;
	
	// Use this for initialization
	protected override void Start () {
		muzzleFlash = transform.GetChild (0).GetComponent<SpriteRenderer>();
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		if(!shooting && transform.position.x < shootingBarrier) StartCoroutine(Shoot());
		base.Update();
	}
	
	protected override void FixedUpdate(){
		base.FixedUpdate ();
	}
	
	IEnumerator Shoot(){
		shooting = true;
		while(true){
			muzzleFlash.enabled = true;
			GameObject clone = Instantiate (bulletPrefab, transform.GetChild (0).position, transform.GetChild(0).rotation) as GameObject;
			clone.GetComponent<Rigidbody2D>().velocity = new Vector2(-shotSpeed, 0);
			GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(0.1f);
			muzzleFlash.enabled = false;
			yield return new WaitForSeconds(shotDelay - 0.1f);
			
		}
	}
}
