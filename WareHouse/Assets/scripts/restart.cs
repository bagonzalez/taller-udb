using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {

	public GameObject MenuPanel;
	public GameObject LevelSelectPanel;

	void Start()
	{

	}

	public void InitGame(){
		SceneManager.LoadScene ("mainMenu", LoadSceneMode.Single);
	}
}
