using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoporteDatosModificable<T>{

	T datos;
	public T Datos {
		get;
		set;
	}
		
	public SoporteDatosModificable(T d)
	{
		datos = d;
	}


}
