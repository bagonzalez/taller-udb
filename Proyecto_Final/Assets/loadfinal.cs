using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadfinal : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other){

		Application.LoadLevel("finallevel");		
	}
}
