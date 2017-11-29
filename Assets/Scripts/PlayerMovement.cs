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
    private int posX = 1;
    private int posY = 1;
    private float timer = 0.1f;
	private int life;
    private int shield;
    private int chargues;
    private bool defense = false;

	public bool myTurn = true;
	public bool legalMove; // SE ACTUALIZA EN MOVE, AUNQUE ESTA DEUELVA VOID
	public CanvasManager canvasReference;
	public int totalTurns = 0;

	public enum playerActions
	{
		Charge, 
		Shoot,
		Move,
		Guard
	};

	public int Life 
	{
		get {
			return life;
			}
	}

	public enum playerMovements
	{
		MoveUp,
		MoveRight,
		MoveLeft,
		MoveDown
	};

    // Use this for initialization
    void Start () {
        life = 3;
        shield = 3;
        chargues = 1;
        for (int i = 0; i < fila1.Length; i++) {

            positions[0, i] = fila1[i];
            positions[1, i] = fila2[i];
            positions[2, i] = fila3[i];

        }
	}
	
	// Update is called once per frame
	void Update () {
		if (myTurn) {
			RandomCalculation ();
			}
				timer = timer + Time.deltaTime;			


    }

	void RandomCalculation()
	{
	if (timer >= 1.0f) 
	{
		float random = Random.value;

		if( random < 0.4f)
		{
			if (random < 0.1f) 
			{
				Move (playerMovements.MoveUp);
			} 
			else if (random < 0.2f) 
			{
				Move (playerMovements.MoveLeft);
			}
			else if (random < 0.3f) 
			{
				Move (playerMovements.MoveRight);
			} 
			else
			{
				Move (playerMovements.MoveDown);
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
			if (shield > 0) {	
				defense = true;
				shield = shield - 1;
			}
			print ("Escudos: " + shield);

		}

		timer = 0.0f;
		totalTurns = totalTurns + 1;
		if (canvasReference != null) 
		{
			canvasReference.UpdateTurnNumberCanvas (totalTurns);
		}
	}
}

	void Move(playerMovements movement) {

		switch (movement) 
		{
		case playerMovements.MoveDown:
			{
				posY = posY - 1;	
				break;
			}
		case playerMovements.MoveLeft:
			{
				posX = posX - 1;
				break;
			}
		case playerMovements.MoveRight:
			{
				posX = posX + 1;
				break;
			}
		case playerMovements.MoveUp:
			{
				posY = posY + 1;
				break;
			}
		}
       // posX = posX + (int)(Input.GetAxisRaw(horizontal));
       // posY = posY + (int)-(Input.GetAxisRaw(vertical));
		legalMove = true;
		if (posX < 0) 
		{ posX = 0; legalMove = false; RandomCalculation (); }
		else if (posX > 2) 
		{ posX = 2; legalMove = false; RandomCalculation (); }
		if (posY < 0)
		{ posY = 0; legalMove = false; RandomCalculation ();}
		else if (posY > 2)
		{ posY = 2; legalMove = false; RandomCalculation (); }
        transform.position = positions[posX, posY].position;
		if (legalMove) 
		{
			legalMove = true;
		}
    }
    void Attack() {

		if (chargues <= 0) {
			RandomCalculation ();
		} 
		else
		{
			float distanceToEnemy = Vector3.Distance(rival.transform.position, transform.position);
			print("Distancia: " + distanceToEnemy); 
			if(chargues > 0 && distanceToEnemy <= 7.5f )
			{
				rival.GetComponent<PlayerMovement>().Damage(1);
				chargues = chargues - 1;
			}
		}
      

    }

    void Rechargue() {


        if (chargues < 5) {

            chargues = chargues + 1;
            print("Cargas "+chargues);
        }

    }
    public void Damage(int damage) {

        if (!defense)
        {
            life = life - damage;
            print("Vida " + life);
			if (canvasReference != null) 
			{
				canvasReference.colorPlayerLifesCanvas (life);
			}

        }
       
    }

}
