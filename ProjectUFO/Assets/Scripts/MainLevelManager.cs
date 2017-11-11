using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour {
    [SerializeField]
    private Transform level_two;

    [SerializeField]
    private Transform level_three;

    [SerializeField]
    private Transform level_four;

    [SerializeField]
    private Transform game_finished;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level_Two")) {
            level_two.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("Level_Three"))
        {
            level_three.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("Level_Four"))
        {
            level_four.gameObject.SetActive(true);
        }

        if (PlayerPrefs.HasKey("Game_Finished"))
        {
            game_finished.gameObject.SetActive(true);
        }



    }

    // Update is called once per frame
    void Update () {        
        
    }
}
