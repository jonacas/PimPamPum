using System.Collections;
using System.Collections.Generic;

public enum playerActions
{
    Charge,
    Shoot,
    Move,
    Guard,
    MoveUp,
    MoveRight,
    MoveLeft,
    MoveDown
};



public class E_PlayerMovement {


	///FILAS = POSY
	/// COLUMNAS = POSX
	/// 
	/// 

	/* public Transform[] fila1 = new Transform[3];
    public Transform[] fila2 = new Transform[3];
    public Transform[] fila3 = new Transform[3];
    private Transform[,] positions = new Transform[3,3];*/
	//private float timer = 0.1f;
	//private bool defense = false;
	//public GameObject rival;

    //public string horizontal;
    //public string vertical;
    ///public Slider vida;
	/// 
	/// 
    public int posX = 1;
    public int posY = 1;

    public int life;
    public int shield;
    public int chargues;


	public bool myTurn = true;
	public bool legalMove; // SE ACTUALIZA EN MOVE, AUNQUE ESTA DEUELVA VOID



    // Use this for initialization
	public E_PlayerMovement () {
        life = 3;
        shield = 3;
        chargues = 1;
        /*for (int i = 0; i < fila1.Length; i++) {

            positions[0, i] = fila1[i];
            positions[1, i] = fila2[i];
            positions[2, i] = fila3[i];

        }*/
	}
	
	// Update is called once per frame
	/*void Update () {
		if (myTurn) {
			if (timer >= 3.0f) 
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
			}
				timer = timer + Time.deltaTime;			
		}

    }*/

	public bool Move(playerActions movement) {

		legalMove = true;

		switch (movement) 
		{
		case playerActions.MoveDown:
			{
				posY = posY + 1;	
				break;
			}
		case playerActions.MoveLeft:
			{
				posX = posX - 1;
				break;
			}
		case playerActions.MoveRight:
			{
				posX = posX + 1;
				break;
			}
		case playerActions.MoveUp:
			{
				posY = posY - 1;
				break;
			}
		}
       // posX = posX + (int)(Input.GetAxisRaw(horizontal));
       // posY = posY + (int)-(Input.GetAxisRaw(vertical));
		if (posX < 0) { posX = 0; legalMove = false; }
		else if (posX > 2) { posX = 2; legalMove = false; }
		if (posY < 0) { posY = 0; legalMove = false;}
		else if (posY > 2) { posY = 2; legalMove = false; }
		return legalMove;
        //transform.position = positions[posX, posY].position;
    }

	public bool CheckLegalMove(playerActions movimiento)
	{
		switch (movimiento) {
		case playerActions.MoveUp:
				if (posY == 0)
					return false;
                break;

		case playerActions.MoveDown:
			if (posY == 2)
				return false;
            break;

		case playerActions.MoveLeft:
			if (posX == 0)
				return false;
            break;

		case playerActions.MoveRight:
			if (posX == 2)
				return false;
            break;

		case playerActions.Shoot:
			if (chargues <= 0)
				return false;
            break;

		case playerActions.Guard:
			if (shield <= 0)
				return false;
            break;
		}

		return true;
	}
    /*void Attack() {

        float distanceToEnemy = Vector3.Distance(rival.transform.position, transform.position);
        print("Distancia: " + distanceToEnemy); 
        if(chargues > 0 && distanceToEnemy <= 7.5f )
        {
            rival.GetComponent<PlayerMovement>().Damage(1);
            chargues = chargues - 1;
        }


    }*/

    public void Rechargue() {


        if (chargues < 5) {

            chargues = chargues + 1;
            //print("Cargas "+chargues);
        }

    }
    public void Damage(int damage = 1) {

        // (!defense)
        //{
            life = life - damage;
            //print("Vida " + life);
        //}
       
    }
}
