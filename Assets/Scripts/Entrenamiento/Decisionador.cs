using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decisionador{

	public const int AL_AZAR = 0;

	int metodo;

	public Decisionador(int m = 0)
	{
		metodo = m;
	}

	public int decidirMovimiento()
	{
		switch (metodo) {
			
		case AL_AZAR:
		default:
			return Random.Range(0, GlobalData.TOTAL_ACCIONES);

		}

	}

	public void SetMetodo(int metodo)
	{
		this.metodo = metodo;
	}
}
