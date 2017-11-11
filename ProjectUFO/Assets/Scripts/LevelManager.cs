using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelOne() {
        SceneManager.LoadScene(1);
    }

    public void LevelTwo() {
        SceneManager.LoadScene(2);
    }

    public void DeleteSaves() {
        PlayerPrefs.DeleteAll();
    }
}
