using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_TurnBasedBehaviour : MonoBehaviour {

	private int currentTurn;

	//Gestion de partida: turnos, gameflow...

	public GameObject[] playerReferences;


	//Hacer la funcion que hace la foto.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		gamePaceManager ();		

		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			Debug.Log ("Es turno del jugador 1");
			enableTurn (0);
			disableTurn (1);
		}
		if (Input.GetKeyDown (KeyCode.F2)) 
		{
			Debug.Log ("Es turno del jugador 2");
			enableTurn (1);
			disableTurn (0);
		}


	}

	void gamePaceManager()
	{
		// Primero, pediremos los inputs de cada jugador.
		// Después, calcularemos el peso que tiene la acción del enemigo
		//siguiendo las variables correspondientes.
		// Por último, actualizaremos estado actual de los jugadores.
		// Si en esta actualizacion hay Game Over (se acaban los turnos o alguien muere)
		// Se pasa a pantalla de fin de partida.


	}


	public void enableTurn(int jugador)
	{
		playerReferences [jugador].GetComponent<PlayerMovement> ().myTurn = true;
	}

	public void disableTurn(int jugador)
	{
		playerReferences [jugador].GetComponent<PlayerMovement> ().myTurn = false;
	}


}
