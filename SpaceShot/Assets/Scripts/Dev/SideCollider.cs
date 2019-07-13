using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollider : MonoBehaviour
{
    public bool rightSide;
    public Mover mover;
    public bool activated = false;

    private void Awake()
    {
        if (GetComponentInParent<Sentity>().IsPlayer)
        {
            activated = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
        {
            float parentScale = transform.parent.localScale.x;
            if ((parentScale > 0f && rightSide) || (parentScale < 0f && !rightSide))
            {
                mover.RightCollisions.Add(collision);
            }
            else
            {
                mover.LeftCollisions.Add(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (activated)
        {
            float parentScale = transform.parent.localScale.x;
            if ((parentScale > 0f && rightSide) || (parentScale < 0f && !rightSide))
            {
                mover.RightCollisions.Remove(collision);
                mover.LeftCollisions.Remove(collision);
            }
            else
            {
                mover.LeftCollisions.Remove(collision);
                mover.RightCollisions.Remove(collision);
            }
        }
    }

}
