using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    [SerializeField]
    private Transform portalOpener;

    [SerializeField]
    private Transform coinCollector;

    [SerializeField]
    private int triggerAt;

    private int score;
    
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;           
            if (score == triggerAt) {
                portalOpener.gameObject.SetActive(true);
                coinCollector.gameObject.SetActive(true);
            }
        }
    }

    public int Lives
    {
        get
        {
            return lives;
        }

        set
        {
            lives = value;
            if (lives == 0) {
                SceneManager.LoadScene(1);
            }
        }
    }

    private int lives;




    private void Awake()
    {
        Score = 0;
        lives = 4;
    }
}
