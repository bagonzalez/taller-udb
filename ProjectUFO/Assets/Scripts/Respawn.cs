using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour {

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private Text lives;

    private void Awake()
    {
        lives.text = "Vidas: 4" ; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.position = respawnPoint.transform.position;
            scoreManager.Lives--;
            lives.text = "Vidas: "+ scoreManager.Lives.ToString();
        }

       
    }
}
