using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nextlevel : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other){

		Application.LoadLevel("cinematicalevel2");	
	}

}
