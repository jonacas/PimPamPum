using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ModoDecisionador
{
	AlAzar, Manco, Normal, ProPlayer
}

public class Decisionador{

	private struct Pareja
	{
		public float valor;
		public playerActions accion;

		public Pareja(float val, int acc)
		{
			valor = val;

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
		}
	}

	public const int AL_AZAR = 0;

	ModoDecisionador metodo;
	MatrizQ matQ;
	float probMejor, probEntreMejores;


	/// <summary>
	/// Crea una instancia de decisionador que usara el metodo que se le proporciones
	/// </summary>
	/// <param name="modo">Modo.</param>
	/// <param name="matQ">Matriz sobre la que se haran los calculos</param>
	public Decisionador(ModoDecisionador modo, MatrizQ matQ)
	{
		this.matQ = matQ;
		SetMetodo (modo);
	}

	public playerActions decidirMovimiento(int[] estado)
	{
		Pareja[] mejores;
		int fila = calcularFila (estado);

		mejores = GenerarArrayMejores (fila);
		if (Random.value < probMejor)
			return mejores [0].accion;
		else if (Random.value < probEntreMejores)
			return mejores[Random.Range(0, mejores.Length - 1)].accion;
		else
			return new Pareja(0, Random.Range(0, GlobalData.TOTAL_ACCIONES - 1 /*bazzonga no se elige*/)).accion;

	}

	public void SetMetodo(ModoDecisionador metodo)
	{
		this.metodo = metodo;

		switch (metodo) {
		case ModoDecisionador.AlAzar:
		default:
			probMejor = -1f;
			probEntreMejores = -1f;
			break;

		case ModoDecisionador.Manco:
			//elige al azar entre los cuatro mejores con una probabilidad grande de elegir entre todos al azar
			probMejor = -1f;
			probEntreMejores = 0.5f;
			break;

		case ModoDecisionador.Normal:
			//a veces elige el mejor, pero suele elegir al azar entre los cuatro mejores con una probabilidad pequeña de hacer un movimiento aleatorio
			probMejor = 0.3f;
			probEntreMejores = 0.7f;
			break;

		case ModoDecisionador.ProPlayer:
			//escoge el mejor movimiento con frecuencia, entre los tres mejores con menor frecuencia y totalmente al azar casi nunca
			probMejor = 0.7f;
			probEntreMejores = 0.7f;
			break;
		}
	}

	/// <summary>
	/// Generars the array mejores.
	/// </summary>
	/// <returns>The array mejores.</returns>
	/// <param name="fila">Fila.</param>
	/// <param name="length">Length.</param>
	private Pareja[] GenerarArrayMejores(int fila, int length = 4)
	{
		Pareja[] valores = new Pareja[length];

		for (int i = 0; i < GlobalData.TOTAL_ACCIONES; i++) {
			ComprobarIAnadir (matQ.Matriz [fila] [i], ref valores, i);
		}
		return valores;
	}

	private void ComprobarIAnadir(float valor, ref Pareja[] valores, int columna)
	{
		//hacemos burbuja de manera que vaya de mayor al menor
		Pareja actual = new Pareja(valor, columna);
		Pareja aux;
		for (int i = 0; i < valores.Length; i++) {
			if (valores [i].valor <= actual.valor) {
				aux = valores [i];
				valores [i] = actual;
				actual = aux;
			}
		}
	}



	private int calcularFila(int[] estado)
	{
		int filaDestino = 0;
		//se suma la aportacion de cada variable al valor de la fila
		filaDestino += estado [GlobalData.FILA] * GlobalData.ANCHO_TABLERO * GlobalData.VALORES_SALUD * GlobalData.VALORES_CARGAS *
			GlobalData.VALORES_ESCUDO * GlobalData.VALORES_DISTANCA_ENEMIGO * GlobalData.VALORES_SALUD_ENEMIGO *
			GlobalData.VALORES_ESCUDO_ENEMIGO * GlobalData.VALORES_CARGA_ENEMIGO;

		filaDestino += estado [GlobalData.COLUMNA] * GlobalData.VALORES_SALUD * GlobalData.VALORES_CARGAS *
			GlobalData.VALORES_ESCUDO * GlobalData.VALORES_DISTANCA_ENEMIGO * GlobalData.VALORES_SALUD_ENEMIGO *
			GlobalData.VALORES_ESCUDO_ENEMIGO * GlobalData.VALORES_CARGA_ENEMIGO;

		filaDestino += estado [GlobalData.SALUD] * GlobalData.VALORES_CARGAS *
			GlobalData.VALORES_ESCUDO * GlobalData.VALORES_DISTANCA_ENEMIGO * GlobalData.VALORES_SALUD_ENEMIGO *
			GlobalData.VALORES_ESCUDO_ENEMIGO * GlobalData.VALORES_CARGA_ENEMIGO;

		filaDestino += estado [GlobalData.CARGAS] * GlobalData.VALORES_ESCUDO * GlobalData.VALORES_DISTANCA_ENEMIGO * GlobalData.VALORES_SALUD_ENEMIGO *
			GlobalData.VALORES_ESCUDO_ENEMIGO * GlobalData.VALORES_CARGA_ENEMIGO;

		filaDestino += estado [GlobalData.ESCUDOS] * GlobalData.VALORES_DISTANCA_ENEMIGO * GlobalData.VALORES_SALUD_ENEMIGO *
			GlobalData.VALORES_ESCUDO_ENEMIGO * GlobalData.VALORES_CARGA_ENEMIGO;

		filaDestino += estado [GlobalData.ENEMIGO_EN_RANGO] * GlobalData.VALORES_SALUD_ENEMIGO *
			GlobalData.VALORES_ESCUDO_ENEMIGO * GlobalData.VALORES_CARGA_ENEMIGO;

		filaDestino += estado [GlobalData.SALUD_ENEMIGO] * GlobalData.VALORES_ESCUDO_ENEMIGO * GlobalData.VALORES_CARGA_ENEMIGO;

		filaDestino += estado [GlobalData.ESCUDO_ENEMIGO] * GlobalData.VALORES_CARGA_ENEMIGO;

		filaDestino += estado [GlobalData.CARGAS_ENEMIGO];

		return filaDestino;
	}
}
