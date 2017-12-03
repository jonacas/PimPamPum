using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GestionMatrizQ {

	public static float ratioAprendizaje = 0.5f;
	public static float factorDescuento = 0.5f;


	//maxQ = mejor valor fila del estado al que movemos

	public static GestionDeArchivos<MatrizQ<float[][]>> CrearMatrizQ(string nombre)
	{
		float[][] matriz = new float[GlobalData.TOTAL_ESTADOS][];

		//inicializamos matriz a cero
		for (int i = 0; i < GlobalData.TOTAL_ESTADOS; i++) {
            matriz[i] = new float[GlobalData.TOTAL_ACCIONES];
			for (int j = 0; j < GlobalData.TOTAL_ACCIONES; j++) {
				matriz [i][j] = 0f;
			}
		}

		//la ponemos en un soporte y creamos el archivo
		MatrizQ<float[][]> soporteMatriz = new MatrizQ<float[][]>();
		soporteMatriz.Matriz = matriz;
		GestionDeArchivos<MatrizQ<float[][]>> gestorMatriz = new GestionDeArchivos<MatrizQ<float[][]>>(nombre, soporteMatriz);
		return gestorMatriz;
	}


	/// <summary>
	/// Calcula el valor de la casilla Q correspondiente.
    /// Los estados deben proporcionarse en forma de array ordenados segun los indices de GlobalData
	/// </summary>
	/// <returns>Valor de la fila futura de la matriz Q. (la que representa el nuevo estado en dicha matriz)</returns>
	/// <param name="mat">Matriz sobre la que se haran los calculos</param>
	/// <param name="matR">Matriz de recompensa</param>
    /// <param name="estadoAnterior">Estado antes de realizar la accion</param>
    /// <param name="accionAnterior">Ultima acción realizada</param>
    /// <param name="estadoFuturo">Estado al que se llega</param>
    /// <param name="accionFutura">Acción que se va a realizar para llegar al nuevo estado</param>
	public static int CalcularValorCasilla(MatrizQ<float[][]> mat, MatrizRecompensa matR, int[] estadoAnterior, int accionAnterior,int[] estadoFuturo, int accionFutura)
	{
        int filaOrigen = calcularFila(estadoAnterior);
		int filaDestino = calcularFila (estadoFuturo);

        mat.Matriz[filaDestino][accionFutura] = mat.Matriz[filaOrigen][accionAnterior] + ratioAprendizaje *
		(matR.GetValor(estadoFuturo) + factorDescuento * buscarMaximoEnFila (mat, filaDestino, GlobalData.TOTAL_ACCIONES)
				- mat.Matriz [filaOrigen][accionAnterior]);

		return filaDestino;
	}

	private static float buscarMaximoEnFila(MatrizQ<float[][]> mat, int fila, int columnas)
	{
		float maximo = float.NegativeInfinity;
		for(int i = 0; i < columnas; i++)
		{
			if (mat.Matriz[fila][ i] > maximo)
				maximo = mat.Matriz [fila][i];
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
