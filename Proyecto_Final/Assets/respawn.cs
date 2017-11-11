using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour {


	[SerializeField] private Transform player;
	[SerializeField] private Transform respawnPoint;


	// Use this for initialization
	void OnTriggerEnter(Collider other){


		player.transform.position = respawnPoint.transform.position;
				
	}
}
