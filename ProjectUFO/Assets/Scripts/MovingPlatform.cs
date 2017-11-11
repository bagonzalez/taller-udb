using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    [SerializeField]
    private Transform movingPlatform;

    [SerializeField]
    private Transform position1;

    [SerializeField]
    private Transform position2;

    [SerializeField]
    private float smoothing;

    [SerializeField]
    private float resetingTime;

    private Vector3 newPosition;

    private string currentState;


	// Use this for initialization
	void Start () {
        currentState = "";
        ChangeTarget();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smoothing * Time.deltaTime);
	}

    void ChangeTarget() {
        if (currentState == "Moving to 1")
        {
            currentState = "Moving to 2";
            newPosition = position2.position;
        }
        else if (currentState == "Moving to 2")
        {
            currentState = "Moving to 1";
            newPosition = position1.position;
        }
        else if (currentState == "") {
            currentState = "Moving to 2";
            newPosition = position2.position;
        }
        Invoke("ChangeTarget", resetingTime);
    }
}
