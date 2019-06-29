using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrip : MonoBehaviour
{
    public GameObject Hand;
    public BoxCollider2D BoxCollider2D;
    public Sentity Sentity;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Sentity.Grounds.Contains(collision))
        {
            Sentity.Grounds.Add(collision);
        }

        Hand.transform.position = Vector2.MoveTowards(Hand.transform.position, transform.position,0.03f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Sentity.Grounds.Contains(collision))
        {
            Sentity.Grounds.Remove(collision);
        }

        if (Sentity.Equipment != null)
        {
            Sentity.Equipment.Equip(Sentity);
        }
        
    }

  

}
