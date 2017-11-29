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
	}

	private const bool PERMISO_PARA_REESCRIBIR_MATRICES_1 = true;

	public int numPartidas;
	public int matricesTotales;

	private int accionJ1, accionJ2, accionJ1Anterior;
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
		bool accionValida;

		while (partidasRealizadas < numPartidas) {

			if (!partidaEnCurso.victoria) {
				accionJ1Anterior = accionJ1Anterior;
				accionJ1 = decisionador.decidirMovimiento ();
				accionJ2 = decisionador.decidirMovimiento ();
				GuardaEstado ();
				ejecutarAcciones ();

			}

		}
		yield return null;
	}

	private void ejecutarAcciones()
	{
		switch (accionJ1) {
		case GlobalData.MOVER_ARRIBA:
			break;
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
		if(comprobarSiEstanAlAlcance())
			estadoActual [GlobalData.ENEMIGO_EN_RANGO] = 1;
		else
			estadoActual [GlobalData.ENEMIGO_EN_RANGO] = 0;
		estadoActual [GlobalData.SALUD_ENEMIGO] = partidaEnCurso.j2.life;
		estadoActual [GlobalData.ESCUDO_ENEMIGO] = partidaEnCurso.j2.shield;
		estadoActual [GlobalData.CARGAS_ENEMIGO] = partidaEnCurso.j2.chargues;
	}

	public bool comprobarSiEstanAlAlcance()
	{
		//esta funcion cuenta las posiciones que seria necesario moverse para llegar a la posicion del otro jugador
		//no hay diagonales
		int distancia = 0;

		distancia += Mathf.Abs (partidaEnCurso.j1.posX - partidaEnCurso.j2.posX);
		distancia += 2 - partidaEnCurso.j1.posY;
		distancia += partidaEnCurso.j2.posY + 1;

		if (distancia <= 3)
			return true;
		else
			return false;
	}

}
