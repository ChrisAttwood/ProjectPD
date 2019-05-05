using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyArmourPickup : MonoBehaviour
{

    public int armourAmount = 20;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        Sentity sentity = (other.GetComponent<Sentity>());
        if (sentity!= null){
            if (sentity.IsPlayer)
            {
                PickupBodyArmour(sentity);
            }
        }
    }

    private void PickupBodyArmour(Sentity sentity)
    {
        sentity.Armour = armourAmount;
        HidePickup();
    }

    private void HidePickup()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
