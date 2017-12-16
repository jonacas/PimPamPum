using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

	Text textoValorDif;
	Slider sliderDif;
	bool alreadyStarted = false;

	// Use this for initialization
	void Awake () {
		textoValorDif = GameObject.Find ("TextoValorDificultad").GetComponent<Text>();
		sliderDif = GameObject.Find ("SliderDificultad").GetComponent<Slider>();


	}

	void Start()
	{
	}
	
	void Update()
	{
		textoValorDif.text = System.Convert.ToString(sliderDif.value);
		GlobalData.Dificultad = System.Convert.ToInt32(sliderDif.value);
	}

	public void IniciarPartida()
	{
		if (!alreadyStarted) 
		{
			SceneManager.LoadSceneAsync (1).allowSceneActivation = true;
			alreadyStarted = true;
		}
	}





}
