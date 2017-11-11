using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPad : MonoBehaviour {

	public float hoverforce = 12f;

	void OnTriggerEnter(Collider other){
	
		Debug.Log ("Object entered the trigger");	
	}

	void OnTriggerStay(Collider other){



		other.attachedRigidbody.AddForce (Vector3.up * hoverforce, ForceMode.Acceleration);
		Debug.Log ("Object is within trigger");	
	}

	void OnTriggerExit(Collider other){

		Debug.Log ("Object exited the trigger");	
	}

}
