using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoporteDatosNoModificable<T>{

	private T datos;

	public T Datos {
		get{
			return datos;
		}
		set{
			Debug.LogError ("Se intento modificar a una matriz invariable nomas wey");
		}
	}

	public SoporteDatosNoModificable(T objeto)
	{
		datos = objeto;
	}
}
