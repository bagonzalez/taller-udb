using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalsePlatformTrigger : MonoBehaviour {

  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            DestroyPlatform();
        }
        
    }

    private void DestroyPlatform() {
        Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
    }
}
