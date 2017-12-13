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
	public static GestionDeArchivos<MatrizRecompensa> CrearRellenarYguardarMatriz(string nombreArchivo = "MatrizRecompensas")
	{
		int[][][][][][][][][][] matriz /*= new int[GlobalData.ALTO_TABLERO][GlobalData.ANCHO_TABLERO][GlobalData.VALORES_SALUD]
			[GlobalData.VALORES_CARGAS][GlobalData.VALORES_POWER_UP][GlobalData.VALORES_DISTANCA_ENEMIGO][GlobalData.VALORES_SALUD_ENEMIGO]*/;

		matriz = new int[GlobalData.ALTO_TABLERO][][][][][][][][][];
		for(int fila = 0; fila < GlobalData.ALTO_TABLERO; fila++)
		{
			matriz [fila] = new int[GlobalData.ANCHO_TABLERO][][][][][][][][];
			for(int columna = 0; columna < GlobalData.ANCHO_TABLERO; columna++)
			{
				matriz [fila][columna] = new int[GlobalData.VALORES_SALUD][][][][][][][];
				for (int salud = 0; salud < GlobalData.VALORES_SALUD; salud++) 
				{
					matriz[fila][columna][salud] = new int[GlobalData.VALORES_CARGAS][][][][][][];
					for(int cargas = 0; cargas < GlobalData.VALORES_CARGAS; cargas++)
					{
						matriz[fila][columna][salud][cargas] = new int[GlobalData.VALORES_ESCUDO][][][][][];
						for (int escudos = 0; escudos < GlobalData.VALORES_ESCUDO; escudos++) 
						{
							matriz[fila][columna][salud][cargas][escudos] = new int[GlobalData.VALORES_POWER_UP][][][][];
							for (int powerUp = 0; powerUp < GlobalData.VALORES_ESCUDO; powerUp++) 
							{
								matriz[fila][columna][salud][cargas][escudos][powerUp] = new int[GlobalData.VALORES_DISTANCA_ENEMIGO][][][];
								for (int distanciaEnemigo = 0; distanciaEnemigo < GlobalData.VALORES_DISTANCA_ENEMIGO; distanciaEnemigo++) 
								{
									matriz[fila][columna][salud][cargas][escudos][powerUp][distanciaEnemigo] = new int[GlobalData.VALORES_SALUD_ENEMIGO][][];
									for (int saludEnemigo = 0; saludEnemigo < GlobalData.VALORES_SALUD_ENEMIGO; saludEnemigo++) 
									{
										matriz[fila][columna][salud][cargas][escudos][powerUp][distanciaEnemigo][saludEnemigo] = new int[GlobalData.VALORES_ESCUDO_ENEMIGO][];
										for(int escudoEnemigo = 0; escudoEnemigo < GlobalData.VALORES_ESCUDO_ENEMIGO; escudoEnemigo++)
										{
											matriz[fila][columna][salud][cargas][escudos][powerUp][distanciaEnemigo][saludEnemigo][escudoEnemigo] = new int[GlobalData.VALORES_CARGA_ENEMIGO];
											for (int cargasEnemigo = 0; cargasEnemigo < GlobalData.VALORES_CARGA_ENEMIGO; cargasEnemigo++) {
	                                            
												matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = 0;

												//si el personaje ha muerto
												if (salud == 0) {
													//si hay empate
													if (saludEnemigo == 0)
														matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = -50;
													else
														matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = -100;
												}
												//si el enemigo ha muerto
												else if (saludEnemigo == 0) {
													matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = 100;
												} 
												//si obtiene el bazoonga
												else if (cargas == GlobalData.VALORES_CARGA_ENEMIGO - 1) {
													//si el enemigo no lo obtiene
													if (cargasEnemigo != GlobalData.VALORES_CARGA_ENEMIGO - 1)
														matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = 100;
													else
														matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = -10;
												}
												//si recoge una carga de escudo
												else if (fila == 1 && columna == 1 && powerUp == 1) {
													matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = 10;
												}

												//si esta a tiro sin tener escudos
												else if(distanciaEnemigo == 1 && escudos < 1)
													matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = -2;
												//resto de casos
	                                            else {
														matriz [fila] [columna] [salud] [cargas] [escudos] [powerUp] [distanciaEnemigo] [saludEnemigo] [escudoEnemigo] [cargasEnemigo] = (salud - saludEnemigo) * 2 /*+ (cargas - cargasEnemigo)*/;
												}


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
		}
        MatrizRecompensa aux = new MatrizRecompensa(matriz);

		return new GestionDeArchivos<MatrizRecompensa>(nombreArchivo, aux);
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
