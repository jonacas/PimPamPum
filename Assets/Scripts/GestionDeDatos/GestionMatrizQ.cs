using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GestionMatrizQ {

	public static float ratioAprendizaje = 0.5f;
	public static float factorDescuento = 0.5f;


	//maxQ = mejor valor fila del estado al que movemos

	public static GestionDeArchivos<MatrizQ<float[,]>> CrearMatrizQ()
	{
		float[,] matriz = new float[GlobalData.TOTAL_ESTADOS,GlobalData.TOTAL_ACCIONES];

		//inicializamos matriz a cero
		for (int i = 0; i < GlobalData.TOTAL_ESTADOS; i++) {
			for (int j = 0; j < GlobalData.TOTAL_ACCIONES; j++) {
				matriz [i,j] = 0;
			}
		}

		//la ponemos en un soporte y creamos el archivo
		MatrizQ<float[,]> soporteMatriz = new MatrizQ<float[,]>();
		soporteMatriz.Matriz = matriz;
		GestionDeArchivos<MatrizQ<float[,]>> gestorMatriz = new GestionDeArchivos<MatrizQ<float[,]>>("MatrizQ", soporteMatriz);
		return gestorMatriz;
	}


	/// <summary>
	/// Calcula el valor de la casilla Q correspondiente
	/// </summary>
	/// <returns>Valor de la fila actual de la matriz Q</returns></returns>
	/// <param name="mat">Matriz sobre la que se haran los calculos</param>
	/// <param name="fila">Fila actual de la matriz</param>
	/// <param name="columna">Columna en la que se esta (última acción realizada)</param>
	/// <param name="columnaDestino">Accion que se ha realizado (columna de la matriz a la que se va)</param>
	/// <param name="estado">Array con el conjunto de parametros que definen el estado ordenados segun los indices de GlobalData</param>
	public static int CalcularValorCasilla(MatrizQ<float[,]> mat, int fila, int columna, int columnaDestino, int[] estado)
	{
		int filaDestino = calcularFila (estado);

		mat.Matriz [filaDestino, columnaDestino] = ratioAprendizaje *
		((/*MatrizRecompensa +*/ factorDescuento * buscarMaximoEnFila (mat, filaDestino, GlobalData.TOTAL_ACCIONES))
				- mat.Matriz [fila, columna]);

		return filaDestino;
	}

	private static float buscarMaximoEnFila(MatrizQ<float[,]> mat, int fila, int columnas)
	{
		float maximo = float.NegativeInfinity;
		for(int i = 0; i < columnas; i++)
		{
			if (mat.Matriz[fila, i] > maximo)
				maximo = mat.Matriz [fila, i];
		}
		return maximo;
	}

	private static int calcularFila(int[] estado)
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
