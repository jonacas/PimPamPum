using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public string horizontal;
    public string vertical;
    public GameObject rival;
    public Slider vida;
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

        if (timer <= 0) { Move(); }
        timer = timer - Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.V)) {
            defense = false;
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            defense = false;
            Rechargue();
        }

        if (Input.GetKeyDown(KeyCode.D)) {

            defense = true;
            shield = shield - 1;
            print("Escudos: " + shield);
        }




    }

    void Move() {

        posX = posX + (int)(Input.GetAxisRaw(horizontal));
        posY = posY + (int)-(Input.GetAxisRaw(vertical));
        if (posX < 0) { posX = 0; }
        else if (posX > 2) { posX = 2; }
        if (posY < 0) { posY = 0; }
        else if (posY > 2) { posY = 2; }
        transform.position = positions[posX, posY].position;
        timer = 0.15f;
    }
    void Attack() {

        float distanceToEnemy = Vector3.Distance(rival.transform.position, transform.position);
        print("Distancia: " + distanceToEnemy); 
        if(chargues > 0 && distanceToEnemy <= 7.5f )
        {
            rival.GetComponent<PlayerMovement>().Damage(1);
            chargues = chargues - 1;
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
        }
       
    }

}
