using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarObjeto : MonoBehaviour 
{
	// En esta variable configuraremos la velocidad a la que se moverá el objeto
	public float Velocidad = 5.0F;

	private bool derecha = false;
	private bool izquierda = false;
	private bool arriba = false;
	private bool abajo = false;
	private bool aumentar = false;
	private bool disminuir = false;

	private Rigidbody2D player;
	private bool isJumping;
	private Animator myAnimator;
	private int horizontal;
	private int vertical;

	void Awake(){
		player = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}


	void Update ()
	{
		if(derecha)
		{
			// Movemos el objeto hacia la derecha
			player.MovePosition (new Vector2 (player.position.x + 0.1f, player.position.y));
			myAnimator.SetInteger ("horizontal", 1);
		}

		if (izquierda)
		{
			// Movemos el objeto hacia la izquierda
			player.MovePosition (new Vector2 (player.position.x - 0.1f, player.position.y));
			myAnimator.SetInteger ("horizontal", -1);
		}

		if (arriba)
		{
			// Movemos el objeto hacia arriba
			player.MovePosition (new Vector2 (player.position.x, player.position.y + 0.1f));
			myAnimator.SetInteger ("vertical", 1);
		}

		if (abajo)
		{
			// Movemos el objeto hacia abajo
			player.MovePosition (new Vector2 (player.position.x, player.position.y - 0.1f));
			myAnimator.SetInteger ("vertical", -1);
		}


	}

	/******************** FUNCIONES PÚBLICAS ********************/

	public void MoverDerecha()
	{
		derecha = true;
	}

	public void MoverIzqda()
	{
		izquierda = true;
	}

	public void MoverArriba()
	{
		arriba = true;
	}

	public void MoverAbajo()
	{
		abajo = true;
	}

	public void stop(){
		derecha = false;
		izquierda = false;
		arriba = false;
		abajo = false;
		myAnimator.SetInteger ("horizontal", 0);
		myAnimator.SetInteger ("vertical", 0);
	}
}