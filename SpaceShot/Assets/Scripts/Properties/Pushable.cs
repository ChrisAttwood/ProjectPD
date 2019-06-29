using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable :MonoBehaviour, ITakeDamage
{
    

    public void TakeDamage(Vector2 source, float radius, int amount)
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        if(rigidbody2D != null)
        {

            var direction = (Vector2)transform.position - source;

            rigidbody2D.AddForceAtPosition(direction * radius * 500f, source);
        }
    }

   
}
