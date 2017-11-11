using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShouldLoadLevel : MonoBehaviour {
    [SerializeField]
    private int loadLevel;

    void OnTriggerEnter(Collider other) {
        Debug.Log("Enter");
        /**int current = SceneManager.GetActiveScene().buildIndex;

        if(current == 1)
        {
            SceneManager.LoadScene(2);
        }

        if (current == 2) {
            SendToMain();
        }

        if (current == 3) {
            SendToMain();
        }**/

        SceneManager.LoadScene(loadLevel);
    }

    private void SendToMain() {
        SceneManager.LoadScene(1);
    }
    
}
