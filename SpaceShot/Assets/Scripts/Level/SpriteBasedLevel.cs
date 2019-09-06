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

    public Color IndestructibleColor;
    public Color DestructibleColor;
    public Color HazardColor;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        CreateLayer(IndestructibleMap, Indestructible, IndestructibleColor);
        CreateLayer(DestructibleMap, Destructible, DestructibleColor);
        CreateLayer(HazardMap, Hazard, HazardColor);
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

    private void CreateLayer(Sprite layerSprite, GameObject tile, Color tileColour)
    {
        int xSize = layerSprite.texture.width;
        int ySize = layerSprite.texture.height;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Color c = layerSprite.texture.GetPixel(x, y);
                if (c.a > 0f)
                {
                    GameObject GO = GameObject.Instantiate(tile, new Vector2(x, y), tile.transform.rotation);
                    if (GO.GetComponent<SpriteRenderer>() != null)
                    {
                        Debug.Log("I couldn't find a component so else");
                        GO.GetComponent<SpriteRenderer>().color = tileColour;
                    } else
                    {
                        SpriteRenderer[] spriteRenderers = GO.GetComponentsInChildren<SpriteRenderer>();
                        foreach(SpriteRenderer sRenderer in spriteRenderers)
                        {
                            sRenderer.color = tileColour;
                        }
                    }
                }
            }
        }
    }
}
