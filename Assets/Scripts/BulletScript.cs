using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private Vector3 bulletOrigin;
    private Vector3 rival;
    private bool moverse = false;
    
	// Use this for initialization
	void Awake () {
        bulletOrigin = this.transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        if (moverse) {

            Vector3.Lerp(bulletOrigin,rival,0.2f);
            if (this.transform.position == rival) {

                moverse = false;
                this.transform.position = bulletOrigin;
            }

        }


	}

    public void Mover(Vector3 position) {

        moverse = true;


    }
}
