using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VariablesConfig : ScriptableObject
{
    public float JumpPower = 100f;
    public float AntiJumpPower = 50f;
    public float DoubleJumpBoost = 3f;
    public float JumpBoost = 3f;
    public float PlayerSpeed = 5f;
    public int PlayerHealth = 10;
    public float GrenadeThrowMax = 75f;
    public float MaxTotalVelocity = 10f;
    public float MaxHorizontalVelocity = 3f;
}
