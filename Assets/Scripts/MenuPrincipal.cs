using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour {

	Text textoValorDif;
	Slider sliderDif;

	// Use this for initialization
	void Awake () {
		textoValorDif = GameObject.Find ("TextoValorDificultad").GetComponent<Text>();
		sliderDif = GameObject.Find ("SliderDificultad").GetComponent<Slider>();

	}
	
	void Update()
	{
		textoValorDif.text = System.Convert.ToString(sliderDif.value);
		GlobalData.Dificultad = System.Convert.ToInt32(sliderDif.value);
	}
}
