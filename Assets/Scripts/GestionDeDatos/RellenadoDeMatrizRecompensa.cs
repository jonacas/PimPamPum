using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RellenadoDeMatrizRecompensa 
{
    /// <summary>
    /// Crea y guarda una matriz de recompensa.
    /// Devuelve una instancia de GestionDeARchivos con la matriz.
    /// </summary>
    /// <param name="nombreArchivo"></param>
	public static GestionDeArchivos<MatrizRecompensa> CrearRellenarYguardarMatriz(string nombreArchivo)
	{
		int[][][][][][][][] matriz /*= new int[GlobalData.ALTO_TABLERO][GlobalData.ANCHO_TABLERO][GlobalData.VALORES_SALUD]
			[GlobalData.VALORES_CARGAS][GlobalData.VALORES_POWER_UP][GlobalData.VALORES_DISTANCA_ENEMIGO][GlobalData.VALORES_SALUD_ENEMIGO]*/;

		matriz = new int[GlobalData.ALTO_TABLERO][][][][][][][];
		for(int fila = 0; fila < GlobalData.ALTO_TABLERO; fila++)
		{
			matriz [fila] = new int[GlobalData.ANCHO_TABLERO][][][][][][];
			for(int columna = 0; columna < GlobalData.ANCHO_TABLERO; columna++)
			{
				matriz [fila][columna] = new int[GlobalData.VALORES_SALUD][][][][][];
				for (int salud = 0; salud < GlobalData.VALORES_SALUD; salud++) 
				{
					matriz[fila][columna][salud] = new int[GlobalData.VALORES_CARGAS][][][][];
					for(int cargas = 0; cargas < GlobalData.VALORES_CARGAS; cargas++)
					{
						matriz[fila][columna][salud][cargas] = new int[GlobalData.VALORES_POWER_UP][][][];
						for (int powerUp = 0; powerUp < GlobalData.VALORES_POWER_UP; powerUp++) 
						{
							matriz[fila][columna][salud][cargas][powerUp] = new int[GlobalData.VALORES_DISTANCA_ENEMIGO][][];
							for (int distanciaEnemigo = 0; distanciaEnemigo < GlobalData.VALORES_DISTANCA_ENEMIGO; distanciaEnemigo++) 
							{
								matriz[fila][columna][salud][cargas][powerUp][distanciaEnemigo] = new int[GlobalData.VALORES_SALUD_ENEMIGO][];
								for (int saludEnemigo = 0; saludEnemigo < GlobalData.VALORES_SALUD_ENEMIGO; saludEnemigo++) 
								{
									matriz[fila][columna][salud][cargas][powerUp][distanciaEnemigo] = new int[GlobalData.VALORES_SALUD_ENEMIGO][];
									for(int cargasEnemigo = 0; cargasEnemigo < GlobalData.VALORES_CARGA_ENEMIGO; cargasEnemigo++)
									{
										if (saludEnemigo == GlobalData.ENEMIGO_MUERTO) {
											matriz [fila] [columna] [salud] [cargas] [powerUp] [distanciaEnemigo] [saludEnemigo] = 100;
										} else {
											matriz [fila] [columna] [salud] [cargas] [powerUp] [distanciaEnemigo] [saludEnemigo] = (salud - saludEnemigo);
										}
                                    /*print(Convert.ToString(fila) + Convert.ToString(columna) + Convert.ToString(salud) + Convert.ToString(cargas) + 
                                        Convert.ToString(powerUp) + Convert.ToString(distanciaEnemigo) + Convert.ToString(saludEnemigo) + " :" + matriz[fila][columna][salud][cargas][powerUp][distanciaEnemigo][saludEnemigo]);*/
									}
								}
							}
						}
					}
				}
			}
		}
        MatrizRecompensa aux = new MatrizRecompensa(matriz);

        return new GestionDeArchivos<MatrizRecompensa>("MatrizRecompensas", aux);
		/*for(int fila = 0; fila < GlobalData.ALTO_TABLERO; fila++)
		{
			for(int columna = 0; columna < GlobalData.ANCHO_TABLERO; columna++)
			{
				for (int salud = 0; salud < GlobalData.VALORES_SALUD; salud++) 
				{
					for(int cargas = 0; cargas < GlobalData.VALORES_CARGAS; cargas++)
					{
						for (int powerUp = 0; powerUp < GlobalData.VALORES_POWER_UP; powerUp++) 
						{
							for (int distanciaEnemigo = 0; distanciaEnemigo < GlobalData.VALORES_DISTANCA_ENEMIGO; distanciaEnemigo++) 
							{
								for (int saludEnemigo = 0; saludEnemigo < GlobalData.VALORES_SALUD_ENEMIGO; saludEnemigo++) 
								{
									if (saludEnemigo == GlobalData.En) {
										matriz [fila] [columna] [salud] [cargas] [powerUp] [distanciaEnemigo] [saludEnemigo] = 100;
									} else {
										//matriz [fila] [columna] [salud] [cargas] [powerUp] [distanciaEnemigo] [saludEnemigo] = 100;
									}
								}
							}
						}
					}
				}
			}
		}*/
	}


}
