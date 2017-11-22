using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedBehaviour : MonoBehaviour {

	private int currentTurn;
	private int TOTAL_NUMBER_OF_TURNS = 30;

	public PlayerMovement referenceMovementPlayer1;
	public PlayerMovement referenceMovementPlayer2;

	//Gestion de partida: turnos, gameflow...

	//Hacer la funcion que hace la foto.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		gamePaceManager ();		
	}

	void gamePaceManager()
	{
		
	}



}
