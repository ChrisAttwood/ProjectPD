using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHazard : MonoBehaviour
{
    public int damageOnEnter = 1000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Sentity>() != null)
        {
            Sentity collidingSentity = collision.GetComponent<Sentity>();
            SentityFellInWater(collidingSentity);
        }
    }

    private void SentityFellInWater(Sentity collidingSentity)
    {
        collidingSentity.TakeDamage(collidingSentity.transform.position, 1, damageOnEnter);
    }
}
