﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebasGestionArchivos : MonoBehaviour {

	public string path;
	public int[] objeto;

	GestionDeArchivos<MatrizPruebas> ges;
	// Use this for initialization
	void Awake () {
		ges = new GestionDeArchivos<MatrizPruebas> ("prueba", new MatrizPruebas());
		path = ges.GetPath ();
		ges.GetObjeto ().matrizPruebas = new int[5] { 0, 1, 2, 3, 4 };
		ges.Guardar ();
		objeto = ges.GetObjeto ().matrizPruebas;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}