using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelConfig 
{
    [Header("Core")]
    public string Name;
    public int LevelLength = 64;
    [Header("Chunk Data")]
    public Chunk[] Chunks;
    [Header("Enemy Data")]
    public Gang Gang;

    [Header("Colours")]
    public Color SoftBlockColour = Color.white;
    public Color HardBlockColour = Color.gray;

    [Header("Resources")]
    public GameObject[] Clutter;
    public GameObject[] PickUps;
    public GameObject SoftBlock;
    public GameObject HardBlock;
    public GameObject Liquid;
    public GameObject Platform;

    [Header("Background")]
    public Sprite Sky;
    public Parallax[] ParallaxObjects;

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