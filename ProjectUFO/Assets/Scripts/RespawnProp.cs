using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnProp : MonoBehaviour {

    [SerializeField]
    private Transform[] propBodies;

    [SerializeField]
    private Transform[] respawnPoint;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prop" || other.tag == "Player")
        {
            for (int i = 0; i < propBodies.Length; i++) {
                if (other.gameObject == propBodies[i].gameObject)
                {
                    Rigidbody rigidbody = other.GetComponent<Rigidbody>();
                    rigidbody.velocity = Vector3.zero;
                    other.transform.position = respawnPoint[i].transform.position;
                }
            }
        }

        


    }
}
