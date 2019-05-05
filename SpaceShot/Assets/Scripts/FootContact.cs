using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootContact : MonoBehaviour
{
    public Sentity Sentity;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Sentity.Grounds.Contains(collision))
        {
            Sentity.Grounds.Add(collision);
        }

      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Sentity.Grounds.Contains(collision))
        {
            Sentity.Grounds.Remove(collision);
        }
    }
}
