using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float shipSpeed = 1.0f;
	public float crossSpeed = 1.0f;
	
	public float roll = 3f;
	public float pitch = 3f;
	
	public GameObject aimPos;
	public bool mouseMode;
	
	private Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update(){
	if(mouseMode){
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.z = 0;
		aimPos.transform.position = mousePosition;
	}
	
	if(Input.GetButton ("Fire1")){
		foreach(Turret t in GetComponentsInChildren<Turret>()){
			t.fireButton = true;
		}
	}else{
		foreach(Turret t in GetComponentsInChildren<Turret>()){
			t.fireButton = false;
		}
	}
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//MOVE SHIP
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0) * shipSpeed;
		rigidbody.velocity = movement;
		rigidbody.position = new Vector2(Mathf.Clamp (rigidbody.position.x, -24, 3),
										Mathf.Clamp (rigidbody.position.y, -12, 12));
		transform.GetChild (0).rotation = Quaternion.Euler ((rigidbody.velocity.y * -roll)+90, (rigidbody.velocity.x * -pitch)-180, 0.0f);
		
		//Rotate Turrets
		foreach(Turret t in GetComponentsInChildren<Turret>()){
			t.AimAtPoint(aimPos.transform.position);
		}
		
		if(!mouseMode){
			Rigidbody2D aimRigidbody = aimPos.GetComponent<Rigidbody2D>();
			Vector3 aimMovement = new Vector3(Input.GetAxis("CrossHorizontal"),Input.GetAxis("CrossVertical"), 0) * crossSpeed;
			aimRigidbody.velocity = aimMovement;
			aimRigidbody.position = new Vector2(Mathf.Clamp (aimRigidbody.position.x, -25, 25),
			                                    Mathf.Clamp (aimRigidbody.position.y, -13, 13));
			Debug.Log ("Axis 1: (\"Horizontal\"): " + Input.GetAxis("Horizontal") + "\n" +
			           "Axis 4: (\"CrossVert\" ): " + Input.GetAxis("CrossVertical"));
		}
	}
}
