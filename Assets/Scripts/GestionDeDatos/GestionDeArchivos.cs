using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDeArchivos<T> {

	string path;
	T objeto;

	public GestionDeArchivos(string nombreArchivo)
	{
		path = System.IO.Path.Combine (Application.streamingAssetsPath, nombreArchivo);
		cargar ();
		//objeto = null;
	}

	public GestionDeArchivos(string nombreArchivo, T obj)
	{
		path = System.IO.Path.Combine (Application.streamingAssetsPath, nombreArchivo);
		objeto = obj;
	}

	public void Guardar()
	{

	}

	private void cargar()
	{


	}

	public string GetPath()
	{
		return path;
	}

	public T GetObjeto()
	{
		return objeto;
	}
}
