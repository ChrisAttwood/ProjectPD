using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ED209 : MonoBehaviour, ITakeDamage
{
    public Boss Boss;

    public void TakeDamage(Vector2 source, float radius, int amount)
    {
        Boss.Complete();
    }

   
}
