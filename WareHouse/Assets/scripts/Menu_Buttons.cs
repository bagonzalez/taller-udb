﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour {

	public GameObject MenuPanel;
	public GameObject LevelSelectPanel;
	
	void Start()
	{
		
	}

	public void InitGame(){
		SceneManager.LoadScene ("floor1", LoadSceneMode.Single);
	}
}
