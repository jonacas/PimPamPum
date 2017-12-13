using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebasGestionArchivos : MonoBehaviour {

	public string path;
	public int[] objeto;
    GestionDeArchivos<MatrizRecompensa> aux;
	GestionDeArchivos<MatrizQ> matrizQ;

	GestionDeArchivos<float[][]> ges;
	// Use this for initialization
	void Awake () {
		//ges = new GestionDeArchivos<MatrizPruebas> ("prueba"/*, new MatrizPruebas()*///);
        float[][] matriz = new float[20][];
        float[][] matrizB;

        //inicializamos matriz a cero
        for (int i = 0; i < 20; i++)
        {
            matriz[i] = new float[5];
            for (int j = 0; j < 5; j++)
            {
                matriz[i][j] = 0f;
            }
        }

        ges = new GestionDeArchivos<float[][]>("aaa", matriz);
        print(ges.objeto);
        print(ges.objeto[0][0]);
		/*path = ges.GetPath ();
		ges.GetObjeto ().matrizPruebas = new int[5] { 0, 1, 2, 3, 4 };
		ges.Guardar ();
		objeto = ges.GetObjeto ().matrizPruebas;
        aux = RellenadoDeMatrizRecompensa.CrearRellenarYguardarMatriz("MatrizRecomoensa");
		matrizQ = GestionMatrizQ.CrearMatrizQ ();*/
	}
}
