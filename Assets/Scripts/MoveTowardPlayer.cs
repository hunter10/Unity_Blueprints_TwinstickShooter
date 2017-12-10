using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour {

    private Transform player;
    public float speed = 1.0f;

	void Start () {
        player = GameObject.Find("PlayerShip").transform;
	}
	
	void Update () {

        Vector3 delta = player.position - transform.position;
        delta.Normalize();
        float moveSpeed = speed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
	}
}
