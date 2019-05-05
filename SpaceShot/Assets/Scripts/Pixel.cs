﻿using UnityEngine;
using System.Collections;

public class Pixel : MonoBehaviour {


    int Life;
    Vector2 direction;
    float Speed;

	void Start () {
        Life = Random.Range(10, 100);
        direction = new Vector2(Random.Range(-1f, 1f), (Random.Range(-1f, 1f)));
        Speed = Random.Range(0.01f, 0.1f);
    }
	
	void Update () {

        Life--;
        if (Life <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(direction * Speed);
        transform.Translate(Vector2.down / (Life +1f ));
    }
}
