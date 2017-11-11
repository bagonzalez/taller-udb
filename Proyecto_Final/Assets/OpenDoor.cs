using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

	public GameObject door;

	private Animator animation_body;
	//private Rigidbody player_body;

	void Awake(){

		animation_body = door.GetComponent<Animator> ();

	}

	// Use this for initialization



	void OnTriggerEnter(Collider other){

		//_animator.SetBool ("close", true);

		animation_body.SetBool ("Open", true);
		animation_body.SetBool ("Close", false);




		Debug.Log ("Object entered the trigger");


	}




	
	// Update is called once per frame
	void Update () {
		
	}
}
