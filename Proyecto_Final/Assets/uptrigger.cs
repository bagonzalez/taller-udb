using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uptrigger : MonoBehaviour {

	// Use this for initialization
	public GameObject cube;

	private Animator animation_body;
	//private Rigidbody player_body;

	void Awake(){

		animation_body = cube.GetComponent<Animator> ();

	}

	// Use this for initialization



	void OnTriggerEnter(Collider other){

		//_animator.SetBool ("close", true);

		animation_body.SetBool ("up", true);





		Debug.Log ("Object entered the trigger");


	}
}
