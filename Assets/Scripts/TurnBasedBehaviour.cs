using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedBehaviour : MonoBehaviour {

	private int currentTurn;

	//Gestion de partida: turnos, gameflow...

	public PlayerMovement humanoReferencia;
	public PlayerMovement IAReferencia;

	public GameObject modeladoEscudo;
	private bool activeHumanShield = false;
	private bool activeIAShield = false;

	//Hacer la funcion que hace la foto.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (humanoReferencia.shield == 0 && !activeHumanShield ) 
		{
			spawneoEscudo (1);
			activeHumanShield = true;
		}
		if (IAReferencia.shield == 0 && !activeIAShield) 
		{
			spawneoEscudo (2);
			activeIAShield = true;
		}

		CheckGameOver ();
	}

	public void spawneoEscudo(int lado)
	{
		if (lado == 1) 
		{
			//Haz aparecer el escudo.
		}
		if (lado == 2) 
		{
			//Haz aparecer el escudo.
		}
		
	}

	public void CheckGameOver()
	{
		if (humanoReferencia.Life <= 0) 
		{
			Debug.Log ("LA IA HA GANADO!!!");
			humanoReferencia.DisableActionButtons ();
		}
		else if (IAReferencia.Life <= 0) 
		{
			Debug.Log ("el jugador ha ganado!!!");
			humanoReferencia.DisableActionButtons ();
		}
	}



	//Haz una funcion para que detecte la colision del escudo
	//generado, y entonces resetea el booleano correspondiente.

	public int[] GetCurrentGameState()
	{
		int shieldAux = 0;
		int rangeAux = 0;
		if (activeHumanShield) 
		{
			shieldAux = 1;
		}
		if (humanoReferencia.distanceToEnemy < 7.5f) 
		{
			rangeAux = 1;
		}
		int[] gameState = new int[]
		{
			humanoReferencia.posX, //Añadir la fila del playerMovement,
			humanoReferencia.posY, //Añadir la columna del playerMovement,
			humanoReferencia.Life, //Añadir la vida del jugador segun el PlayerMovement,
			humanoReferencia.chargues, //Añadir el numero de cargas del jugador.
			humanoReferencia.shield, //Añadir el nº de escudos del jugador segun el PlayerMovement,
			shieldAux, //Añadir si el powerup esta activo,
			rangeAux,//Añadir si el enemigo esta al alcance,
			IAReferencia.Life,//Añadir la salud del enemigo,
			IAReferencia.shield,//Añadir el numero de escudos del enemigo,
			IAReferencia.chargues//Añadir el numero de cargas del enemigo
		};

		return gameState;


	}


}
