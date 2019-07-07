using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerSentity playerSentity = collision.GetComponent<PlayerSentity>();
        if (playerSentity != null)
        {
            playerSentity.PutSentityOnLadder();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerSentity playerSentity = collision.GetComponent<PlayerSentity>();
        if (playerSentity != null)
        {
            playerSentity.GetComponent<Rigidbody2D>().velocity.Set(0f, 0f);
            Debug.Log(playerSentity.GetComponent<Rigidbody2D>().velocity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerSentity playerSentity = collision.GetComponent<PlayerSentity>();
        if (playerSentity != null)
        {
            playerSentity.TakeSentityOffLadder();
        }
    }
}
