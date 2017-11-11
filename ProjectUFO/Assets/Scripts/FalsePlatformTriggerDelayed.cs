using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalsePlatformTriggerDelayed : MonoBehaviour {
    [SerializeField]
    private float destroyingTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                        
            Invoke("DestroyPlatform",destroyingTime);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CancelInvoke("DestroyPlatform");
            DestroyPlatform();
        }
    }

    private void DestroyPlatform()
    {
        Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        
    }
}
