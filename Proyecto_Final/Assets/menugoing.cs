using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menugoing : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other){

		Application.LoadLevel("Menu");		
	}
}
