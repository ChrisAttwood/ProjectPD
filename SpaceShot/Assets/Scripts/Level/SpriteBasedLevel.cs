using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBasedLevel : MonoBehaviour
{
    public Sprite IndestructibleMap;
    public Sprite DestructibleMap;
    public Sprite HazardMap;
    public Sprite PlayerStartMap;

    public GameObject Indestructible;
    public GameObject Destructible;
    public GameObject Hazard;
    public GameObject Player;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        CreateLayer(IndestructibleMap, Indestructible);
        CreateLayer(DestructibleMap, Destructible);
        CreateLayer(HazardMap, Hazard);
        CreateLayer(PlayerStartMap, Player);
    }

    private void CreateLayer(Sprite layerSprite, GameObject tile)
    {
        int xSize = layerSprite.texture.width;
        int ySize = layerSprite.texture.height;
        for (int x = 0; x < xSize; x++)
        {
            for (int y= 0; y < ySize; y++)
            {
                Color c = layerSprite.texture.GetPixel(x, y);
                if (c.a > 0f)
                {
                    GameObject.Instantiate(tile, new Vector2(x, y), tile.transform.rotation);
                }
            }
        }
    }
}
