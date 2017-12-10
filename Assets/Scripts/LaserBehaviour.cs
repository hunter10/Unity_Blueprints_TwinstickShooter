using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour 
{
    // 레이저가 얼마 동안 존재할지
    public float lifetime = 2.0f;

    // 레이저가 얼마나 빠르게 움직이는지
    public float speed = 5.0f;

    // 레이저가 적과 충돌하면 얼마큼의 피해를 주는지
    public int damage = 1;

	void Start () 
    {
        Destroy(gameObject, lifetime);	
	}
	
	void Update () 
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);	
	}
}
