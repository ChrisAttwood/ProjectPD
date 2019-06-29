using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    
    public void ThrowMe(Vector3 target, float forceMultiplier)
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        var direction = target - transform.position;
        direction.Normalize();
        rb2D.AddForce(direction * forceMultiplier);
    }

}
