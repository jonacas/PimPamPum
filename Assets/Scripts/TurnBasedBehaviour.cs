using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedBehaviour : MonoBehaviour {

	public bool ReadyPlayerOne = true;
	public bool ReadyPlayerTwo = false;

	private int currentTurn;
	private int TOTAL_NUMBER_OF_TURNS = 30;

	public PlayerMovement referenceMovementPlayer1;
	public PlayerMovement referenceMovementPlayer2;

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
		if (ReadyPlayerOne) // Es turno pra jugador uno
		{
			
			print ("Fin de turno, it seems");
		}


	}



}
