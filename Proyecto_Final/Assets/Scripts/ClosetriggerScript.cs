using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetriggerScript : MonoBehaviour {
	private Animator animation_body;
	//private Rigidbody player_body;

	// Use this for initialization
	void Awake(){
		
		animation_body = GetComponent<Animator> ();
		//player_body = GetComponent<Rigidbody> ();
	}

	
	void OnTriggerEnter(Collider other){

		//_animator.SetBool ("close", true);



		Debug.Log ("Object entered the trigger");

		
	}

	void OnTriggerStay(Collider other){

		animation_body.SetBool("Close", true);




		Debug.Log ("Object is within trigger");	
	}

	void OnTriggerExit(Collider other){

		Debug.Log ("Object exited the trigger");	
	}
}
