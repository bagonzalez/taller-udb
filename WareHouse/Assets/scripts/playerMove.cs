using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

	private Rigidbody2D player;
	private bool isJumping;
	private Animator myAnimator;
	private int horizontal;
	private int vertical;

	void Awake(){
		player = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}


	void FixedUpdate () {
		
		if (Input.GetAxisRaw ("Vertical") == 1) {
			player.MovePosition (new Vector2 (player.position.x, player.position.y + 0.1f));
			myAnimator.SetInteger ("vertical", 1);

		} else if (Input.GetAxisRaw ("Vertical") == -1) {
			player.MovePosition (new Vector2 (player.position.x, player.position.y - 0.1f));
			myAnimator.SetInteger ("vertical", -1);

		} else if (Input.GetAxisRaw ("Horizontal") == 1) {
			player.MovePosition (new Vector2 (player.position.x + 0.1f, player.position.y));
			myAnimator.SetInteger ("horizontal", 1);

		} else if (Input.GetAxisRaw ("Horizontal") == -1) {
			player.MovePosition (new Vector2 (player.position.x - 0.1f, player.position.y));
			myAnimator.SetInteger ("horizontal", -1);
		} else {
			myAnimator.SetInteger ("horizontal", 0);
			myAnimator.SetInteger ("vertical", 0);
		}

	}
}
