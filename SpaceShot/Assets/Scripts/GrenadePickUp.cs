using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        Sentity sentity = (other.GetComponent<Sentity>());
        if (sentity != null)
        {
            if (sentity.IsPlayer)
            {
                PickupBodyGrenades(sentity);
            }
        }
    }

    private void PickupBodyGrenades(Sentity sentity)
    {
        sentity.Grenades = 5;
        GrenadeCounter.instance.UpdateCounter(sentity.Grenades);
        HidePickup();
    }

    private void HidePickup()
    {
        Destroy(gameObject);
        //GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = false;
    }
}
