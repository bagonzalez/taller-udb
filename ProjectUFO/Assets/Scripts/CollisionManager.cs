using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("There is a collision between" + gameObject.name + " and " + collision.collider.name);

        if (gameObject.tag == "Destructable") {
            Destroy(gameObject);
        }

        
    }


    private void OnCollisionExit(Collision collision)
    {
        
    }

}
