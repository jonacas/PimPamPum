using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrizRecompensa {

    int[][][][][][][][][] matriz;

    public MatrizRecompensa(int[][][][][][][][][] m)
    {
        matriz = m;
    }

	public int GetValor(int[] estado)
    {
		return matriz[estado[GlobalData.FILA]][estado[GlobalData.COLUMNA]][estado[GlobalData.SALUD]]
            [estado[GlobalData.CARGAS]][estado[GlobalData.ESCUDOS]][estado[GlobalData.ENEMIGO_EN_RANGO]]
            [estado[GlobalData.SALUD_ENEMIGO]][estado[GlobalData.ESCUDO_ENEMIGO]][estado[GlobalData.CARGAS_ENEMIGO]];
    }
}
