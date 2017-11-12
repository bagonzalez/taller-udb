using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finLevel : MonoBehaviour {

	public int numberBoxes = 0;
	public string scene = "";

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			Debug.Log("Ok");
			int baseBoxesaActivate = baseBox.baseBoxesaActivate;
			if (numberBoxes == baseBoxesaActivate) {
				Debug.Log("Complete");
				baseBox.baseBoxesaActivate = 0;
				SceneManager.LoadScene (scene, LoadSceneMode.Single);

			}
		}	
	}
}
