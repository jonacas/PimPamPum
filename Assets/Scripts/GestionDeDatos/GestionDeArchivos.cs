using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GestionDeArchivos<T> {

	string path;
	T objeto;

    public T Objeto
    {
        get;
        set;
    }


    /// <summary>
    /// Este constrctor intentará abrir el archivo que se le pasa y cargarlo.
    /// Si no existe devuelve un FileNotFoundException
    /// </summary>
    /// <param name="nombreArchivo"> Nombre del archivo que abrira</param>
	public GestionDeArchivos(string nombreArchivo)
	{
		path = System.IO.Path.Combine (Application.streamingAssetsPath, nombreArchivo);

		if (File.Exists (path)) {
			cargar ();
		} else {
			throw new FileNotFoundException ();
			//File.Create (path);
		}
		//objeto = null;
	}

    /// <summary>
    /// Este constructor crea un n uevo archivo en el disco duro con el nombre que se le proporciona
    /// y guarda el objeto que se le pasa como argumento
    /// SI HAY UN ARCHIVO CON EL MISMO NOMBRE SERA SOBREESCRITO
    /// </summary>
    /// <param name="nombreArchivo"> Nombre que tendrá el archivo en disco</param>
    /// <param name="obj">Objeto que se guardara en el archivo</param>
	public GestionDeArchivos(string nombreArchivo, T obj)
	{
		path = System.IO.Path.Combine (Application.streamingAssetsPath, nombreArchivo);
		objeto = obj;
		Guardar ();
	}

	public void Guardar()
	{
       /* if (!File.Exists(path))
            File.Create(path);*/
		string datosJSon = JsonUtility.ToJson (objeto);
		File.WriteAllText (path, datosJSon);
	}

	private void cargar()
	{
		if (File.Exists (path)) {
			string datosJson = File.ReadAllText (path);
			objeto = JsonUtility.FromJson<T> (datosJson);
		} else
			throw new FileNotFoundException ();
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
