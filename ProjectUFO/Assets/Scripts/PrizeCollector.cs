using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrizeCollector : MonoBehaviour {

    [SerializeField]
    private AudioClip coinSound;

    [SerializeField] 
    private AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {

        

        int current = SceneManager.GetActiveScene().buildIndex;
        if (current == 1)
        {
            if (!PlayerPrefs.HasKey("Level_Two"))
            {
                audioSource.PlayOneShot(coinSound);
                PlayerPrefs.SetString("Level_Two", "YES");
                PlayerPrefs.SetInt("Score", 500);
                PlayerPrefs.Save();
                gameObject.SetActive(false);
                Debug.Log(PlayerPrefs.GetString("Level_Two"));

            }
            else
            {
                audioSource.PlayOneShot(coinSound);
                Debug.Log(PlayerPrefs.GetString("Level_Two"));
                gameObject.SetActive(false);


            }
        }

        if (current == 2) {
            if (!PlayerPrefs.HasKey("Level_Three"))
            {
                audioSource.PlayOneShot(coinSound);
                PlayerPrefs.SetString("Level_Three", "YES");
                PlayerPrefs.SetInt("Score", 1000);
                PlayerPrefs.Save();
                gameObject.SetActive(false);
                Debug.Log(PlayerPrefs.GetString("Level_Three"));

            }
            else
            {
                audioSource.PlayOneShot(coinSound);
                Debug.Log(PlayerPrefs.GetString("Level_Three"));
                gameObject.SetActive(false);
            }
        }

        if (current == 3) {
            if (!PlayerPrefs.HasKey("Level_Four"))
            {
                audioSource.PlayOneShot(coinSound);
                PlayerPrefs.SetString("Level_Four", "YES");
                PlayerPrefs.SetInt("Score", 5000);
                PlayerPrefs.Save();
                gameObject.SetActive(false);
                Debug.Log(PlayerPrefs.GetString("Level_Four"));

            }
            else {
                audioSource.PlayOneShot(coinSound);
                Debug.Log(PlayerPrefs.GetString("Level_Four"));
                gameObject.SetActive(false);

            }
        }

        if (current == 4) {
            if (!PlayerPrefs.HasKey("Game_Finished"))
            {
                audioSource.PlayOneShot(coinSound);
                PlayerPrefs.SetString("Game_Finished", "YES");
                PlayerPrefs.SetInt("Score", 5000);
                PlayerPrefs.Save();
                gameObject.SetActive(false);
                

            }
            else
            {
                audioSource.PlayOneShot(coinSound);
                Debug.Log(PlayerPrefs.GetString("Game_Finished"));
                gameObject.SetActive(false);

            }
        }
        
        
    }
}
