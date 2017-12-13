using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GestionDeArchivos<T> {

	string path;
	public T objeto;

    public T Objeto
    {
        get
        {
            return objeto;
        }
        set
        {
            objeto = value;
        }
    }


    /// <summary>
    /// Este constrctor intentará abrir el archivo que se le pasa y cargarlo.
    /// Si no existe devuelve un FileNotFoundException
    /// </summary>
    /// <param name="nombreArchivo"> Nombre del archivo que abrira</param>
	public GestionDeArchivos(string nombreArchivo)
	{
		path = System.IO.Path.Combine (Application.streamingAssetsPath, nombreArchivo) + ".cagonTo";

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
		path = System.IO.Path.Combine (Application.streamingAssetsPath, nombreArchivo) + ".cagonTo";
		objeto = obj;
		Guardar ();
	}

	public void Guardar()
	{
        byte[] obj = ObjectToByteArray(objeto);

        BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
        bw.Write(obj);

       /* if (!File.Exists(path))
            File.Create(path);*/
		/*string datosJSon = JsonUtility.ToJson (objeto);
		File.WriteAllText (path, datosJSon);*/
	}

	private void cargar()
	{
		if (File.Exists (path)) {
            objeto = ByteArrayToObject(File.ReadAllBytes(path));
			//objeto = JsonUtility.FromJson<T> (datosJson);
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

    public string GetCadena()
    {
        return JsonUtility.ToJson(objeto);
    }

    byte[] ObjectToByteArray(T obj)
    {
        if (obj == null)
            return null;
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }


    private T ByteArrayToObject(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        //memStream.Seek(0, SeekOrigin.Begin);
        memStream.Position = 0;
        T obj = (T)binForm.Deserialize(memStream);

        return obj;
    }
}
