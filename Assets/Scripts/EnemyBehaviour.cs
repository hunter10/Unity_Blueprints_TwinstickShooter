﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public int health = 2;
    public Transform explosion;

    private void OnCollisionEnter2D(Collision2D theCollision)
    {
        Debug.Log("Hit " + theCollision.gameObject.name);

        if(theCollision.gameObject.name.Contains("Laser"))
        {
            LaserBehaviour laser = theCollision.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
            health -= laser.damage;
            Destroy(theCollision.gameObject);

            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            controller.KilledEnemy();
        }

        if(health <= 0)
        {
            if(explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
            }
            Destroy(this.gameObject);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
