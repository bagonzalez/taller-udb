using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel : MonoBehaviour {

	void OnTriggerEnter(Collider other){

		Application.LoadLevel("cinematicafinal");	
	}

}
