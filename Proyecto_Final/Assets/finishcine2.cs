using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishcine2 : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other){

		Application.LoadLevel("level2");		
	}

}
