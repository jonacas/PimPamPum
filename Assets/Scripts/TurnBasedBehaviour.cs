using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedBehaviour : MonoBehaviour {

    private const float TIEMPO_ENTRE_PULSACIONES = 0.5F;

	private int currentTurn;

	//Gestion de partida: turnos, gameflow...

	public PlayerMovement humanoReferencia;
	public PlayerMovement IAReferencia;

	public GameObject modeladoEscudo;
	private bool activeHumanShield = false;
	private bool activeIAShield = false;

    private playerActions accionJugador, accionIa;
    private PlayerMovement jugador, IA;
    private bool accionJgadorRealizada;
    private GameObject botones;

	//Hacer la funcion que hace la foto.

	// Use this for initialization
	void Start () {
        jugador = GameObject.Find("PlayerHuman").GetComponent<PlayerMovement>();
        IA = GameObject.Find("PlayerIA").GetComponent<PlayerMovement>();
        botones = GameObject.Find("Buttons");
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
        if (comprobarSiEstanAlAlcance())
            rangeAux = 1;
        else
            rangeAux = 0;
		int[] gameState = new int[]
		{
			IAReferencia.posX, //Añadir la fila del playerMovement,
			IAReferencia.posY, //Añadir la columna del playerMovement,
			IAReferencia.Life, //Añadir la vida del jugador segun el PlayerMovement,
			IAReferencia.chargues, //Añadir el numero de cargas del jugador.
			IAReferencia.shield, //Añadir el nº de escudos del jugador segun el PlayerMovement,
			shieldAux, //Añadir si el powerup esta activo,
			rangeAux,//Añadir si el enemigo esta al alcance,
			humanoReferencia.Life,//Añadir la salud del enemigo,
			humanoReferencia.shield,//Añadir el numero de escudos del enemigo,
			humanoReferencia.chargues//Añadir el numero de cargas del enemigo
		};

		return gameState;


	}


    public bool comprobarSiEstanAlAlcance()
    {
        //esta funcion cuenta las posiciones que seria necesario moverse para llegar a la posicion del otro jugador
        //no hay diagonales
        int distancia = 0;

        distancia += Mathf.Abs(IAReferencia.posY - humanoReferencia.posY);
        distancia += 2 - IAReferencia.posX;
        distancia += humanoReferencia.posX + 1;

        if (distancia <= 3)
            return true;
        else
            return false;
    }


    /// <summary>
    /// Comrpueba si una accion es legal y si lo es, la ejecuta
    /// </summary>
    /// <param name="accion">Accion que va a testearse</param>
    /// <param name="player">Jugador que debe ejecutarla</param>
    /// <returns>True si se ha ejecutado, false si no lo ha hecho</returns>
    private bool comprobarYEjecutarAccion(playerActions accion, PlayerMovement player)
    {
        return false;
    }



    IEnumerator DesactivarBotonesTemporalmente()
    {
        float tiempoActivado = Time.time;
        foreach (Transform child in botones.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }

        while (Time.time - tiempoActivado < TIEMPO_ENTRE_PULSACIONES)
        {
            yield return null;
        }

        foreach (Transform child in botones.transform)
        {
            child.GetComponent<Button>().interactable = true;
        }
    }

    #region HANDLERS BOTONES
    public void OnClickAttack()
    {
        accionJugador = playerActions.Shoot;
        accionJgadorRealizada =  comprobarYEjecutarAccion(accionJugador, IA);
    }

    public void OnClickDefense()
    {
        accionJugador = playerActions.Guard;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, IA);
    }

    public void OnClickChargue()
    {
        accionJugador = playerActions.Charge;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, IA);
    }

    public void OnClickMoveUp()
    {
        accionJugador = playerActions.MoveUp;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, IA);
    }

    public void OnClickMoveDown()
    {
        accionJugador = playerActions.MoveDown;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, IA);
    }

    public void OnClickMoveLeft()
    {
        accionJugador = playerActions.MoveLeft;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, IA);
    }

    public void OnClickMoveRight()
    {
        accionJugador = playerActions.MoveRight;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, IA);
    }

    #endregion
}
