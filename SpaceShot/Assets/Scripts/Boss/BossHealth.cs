using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, ITakeDamage
{

    public Boss Boss;
    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    public bool dead;

    public void TakeDamage(Vector2 source, float radius, int amount)
    {
        Debug.Log("Taking Damage");
        CurrentHealth = CurrentHealth - amount;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (CurrentHealth < 1)
        {
            Boss.Complete();
            dead = true;
        }
    }
}
