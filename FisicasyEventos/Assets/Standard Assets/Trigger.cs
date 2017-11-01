using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	// Use this for initialization

	void OnTriggerEnter(Collider other){


		Debug.Log ("trigger enter");
		if (gameObject.name == "destruir1") {
		
			Destroy (gameObject);
		}
		if (gameObject.name == "destruir2") {

			Destroy (gameObject);
		}

	


	}

}
