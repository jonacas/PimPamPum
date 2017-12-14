﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public string horizontal;
    public string vertical;
    public GameObject rival;
    public Transform[] fila1 = new Transform[3];
    public Transform[] fila2 = new Transform[3];
    public Transform[] fila3 = new Transform[3];
    private Transform[,] positions = new Transform[3,3];
	public int posX = 1;
	public int posY = 1;
    private float timer = 0.1f;
	private int life;
    public int shield;
	public int chargues;
    private bool defense = false;

	public bool myTurn = true;
	public bool legalMove; // SE ACTUALIZA EN MOVE, AUNQUE ESTA DEUELVA VOID
	public CanvasManager canvasReference;
	public Slider lifeSlider;
	public int totalTurns = 0;
    
	public float distanceToEnemy;

	public bool IAPhase;
	public bool IAMovementAllowed = false;

	public float timerBetweenTurns = 0.0F;
	public float targetTimeBetweenTurns = 3.0f;
	public Button[] ActionButtons;

    private Decisionador decisionadorIA;
    private GestionDeArchivos<MatrizQ> matQ;
    private playerActions accionIA;


	public int Life 
	{
		get {
			return life;
		}
	}

	
    // Use this for initialization
    void Start () {

        life = 3;
        shield = 3;
        chargues = 0;
        for (int i = 0; i < fila1.Length; i++) {

            positions[0, i] = fila1[i];
            positions[1, i] = fila2[i];
            positions[2, i] = fila3[i];

        }
	}


	void Move(int move) { //Funcion para mover al personaje


		switch ( move /*movement*/) 
		{
		case 1:    //playerActions.MoveDown:
			{
				posY = posY + 1;	
				break;
			}
        case 2:      //playerActions.MoveLeft:
			{
				posX = posX - 1;
				break;
			}
        case 3:        //playerActions.MoveRight:
			{
				posX = posX + 1;
				break;
			}
        case 4: //playerActions.MoveUp:
			{
				posY = posY - 1; //Cambiado para tenerlo en cuenta, va en base a la matriz
				break;
			}
        default:
            {
                print("error");
                break;
            }
		}
    }

    void Bazoonga() {

        chargues = 0;


    }
    void Attack() {

        
        chargues = chargues - 1;

    }

    void Rechargue() {
        chargues = chargues + 1;
       
    }
    public void Damage(int damage) {
		
        if (!defense)
        {
            life = life - damage;
            
        }
		if (canvasReference != null && lifeSlider != null) 
		{
			canvasReference.colorPlayerLifesCanvas (life, lifeSlider);
		}       
    }

    public void Defense() {

        shield = shield - 1;
    }

	public void EjecutarAccion(playerActions action)
	{
		switch (action) 
		{
		case playerActions.Charge:
			{
                    Rechargue();break;
			}
		case playerActions.Guard:
			{
                    Defense();break;
			}
		case playerActions.Shoot:
			{
                    Attack();break;
			}
		case playerActions.MoveDown:
			{
                    Move(1);break;
			}
		case playerActions.MoveLeft:
			{
                    Move(2); break;
                }
		case playerActions.MoveUp:
			{
                    Move(4); break;
                }
		case playerActions.MoveRight:
			{
                    Move(3); break;
                }
		}
	}

    public bool CheckLegalMove(playerActions move)
    {
        switch (move)
        {
            case playerActions.MoveUp:
                if(posY <= 0)
                    return false;
                break;
            case playerActions.MoveDown:
                if (posY >= 2)
                    return false;
                break;
            case playerActions.MoveLeft:
                if (posX <= 0)
                    return false;
                break;
            case playerActions.MoveRight:
                if (posX >= 2)
                    return false;
                break;
            case playerActions.Shoot:
                if (chargues <= 0)
                    return false;
                break;
            case playerActions.Guard:
                if (shield >= 0)
                    return false;
                break;
        }

        return true;
    }

}
