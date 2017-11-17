﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Transform[] fila1 = new Transform[3];
    public Transform[] fila2 = new Transform[3];
    public Transform[] fila3 = new Transform[3];
    private Transform[,] positions = new Transform[3,3];
    private int posX = 1;
    private int posY = 1;
    private float timer = 0.1f;
	// Use this for initialization
	void Start () {
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
       


		
	}

    void Move() {

        posX = posX + (int)(Input.GetAxisRaw("Horizontal"));
        posY = posY + (int)-(Input.GetAxisRaw("Vertical"));
        if (posX < 0) { posX = 0; }
        else if (posX > 2) { posX = 2; }
        if (posY < 0) { posY = 0; }
        else if (posY > 2) { posY = 2; }
        transform.position = positions[posX, posY].position;
        timer = 0.15f;
    }
}
