using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is here to create an area for the boss to fight in, as environment will likely play a big part in the action.
// Its designed to be pretty flexible so its just a collection of game objects and where you want to put them.
public class BossArea : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public List<Vector2> location;
    public Vector2 levelBossArea;

    public void BuildBossArea()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            GameObject go = Instantiate(gameObjects[i]);
            go.transform.position = location[i] + levelBossArea;
        }
    }
}
