using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollider : MonoBehaviour
{
    public bool rightSide;
    public Mover mover;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rightSide)
        {
            mover.RightCollisions.Add(collision);
        } else
        {
            mover.LeftCollisions.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (rightSide)
        {
            mover.RightCollisions.Remove(collision);
        } else
        {
            mover.LeftCollisions.Remove(collision);
        }
    }

}
