using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnProp : MonoBehaviour {

    [SerializeField]
    private Transform prop;

    [SerializeField]
    private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prop")
        {
            prop.transform.position = respawnPoint.transform.position;
        }


    }
}
