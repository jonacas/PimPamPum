using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GestionMatrizQ {

	public static GestionDeArchivos<MatrizQ<int[,]>> CrearMatrizQ()
	{
		int[,] matriz = new int[GlobalData.MAX_TURNOS,GlobalData.TOTAL_ACCIONES];

		//inicializamos matriz a cero
		for (int i = 0; i < GlobalData.MAX_TURNOS; i++) {
			for (int j = 0; j < GlobalData.TOTAL_ACCIONES; j++) {
				matriz [i,j] = 0;
			}
		}

		//la ponemos en un soporte y creamos el archivo
		MatrizQ<int[,]> soporteMatriz = new MatrizQ<int[,]>();
		soporteMatriz.Matriz = matriz;
		GestionDeArchivos<MatrizQ<int[,]>> gestorMatriz = new GestionDeArchivos<MatrizQ<int[,]>>("MatrizQ", soporteMatriz);
		return gestorMatriz;
	}
}
