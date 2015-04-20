using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBomber : EnemyStrike {

	public GameObject missilePrefab;
	
	private List<Transform> tubes;
	private bool shooting;
	
	// Use this for initialization
	protected override void Start () {
		tubes = new List<Transform>();
		tubes.Add (transform.GetChild(0).transform);
		tubes.Add (transform.GetChild (1).transform);
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
		foreach(Transform tube in tubes){
			Instantiate(missilePrefab, tube.position, tube.rotation);
		}
		yield return null;
	}
}
