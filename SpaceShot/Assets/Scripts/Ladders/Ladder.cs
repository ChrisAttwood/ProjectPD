using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float LadderSpeed = 5f;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Mover mover = collision.GetComponent<Mover>();
    //    if (mover != null)
    //    {
    //        mover.PutSentityOnLadder();
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        Mover mover = collision.GetComponent<Mover>();
        if (mover != null)
        {
            mover.PutSentityOnLadder(LadderSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Mover mover = collision.GetComponent<Mover>();
        if (mover != null)
        {
            mover.TakeSentityOffLadder();
        }
    }
}
