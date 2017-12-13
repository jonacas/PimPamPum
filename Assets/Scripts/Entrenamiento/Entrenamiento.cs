using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrenamiento : MonoBehaviour {

	struct Partida
	{
		public int id;
		public E_PlayerMovement j1, j2;
		public bool j1Escudo, j1Ataca, j1Bazoonga;
		public bool j2Escudo, j2Ataca, j2Bazoonga;
		public bool victoriaJ1, victoriaJ2;
		public bool victoria;
		public int turnos;

		public Partida(int id)
		{
			this.id = id;
			j1 = new E_PlayerMovement();
			j2 = new E_PlayerMovement();

			j1Escudo = j1Ataca = j1Bazoonga = false;
			j2Escudo = j2Ataca = j2Bazoonga = false;

			victoriaJ1 = victoriaJ2 = false;
			victoria = false;
			turnos = 0;
		}

        public void NuevoTurno()
        {
            j1Escudo = j1Ataca = j1Bazoonga = false;
			j2Escudo = j2Ataca = j2Bazoonga = false;
        }
	}

	private const bool PERMISO_PARA_REESCRIBIR_MATRICES_1 = false;

	public int numPartidas;
	public int matricesTotales;

	private playerActions accionJ1, accionJ2, accionJ1Anterior;
	Decisionador decisionador;
	GestionDeArchivos<MatrizQ> matQ;
	GestionDeArchivos<MatrizRecompensa> matR;
	private Partida partidaEnCurso;
	private int[] estadoActual, estadoAnterior;

    //para controlar la aparicion del escudo
    private int turnoEscudo1, turnoEscudo2;
    bool powerUpActivo1, powerUpActivo2;

    string debugJ1, debugJ2;

    int totalPartidasARealizar, segmento;

	// Use this for initialization
	void Awake () {

        
		crearMatrices ();
		decisionador = new Decisionador (ModoDecisionador.AlAzar, matQ.objeto);

		estadoActual = new int[GlobalData.TOTAL_INDICES_ARRAY_ESTADO];
		estadoAnterior = new int[GlobalData.TOTAL_INDICES_ARRAY_ESTADO];
        accionJ1 = 0;

        /*matQ = new GestionDeArchivos<MatrizQ>("MatrizQ9");
        for (int i = 0; i < matQ.objeto.Matriz.Length; i++)
        {
            for (int j = 0; j < matQ.objeto.Matriz[0].Length; j++)
            {
                if (matQ.objeto.Matriz[i][j] != 0)
                    print(matQ.objeto.Matriz[i][j]);
            }
        }*/

        StartCoroutine("Entrenar");
        numPartidas = 10000;
        segmento = 10;
	}

	private void crearMatrices()
	{
		if (PERMISO_PARA_REESCRIBIR_MATRICES_1 && GlobalData.PERMISO_PARA_REESCRIBIR_MATRICES_2) {
            Debug.Log("MATRICES CREADAS");
			matQ = GestionMatrizQ.CrearMatrizQ ("MatrizQ0");
			matR = RellenadoDeMatrizRecompensa.CrearRellenarYguardarMatriz ();
		} else {
			Debug.LogError ("SE HA INTENTADO SOBREESCRIBIR LAS MATRICES SIN PERMISO");
		}
	}

	IEnumerator Entrenar()
	{
		//la matriz Q se entrenara desde la perspectiva del j1
		int partidasRealizadas = 1;
		bool accionValida;
        partidaEnCurso = new Partida();
        partidaEnCurso.j1 = new E_PlayerMovement();
        partidaEnCurso.j1.chargues = 3;
        partidaEnCurso.j1.shield = 3;
        partidaEnCurso.j2 = new E_PlayerMovement();
        partidaEnCurso.j2.chargues = 3;
        partidaEnCurso.j2.shield = 3;
        turnoEscudo1 = turnoEscudo2 = 0;

		while (partidasRealizadas < numPartidas) {

            if ((!partidaEnCurso.victoria || !(partidaEnCurso.turnos > 80))  && !comprobarFinPartida())
            {
                //, se guarda la accion, 0 para el primer turno
                 accionJ1Anterior = accionJ1;

                //se prepara para un turno nuevo
                partidaEnCurso.NuevoTurno();

                //se comprueba si alguno tiene el bazoonga
                comprobarBazoongas();

                //comprobamos aparicion del escudo
                aparicionEscudo();

                //se eligne acciones validad para los jugadores SI NO TIENEN BAZOONGA
                if (!partidaEnCurso.j1Bazoonga)
                {
                    accionValida = false;
                    while (!accionValida)
                    {
						accionJ1 = decisionador.decidirMovimiento(estadoActual);
                        accionValida = partidaEnCurso.j1.CheckLegalMove(accionJ1);
                    }
                }

                if (!partidaEnCurso.j2Bazoonga)
                {
                    accionValida = false;
                    while (!accionValida)
                    {
						accionJ2 = decisionador.decidirMovimiento(estadoActual);
                        accionValida = partidaEnCurso.j2.CheckLegalMove(accionJ2);
                    }
                }


                //se guarda el estado que habia antes
                GuardaEstado();
                //se ejecutan las acciones
                ejecutarAcciones();

                partidaEnCurso.turnos++;
                //Debug.Log("Turno " + partidaEnCurso.turnos + " J1 " + debugJ1 + " // J2 " + debugJ2);
                //se obtiene el nuevo estado tras ejecutar las acicones
                SetEstadoActual();
                //se actualiza la matriz q
				GestionMatrizQ.CalcularValorCasilla(matQ.Objeto, matR.Objeto, estadoAnterior, playerActionsToInt(accionJ1Anterior), estadoActual, playerActionsToInt(accionJ1));

                //comprobamos si alguien ha cogio el escudo
                comprobarRecogidaEscudo();

                //print("Valor R: " + matR.Objeto.GetValor(estadoActual) + "// Valor Q: " + matQ.Objeto.Matriz[GestionMatrizQ.calcularFila(estadoActual)][accionJ1]);
                if (partidaEnCurso.j1.life <= 0 || partidaEnCurso.j2.life <= 0)
                    partidaEnCurso.victoria = true;
                
            }
            else
            {
                Debug.Log("Partida " + partidaEnCurso.id + " finalizada");
                partidaEnCurso = new Partida();
                partidaEnCurso.id = ++partidasRealizadas;
                partidaEnCurso.j1 = new E_PlayerMovement();
                partidaEnCurso.j1.chargues = 3;
                partidaEnCurso.j1.shield = 3;
                partidaEnCurso.j2 = new E_PlayerMovement();
                partidaEnCurso.j2.chargues = 3;
                partidaEnCurso.j2.shield = 3;
                turnoEscudo1 = turnoEscudo2 = 0;

                if (partidasRealizadas % 1000 == 0)
                {
                    new GestionDeArchivos<MatrizQ>("MatrizQ" + System.Convert.ToString(partidasRealizadas / 1000), matQ.objeto);
                }

                yield return null;
            }

            if (partidaEnCurso.turnos % 60 == 0)
                yield return null;

		}

	}

    private bool comprobarFinPartida()
    {
        return partidaEnCurso.j1.life == 0 || partidaEnCurso.j2.life == 0;
    }

    private void comprobarRecogidaEscudo()
    {
        if (powerUpActivo1)
        {
            if (partidaEnCurso.j1.posX == 1 && partidaEnCurso.j1.posY == 1)
            {
                //print("J1 coge escudo");
                partidaEnCurso.j1.shield++;
                powerUpActivo1 = false;
                turnoEscudo1 = partidaEnCurso.turnos;
            }
        }

        if (powerUpActivo2)
        {
            if (partidaEnCurso.j2.posX == 1 && partidaEnCurso.j2.posY == 1)
            {
                //print("J2 coge escudo");
                partidaEnCurso.j2.shield++;
                powerUpActivo2 = false;
                turnoEscudo2 = partidaEnCurso.turnos;
            }
        }
    }

    private void aparicionEscudo()
    {
        if (!powerUpActivo1 &&  partidaEnCurso.turnos > turnoEscudo1 + 3)
        {
            if (Random.value > 0.7)
            {
                powerUpActivo1 = true;
            }
        }

        if (!powerUpActivo2 && partidaEnCurso.turnos > turnoEscudo2 + 3)
        {
            if (Random.value > 0.7)
            {
                powerUpActivo2 = true;
            }
        }
    }

    private void comprobarBazoongas()
    {
        if (partidaEnCurso.j1.chargues >= 5)
        {
            partidaEnCurso.j1Bazoonga = true;
			accionJ1 = intToPlayerAction (GlobalData.BAZOONGA);
        }
        if (partidaEnCurso.j2.chargues >= 5)
            partidaEnCurso.j2Bazoonga = true;
		accionJ2 = intToPlayerAction(GlobalData.BAZOONGA);
    }

	private void ejecutarAcciones()
	{
        debugJ1 = "";
        debugJ2 = "";

		switch (playerActionsToInt (accionJ1)) {
		case GlobalData.MOVER_ARRIBA:
                partidaEnCurso.j1.Move(playerActions.MoveUp);
                debugJ1 = "MOVER_ARRIBA";
			break;
        case GlobalData.MOVER_ABAJO:
            partidaEnCurso.j1.Move(playerActions.MoveDown);
            debugJ1 = "MOVER_ABAJO";
            break;
        case GlobalData.MOVER_IZQ:
            partidaEnCurso.j1.Move(playerActions.MoveLeft);
            debugJ1 = "MOVER_IZQ";
            break;
        case GlobalData.MOVER_DER:
            partidaEnCurso.j1.Move(playerActions.MoveRight);
            debugJ1 = "MOVER_DER";
            break;
        case GlobalData.DISPARO:
            partidaEnCurso.j1Ataca = true;
            partidaEnCurso.j1.chargues--;
            debugJ1 = "DISPARO";
            break;
        case GlobalData.ESCUDO_ACCION:
            partidaEnCurso.j1Escudo = true;
            partidaEnCurso.j1.shield--;
            debugJ1 = "ESCUDO_ACCION";
            break;
        case GlobalData.CARGAR_DISPARO:
            partidaEnCurso.j1.Rechargue();
            debugJ1 = "CARGAR_DISPARO";
            break;
        case GlobalData.BAZOONGA:
                //solo si el otro no tiene bazzonga se pone a false
                //esto es para que el otro tambien pueda reaccionar adecuadamente
            if (!partidaEnCurso.j2Bazoonga)
            {
                partidaEnCurso.j2.Damage(3);
                partidaEnCurso.j1Bazoonga = false;
            }
            debugJ1 = "BAZOONGA";
            partidaEnCurso.j1.chargues = 0;
            break;
		}

		switch (playerActionsToInt(accionJ2))
        {
            case GlobalData.MOVER_ARRIBA:
                partidaEnCurso.j2.Move(playerActions.MoveUp);
                debugJ2 = "MOVER_ARRIBA";
                break;
            case GlobalData.MOVER_ABAJO:
                partidaEnCurso.j2.Move(playerActions.MoveDown);
                debugJ2 = "MOVER_ABAJO";
                break;
            case GlobalData.MOVER_IZQ:
                partidaEnCurso.j2.Move(playerActions.MoveLeft);
                debugJ2 = "MOVER_IZQ";
                break;
            case GlobalData.MOVER_DER:
                partidaEnCurso.j2.Move(playerActions.MoveRight);
                debugJ2 = "MOVER_DER";
                break;
            case GlobalData.DISPARO:
                partidaEnCurso.j2Ataca = true;
                partidaEnCurso.j2.chargues--;
                debugJ2 = "DISPARO";
                break;
            case GlobalData.ESCUDO_ACCION:
                partidaEnCurso.j2Escudo = true;
                partidaEnCurso.j2.shield--;
                debugJ2 = "ESCUDO_ACCION";
                break;
            case GlobalData.CARGAR_DISPARO:
                partidaEnCurso.j2.Rechargue();
                debugJ2 = "CARGAR_DISPARO";
                break;
            case GlobalData.BAZOONGA:
                if (!partidaEnCurso.j1Bazoonga)
                    partidaEnCurso.j1.Damage(3);
                partidaEnCurso.j1Bazoonga = false;
                partidaEnCurso.j2Bazoonga = false;
                partidaEnCurso.j2.chargues = 0;
                debugJ2 = "BAZOONGA";
                break;
        }

        //si j1 tiene bazoonga y j2 no, j1 gana
        if (partidaEnCurso.j1Bazoonga && !partidaEnCurso.j2Bazoonga)
        {
            partidaEnCurso.j2.Damage(3);
        }

        //si j1 tiene bazoonga y j2 no, j1 gana
        if (partidaEnCurso.j2Bazoonga && !partidaEnCurso.j1Bazoonga)
        {
            partidaEnCurso.j1.Damage(3);
        }

        //si ataca j1 pero no el 2
        if (partidaEnCurso.j1Ataca && !partidaEnCurso.j2Ataca)
        {
            //si j2 no ha usado el escudo
            if (!partidaEnCurso.j2Escudo)
            {
                partidaEnCurso.j2.Damage();
            }

        }
        //si ataca j2 pero no el 1
        if (partidaEnCurso.j2Ataca && !partidaEnCurso.j1Ataca)
        {
            //si j2 no ha usado el escudo
            if (!partidaEnCurso.j1Escudo)
            {
                partidaEnCurso.j1.Damage();
            }

        }
	}

	private void GuardaEstado()
	{
		for (int i = 0; i < estadoActual.Length; i++) {
			estadoAnterior [i] = estadoActual [i];
		}
	}

	private void SetEstadoActual()
	{
		estadoActual [GlobalData.FILA] = partidaEnCurso.j1.posY;
		estadoActual [GlobalData.COLUMNA] = partidaEnCurso.j1.posX;
        if(partidaEnCurso.j1.life < 0)
            estadoActual[GlobalData.SALUD] = 0;
        else
		    estadoActual [GlobalData.SALUD] = partidaEnCurso.j1.life;
		estadoActual [GlobalData.CARGAS] = partidaEnCurso.j1.chargues;

        if(partidaEnCurso.j1.shield > 0)
		    estadoActual [GlobalData.ESCUDOS] = 1;
        else
            estadoActual[GlobalData.ESCUDOS] = 0;

        if (powerUpActivo1)
            estadoActual[GlobalData.POWER_UP] = 1;
        else
            estadoActual[GlobalData.POWER_UP] = 0;

		if(comprobarSiEstanAlAlcance())
			estadoActual [GlobalData.ENEMIGO_EN_RANGO] = 1;
		else
			estadoActual [GlobalData.ENEMIGO_EN_RANGO] = 0;


        if (partidaEnCurso.j2.life < 0)
            estadoActual[GlobalData.SALUD_ENEMIGO] = 0;
        else
            estadoActual[GlobalData.SALUD_ENEMIGO] = partidaEnCurso.j2.life;


        if (partidaEnCurso.j2.shield > 0)
            estadoActual[GlobalData.ESCUDOS] = 1;
        else
            estadoActual[GlobalData.ESCUDOS] = 0;


		estadoActual [GlobalData.CARGAS_ENEMIGO] = partidaEnCurso.j2.chargues;

        /*print("Estado j1: fila " + estadoActual[GlobalData.FILA] + " //columna" + estadoActual[GlobalData.COLUMNA] + " //salud " + estadoActual[GlobalData.SALUD] + " // cargas " + estadoActual[GlobalData.CARGAS] + " //escudos " + partidaEnCurso.j1.shield);
        print("A tiro " + estadoActual[GlobalData.ENEMIGO_EN_RANGO]);
        print("Estado j2:fila " + partidaEnCurso.j2.posY + " //columna" + partidaEnCurso.j2.posX + " //salud " + estadoActual[GlobalData.SALUD_ENEMIGO] + " // cargas " + estadoActual[GlobalData.CARGAS_ENEMIGO] + " //escudos " + partidaEnCurso.j2.shield);*/
	}

	public bool comprobarSiEstanAlAlcance()
	{
		//esta funcion cuenta las posiciones que seria necesario moverse para llegar a la posicion del otro jugador
		//no hay diagonales
		int distancia = 0;

		distancia += Mathf.Abs (partidaEnCurso.j1.posY - partidaEnCurso.j2.posY);
		distancia += 2 - partidaEnCurso.j1.posX;
		distancia += partidaEnCurso.j2.posX + 1;

		if (distancia <= 3)
			return true;
		else
			return false;
	}

	public playerActions intToPlayerAction(int acc)
	{
		playerActions accion;
		switch (acc) {
		case GlobalData.MOVER_ARRIBA:
			accion = playerActions.MoveUp;
			break;
		case GlobalData.MOVER_ABAJO:
			accion = playerActions.MoveDown;
			break;
		case GlobalData.MOVER_IZQ:
			accion = playerActions.MoveLeft;
			break;
		case GlobalData.MOVER_DER:
			accion = playerActions.MoveRight;
			break;
		case GlobalData.DISPARO:
			accion = playerActions.Shoot;
			break;
		case GlobalData.ESCUDO_ACCION:
			accion = playerActions.Guard;
			break;
		case GlobalData.CARGAR_DISPARO:
		default:
			accion = playerActions.Charge;
			break;
		}
		return accion;
	}


	public int playerActionsToInt(playerActions acc)
	{
		int accion;
		switch (acc) {
		case playerActions.MoveUp:
			accion = GlobalData.MOVER_ARRIBA;
			break;
		case playerActions.MoveDown:
			accion = GlobalData.MOVER_ABAJO;
			break;
		case playerActions.MoveLeft:
			accion = GlobalData.MOVER_IZQ;
			break;
		case playerActions.MoveRight:
			accion = GlobalData.MOVER_DER;
			break;
		case playerActions.Shoot:
			accion = GlobalData.DISPARO;
			break;
		case playerActions.Guard:
			accion = GlobalData.ESCUDO_ACCION ;
			break;
		case playerActions.Charge:
		default:
			accion = GlobalData.CARGAR_DISPARO;
			break;
		}
		return accion;
	}
}
