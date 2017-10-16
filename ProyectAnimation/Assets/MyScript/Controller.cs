using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


	public float maxSpeed = 10f;
	private Animator animation_body;
	private Rigidbody2D player_body;
	public bool jump;

	private bool IsJump;

	void Awake(){

		animation_body = GetComponent<Animator> ();
		player_body = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate (){
		


		if (Input.GetKeyDown(KeyCode.W))
		{
			
			animation_body.SetBool ("IsJump", true);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{

			animation_body.SetBool ("IsRunning", true);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{

			animation_body.SetBool ("IsRunning", false);
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			
			animation_body.SetBool ("IsJump", false);
		}


	}
	 



	
	// Update is called once per frame
	void Update () {
		
	}
}
