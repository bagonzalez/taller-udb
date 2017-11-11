using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelTrigger : MonoBehaviour {
    [SerializeField]
    private Transform second_level_trigger;

    private void OnTriggerEnter(Collider other)
    {
        
       second_level_trigger.gameObject.SetActive(true);
    }
}
