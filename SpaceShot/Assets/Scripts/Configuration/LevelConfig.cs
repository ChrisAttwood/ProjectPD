using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    [Header("Core")]
    public string Name;
    public int LevelLength = 8;
    public Equipment StartWeapon;

    [Header("Chunk Data")]
    public Chunk[] Chunks;
    [Header("Enemy Data")]
    public Gang Gang;
    public Boss Boss;
    public Vector2 BossLocalPosition;
    public BossArea BossArea;

    [Header("Colours")]
    public Color SoftBlockColour = Color.white;
    public Color HardBlockColour = Color.gray;
    public Color LadderColour = Color.black;

    [Header("Resources")]
    public GameObject[] Clutter;
    public GameObject[] PickUps;
    public GameObject SoftBlock;
    public GameObject HardBlock;
    public GameObject Liquid;
    public GameObject Platform;
    public GameObject Ladder;

    [Header("Background")]
    public Sprite Sky;
    public Sprite[] ParallaxSprites;





}

[System.Serializable]
public class Gang
{
    [Header("Weapons")]
    public Equipment CommonWeapon;
    public Equipment RareWeapon;
    [Header("Stats")]
    public int Health = 10;
    public float Speed = 5f;
    public float Awareness = 10f;
    public float Engage = 5f;
    public float TriggerRate = 1f;
    [Header("Colours")]
    public Color BodyColour = Color.white;
    public Color LegColour = Color.white;

}