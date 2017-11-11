using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1 : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other){

		Application.LoadLevel("level1");		
	}
}
