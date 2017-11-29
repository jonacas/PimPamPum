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
		public int turnos;

		public Partida(int id)
		{
			this.id = id;
			j1 = new E_PlayerMovement();
			j2 = new E_PlayerMovement();

			j1Escudo = j1Ataca = j1Bazoonga = false;
			j2Escudo = j2Ataca = j2Bazoonga = false;

			victoriaJ1 = victoriaJ2 = false;
			turnos = 0;
		}
	}

	private const bool PERMISO_PARA_REESCRIBIR_MATRICES_1 = true;

	public int numPartidas;
	public int matricesTotales;

	Decisionador decisionador;
	GestionDeArchivos<MatrizQ<float[,]>> matQ;
	GestionDeArchivos<MatrizRecompensa> matR;
	private Partida partidaEnCurso;
	private int[] estadoActual, estadoAnterior;

	// Use this for initialization
	void Awake () {
		decisionador = new Decisionador ();
		crearMatrices ();
		estadoActual = new int[GlobalData.TOTAL_INDICES_ARRAY_ESTADO];
		estadoAnterior = new int[GlobalData.TOTAL_INDICES_ARRAY_ESTADO];
	}

	private void crearMatrices()
	{
		if (PERMISO_PARA_REESCRIBIR_MATRICES_1 && GlobalData.PERMISO_PARA_REESCRIBIR_MATRICES_2) {
			matQ = GestionMatrizQ.CrearMatrizQ ("MatrizQ00");
			matR = RellenadoDeMatrizRecompensa.CrearRellenarYguardarMatriz ();
		} else {
			Debug.LogError ("SE HA INTENTADO SOBREESCRIBIR LAS MATRICES SIN PERMISO");
		}
	}

	IEnumerator Entrenar()
	{
		//la matriz Q se entrenara desde la perspectiva del j1

		int partidasRealizadas = 0;

		while (partidasRealizadas < numPartidas) {


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
		estadoActual [GlobalData.FILA] = partidaEnCurso.j1.posX;
		estadoActual [GlobalData.COLUMNA] = partidaEnCurso.j1.posY;
		estadoActual [GlobalData.SALUD] = partidaEnCurso.j1.life;
		estadoActual [GlobalData.CARGAS] = partidaEnCurso.j1.chargues;
		estadoActual [GlobalData.ESCUDOS] = partidaEnCurso.j1.shield;
		estadoActual [GlobalData.ENEMIGO_EN_RANGO] = partidaEnCurso.j1.posY;

	}

	public bool comprobarSiEstanAlAlcance()
	{
		//esta funcion cuenta las posiciones que seria necesario moverse para llegar a la posicion del otro jugador
		//no hay diagonales
		int distancia;

		distancia += Mathf.Abs (partidaEnCurso.j1.posX - partidaEnCurso.j2.posX);
		distancia += 2 - partidaEnCurso.j1.posY;
		distancia += partidaEnCurso.j2.posY + 1;

		if (distancia <= 3)
			return true;
		else
			return false;
	}

}
