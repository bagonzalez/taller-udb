using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalsePlatformRestorer : MonoBehaviour {

    [SerializeField]
    private Transform[] platforms;

    [SerializeField]
    private Transform[] respawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {            
            for (int i = 0; i < platforms.Length; i++)
            {
                Rigidbody rigidBody = platforms[i].gameObject.GetComponent<Rigidbody>();
                rigidBody.isKinematic = true;                
                platforms[i].transform.position = respawnPoints[i].transform.position;
                platforms[i].transform.rotation = respawnPoints[i].rotation;
                
            }
        }


    }
}
