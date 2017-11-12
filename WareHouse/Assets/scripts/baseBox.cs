using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class baseBox : MonoBehaviour {

	private bool active = false;
	public static int baseBoxesaActivate = 0;
	private AudioSource source;


	void Awake (){
		source = GetComponent<AudioSource> ();
	}

	void Start () {

	}


	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "box" && !active){
			Debug.Log("Ok");
			active = true;
			baseBoxesaActivate += 1;
			source.Play ();
		}	
	}
}
