using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrizRecompensa {

    int[][][][][][][][] matriz;

    public MatrizRecompensa(int[][][][][][][][] m)
    {
        matriz = m;
    }

	public int GetValor(int fila, int columna, int salud, int cargas, int powerUp, int distanciaEnemigo, int saludEnemigo, int cargasEnemigo)
    {
        return matriz[fila][columna][salud][cargas][powerUp][distanciaEnemigo][saludEnemigo][cargasEnemigo];
    }
}
