using System.Collections;
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

		IAPhase = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (myTurn) {
			//RandomCalculation ();
			}
				timer = timer + Time.deltaTime;			

		distanceToEnemy = Vector3.Distance(rival.transform.position, transform.position);

    }

	public void RandomCalculation()
	{
	if (timer >= 1.0f) 
	{
		float random = Random.value;

		if( random < 0.4f)
		{
			if (random < 0.1f) 
			{
                Move(1);//playerActions.MoveUp);
			} 
			else if (random < 0.2f) 
			{
                Move(2); //playerActions.MoveLeft);
			}
			else if (random < 0.3f) 
			{
                Move(3); //(playerActions.MoveRight);
			} 
			else
			{
                Move(4); //(playerActions.MoveDown);
			}

		}
		else if( random < 0.6f)
		{
			defense = false;
			Attack ();
		}
		else if( random < 0.8f)
		{
			defense = false;
			Rechargue ();

		}
		else
		{
            Defense();

		}

		timer = 0.0f;
	}
}

	public void UpdateNumberOfTurns()
	{
		totalTurns = totalTurns + 1;
		if (canvasReference != null) 
		{
			canvasReference.UpdateTurnNumberCanvas (totalTurns);
		}
		IAPhase = true;
		//AÑADIR AQUI EL LANZAMIENTO DEL MOVIMIENTO DE LA IA

		//AQUI FINALIZA EL LANZAMIENTO DEL MOVIMIENTO DE LA IA
		IAPhase = false;

	}

	public void Move(int move) {


		switch ( move /*movement*/) 
		{
		case 1:    //playerActions.MoveDown:
			{
				posY = posY - 1;	
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
				posY = posY + 1;
				break;
			}
        default:
            {
                print("error");
                break;
            }
		}
       // posX = posX + (int)(Input.GetAxisRaw(horizontal));
       // posY = posY + (int)-(Input.GetAxisRaw(vertical));
		legalMove = true;
		if (posX < 0) 
		{ posX = 0; legalMove = false; /*RandomCalculation ()*/ }
		else if (posX > 2) 
		{ posX = 2; legalMove = false; /*RandomCalculation ()*/ }
		if (posY < 0)
		{ posY = 0; legalMove = false;/*RandomCalculation ()*/}
        else if (posY > 2)
		{ posY = 2; legalMove = false; /*RandomCalculation ()*/ }
        transform.position = positions[posX, posY].position;
		if (legalMove) 
		{
			legalMove = true;
			if (!IAPhase) {
				UpdateNumberOfTurns ();
			}
		}



    }
    public void Attack() {

		if (chargues <= 0) {
			//RandomCalculation ();
		} 
		else
		{
			 
			print("Distancia: " + distanceToEnemy); 
			if(chargues > 0 && distanceToEnemy <= 7.5f )
			{
				if (chargues == 5)
				{
					rival.GetComponent<PlayerMovement> ().Damage (2);
					chargues = 0;
				}
				else
				{
					rival.GetComponent<PlayerMovement>().Damage(1);
					chargues = chargues - 1;
				}
				if (!IAPhase) {
					UpdateNumberOfTurns ();
				}
			}
		
		}
      

    }

    public void Rechargue() {


        if (chargues < 5) {

            chargues = chargues + 1;
            print("Cargas "+chargues);
        }
		else if (chargues == 5) 
		{
			print ("Carga máxima conseguida");
		}

		if (!IAPhase) {
			UpdateNumberOfTurns ();
		}

    }
    public void Damage(int damage) {
		if (damage == 2) 
		{
			life = 0;
		}
        else if (!defense)
        {
            life = life - damage;
            print("Vida " + life);
        }
		if (canvasReference != null && lifeSlider != null) 
		{
			canvasReference.colorPlayerLifesCanvas (life, lifeSlider);
		}       
    }

    public void Defense() {

        if (shield > 0)
        {
            defense = true;
            shield = shield - 1;
			if (!IAPhase) {
				UpdateNumberOfTurns ();
			}
        }
    }

}
