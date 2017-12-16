using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnBasedBehaviour : MonoBehaviour {

    private const float TIEMPO_ENTRE_PULSACIONES = 0.3F;

    enum EstadoLoop
    {
        turnoJugador, turnoIa
    }


	private int currentTurn;

	//Gestion de partida: turnos, gameflow...

	public GameObject modeladoEscudo;
	private bool activeHumanShield = false;
	private bool activeIAShield = false;

    private playerActions accionJugador, accionIa;
    private PlayerMovement jugador, IA;
    private bool accionJgadorRealizada, accionIARealizada;
    private GameObject botones;
    private GestionDeArchivos<MatrizQ> matQ;
    private Decisionador decisionadorIA;

    private bool bazoongaJugador, bazoongaIA;
    private bool ataqueJugador, ataqueIA;

	public Text gameOverText;

	//Hacer la funcion que hace la foto.

	// Use this for initialization
	void Start () {

        matQ = new GestionDeArchivos<MatrizQ>("MatrizQ" + System.Convert.ToString(GlobalData.Dificultad));

        if (GlobalData.Dificultad >= 7)
        {
            decisionadorIA = new Decisionador(ModoDecisionador.ProPlayer, matQ.objeto);
        }

        else if (GlobalData.Dificultad >= 4)
        {
            decisionadorIA = new Decisionador(ModoDecisionador.Normal, matQ.objeto);
        }
        else
        {
            decisionadorIA = new Decisionador(ModoDecisionador.Manco, matQ.objeto);
        }


        jugador = GameObject.Find("PlayerHuman").GetComponent<PlayerMovement>();
        IA = GameObject.Find("PlayerIA").GetComponent<PlayerMovement>();
        botones = GameObject.Find("Buttons");
        StartCoroutine("MainLoop");
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

	public bool CheckGameOver()
	{
		if (jugador.Life <= 0) 
		{
			Debug.Log ("LA IA HA GANADO!!!");
			gameOverText.text = "OMAE WA MO SHINDEIRU";
			StartCoroutine("MostrarVictoriaYVolverAlMenu");
            return true;
		}
		else if (IA.Life <= 0) 
		{
			Debug.Log ("el jugador ha ganado!!!");
			gameOverText.text = "HAS GANADO, INCREÍBLE!!!";
            StartCoroutine("MostrarVictoriaYVolverAlMenu");
            return true;
		}

        return false;
	}

    public void AparicionEscudo()
    {
        if (jugador.shield == 0 && !activeHumanShield && Random.value > 0.7f)
        {
            spawneoEscudo(1);
            activeHumanShield = true;
        }
        if (IA.shield == 0 && !activeIAShield && Random.value > 0.7f)
        {
            spawneoEscudo(2);
            activeIAShield = true;
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
			IA.posX, //Añadir la fila del playerMovement,
			IA.posY, //Añadir la columna del playerMovement,
			IA.Life, //Añadir la vida del jugador segun el PlayerMovement,
			IA.chargues, //Añadir el numero de cargas del jugador.
			IA.shield, //Añadir el nº de escudos del jugador segun el PlayerMovement,
			shieldAux, //Añadir si el powerup esta activo,
			rangeAux,//Añadir si el enemigo esta al alcance,
			jugador.Life,//Añadir la salud del enemigo,
			jugador.shield,//Añadir el numero de escudos del enemigo,
			jugador.chargues//Añadir el numero de cargas del enemigo
		};

		return gameState;


	}


    public bool comprobarSiEstanAlAlcance()
    {
        //esta funcion cuenta las posiciones que seria necesario moverse para llegar a la posicion del otro jugador
        //no hay diagonales
        int distancia = 0;

        distancia += Mathf.Abs(IA.posY - jugador.posY);
        distancia += 2 - IA.posX;
        distancia += jugador.posX + 1;

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
        if (player.CheckLegalMove(accion))
        {
            player.EjecutarAccion(accion);
            return true;
        }

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

    IEnumerator MostrarVictoriaYVolverAlMenu()
    {
		jugador.canvasReference.GameOverScreen ();
		yield return new WaitForSeconds (5.0f);

		SceneManager.LoadScene (0);

        yield return null;
    }

    IEnumerator MainLoop()
    {
        EstadoLoop estado = EstadoLoop.turnoJugador;
        bool victoria = false;
        accionJgadorRealizada = false;

        print(jugador.Life + " " + IA.Life);
        //mientras no haya victoria
        while (!victoria)
        {
            AparicionEscudo();
            victoria = CheckGameOver();

            //le toca al jugador
            if (estado == EstadoLoop.turnoJugador)
            {
                //si puede lanzar el bazoonga, lo lanza
                if (comprobarYEjecutarAccion(playerActions.Bazoonga, jugador))
                {
                    bazoongaJugador = true;
                    estado = EstadoLoop.turnoIa;
                    continue;
                }

                //si aun no ha pusado un boton, esperamos
                if (!accionJgadorRealizada)
                    yield return null;
                //si lo ha pulsado, volvemos al estado anterior y le cedemos el turno a la IA
                else
                {
                    accionJgadorRealizada = false;
                    estado = EstadoLoop.turnoIa;
                }

            }
            else
            {
                currentTurn++;
                //si puede lanzar el bazoonga, lo lanza
                if (comprobarYEjecutarAccion(playerActions.Bazoonga, IA))
                {
                    bazoongaIA = true;
                    accionIARealizada = true;
                }

                //elegimos acciones hasta encontrar una legal
                accionIa = decisionadorIA.decidirMovimiento(GetCurrentGameState());
                accionIARealizada = comprobarYEjecutarAccion(accionIa, IA);


                while (!accionIARealizada)
                {
                    accionIa = decisionadorIA.decidirMovimiento(GetCurrentGameState());
                    accionIARealizada = comprobarYEjecutarAccion(accionIa, IA);
                }


                //comprobamos danos y escudos
                comprobarInteracciones();


                //devolvemos turno al jugador
                estado = EstadoLoop.turnoJugador;
                print(accionJugador + "/" + accionIa);
            }

        }


    }

    private void comprobarInteracciones()
    {
        //si el jugador tiene bazoonga
        if (bazoongaJugador)
        {
            //si la IA no lo tiene
            if (!bazoongaIA)
            {
                IA.Damage(3);
                return;
            }
        }

        //si la IA tiene bazoonga
        if (bazoongaIA)
        {
            jugador.Damage(3);
            return;
        }



        //si el jugador dispara
        if (accionJugador == playerActions.Shoot)
        {
            //...y la ia no se protege o dispara, sufre dano
            if (accionIa != playerActions.Guard && accionIa != playerActions.Shoot)
                IA.Damage();
        }

        //si la ia dispara
        if (accionIa == playerActions.Shoot)
        {
            //...y el jugador no se protege o dispara, sufre dano
            if (accionJugador != playerActions.Guard && accionJugador!= playerActions.Shoot)
                jugador.Damage();
        }

        //comprobamos si han cogido escudo
        if (jugador.posX == 1 && jugador.posY == 1 && activeHumanShield)
        {
            jugador.shield = 3;
            activeHumanShield = false;
        }

        if (IA.posX == 1 && IA.posY == 1 && activeIAShield)
        {
            jugador.shield = 3;
            activeIAShield = false;
        }
    }

    #region HANDLERS BOTONES
    public void OnClickAttack()
    {
        accionJugador = playerActions.Shoot;
        accionJgadorRealizada =  comprobarYEjecutarAccion(accionJugador, jugador);
        StartCoroutine("DesactivarBotonesTemporalmente");
    }

    public void OnClickDefense()
    {
        accionJugador = playerActions.Guard;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, jugador);
        StartCoroutine("DesactivarBotonesTemporalmente");
    }

    public void OnClickChargue()
    {
        accionJugador = playerActions.Charge;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, jugador);
        StartCoroutine("DesactivarBotonesTemporalmente");
    }

    public void OnClickMoveUp()
    {
        accionJugador = playerActions.MoveUp;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, jugador);
        StartCoroutine("DesactivarBotonesTemporalmente");
    }

    public void OnClickMoveDown()
    {
        accionJugador = playerActions.MoveDown;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, jugador);
        print("MoverArriba" + accionJgadorRealizada);
        StartCoroutine("DesactivarBotonesTemporalmente");
    }

    public void OnClickMoveLeft()
    {
        accionJugador = playerActions.MoveLeft;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, jugador);
        StartCoroutine("DesactivarBotonesTemporalmente");
    }

    public void OnClickMoveRight()
    {
        accionJugador = playerActions.MoveRight;
        accionJgadorRealizada = comprobarYEjecutarAccion(accionJugador, jugador);
        StartCoroutine("DesactivarBotonesTemporalmente");
    }

    #endregion
}
