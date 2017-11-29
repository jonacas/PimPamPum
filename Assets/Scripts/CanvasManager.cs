using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public TurnBasedBehaviour TurnManagerReference;


	public Text numberOfTurns;
	public Slider playerLifesCanvas;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void colorPlayerLifesCanvas(int livesMatter)
	{
		switch (livesMatter) 
		{
		case 3: 
			{
				playerLifesCanvas.fillRect.gameObject.GetComponent<Image> ().color = Color.green;
				playerLifesCanvas.value = 3;
				break;
			}
		case 2:
			{
				playerLifesCanvas.fillRect.gameObject.GetComponent<Image> ().color = Color.yellow;
				playerLifesCanvas.value = 2;
				break;
			}
		case 1:
			{
				playerLifesCanvas.fillRect.gameObject.GetComponent<Image> ().color = Color.red;
				playerLifesCanvas.value = 1;
				break;
			}
		case 0:
			{
				playerLifesCanvas.value = 0;
				break;
			}
		}


	}

	public void UpdateTurnNumberCanvas(int totalTurns)
	{
		numberOfTurns.text = "Turno Actual: \n" + totalTurns;
	}


}
