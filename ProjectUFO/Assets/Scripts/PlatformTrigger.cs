using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {

    [SerializeField]
    Transform lightObject;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prop")
        {
            lightObject.gameObject.SetActive(true);
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.isKinematic = true;
            scoreManager.Score++;
            Debug.Log("There was a trigger event");
        }


    }
}
