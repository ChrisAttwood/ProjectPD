using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public Mover Mover;


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (!Mover.Grounds.Contains(collision))
        {
            Mover.Grounds.Add(collision);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (Mover.Grounds.Contains(collision))
        {
            Mover.Grounds.Remove(collision);
        }
    }
}
