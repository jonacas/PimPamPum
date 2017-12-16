using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public TurnBasedBehaviour TurnManagerReference;


	public Text numberOfTurns;
	public Slider playerLifesCanvas;
	public Slider IALifesCanvas;

	public GameObject gameOverScreen;

	// Use this for initialization
	void Start () 
	{
		gameOverScreen.SetActive (false);	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void colorPlayerLifesCanvas(int livesMatter, Slider lifeSlider)
	{
		switch (livesMatter) 
		{
		case 3: 
			{
				lifeSlider.fillRect.gameObject.GetComponent<Image> ().color = Color.green;
				lifeSlider.value = 3;
				break;
			}
		case 2:
			{
				lifeSlider.fillRect.gameObject.GetComponent<Image> ().color = Color.yellow;
				lifeSlider.value = 2;
				break;
			}
		case 1:
			{
				lifeSlider.fillRect.gameObject.GetComponent<Image> ().color = Color.red;
				lifeSlider.value = 1;
				break;
			}
		case 0:
			{
				lifeSlider.value = 0;
				break;
			}
		}


	}

	public void UpdateTurnNumberCanvas(int totalTurns)
	{
		numberOfTurns.text = "Turno Actual: \n" + totalTurns;
	}

	public void GameOverScreen()
	{
		gameOverScreen.SetActive (true);	
	}





}
