using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShouldLoadLevel : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        Debug.Log("Enter");
        SceneManager.LoadScene(2);
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
}
