using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void TakeDamage(Vector2 source, float radius, int amount);

}
