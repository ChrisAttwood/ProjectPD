using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    int[,] data;
    LevelConfig LevelConfig;

    private void Awake()
    {
        if (GameFileManager.GameFile == null)
        {
            SceneManager.LoadScene("Menu");
            return;
        }

        LevelConfig = Configuration.Data.CurrentLevel();

        Build();
    }

    void Start()
    {
        var player = Instantiate(Configuration.Data.SentityPrefab);
        player.transform.position = new Vector2(-5f, 5f);
        player.CreatePlayer(LevelConfig.StartWeapon);
    }


    void Build()
    {
        GenerateData();
        BuildLevelFromData();
    }

   

    private void GenerateData()
    {
        data = new int[LevelConfig.LevelLength * 8, 16];
        for (int x = 0; x < LevelConfig.LevelLength * 8; x += 8)
        {
            for (int y = 0; y < 16; y += 8)
            {

                var chunk = LevelConfig.Chunks.GetRandom();

                for (int cX = 0; cX < 8; cX++)
                {
                    for (int cY = 0; cY < 8; cY++)
                    {
                        data[x + cX, y + cY] = chunk.ChunkData.rows[cY].row[cX];
                    }
                }
            }
        }
    }

    private void BuildLevelFromData()
    {


        for (int x = 0; x < LevelConfig.LevelLength * 8; x++)
        {
            for (int y = 0; y < 16; y++)
            {

                if (data[x, y] > 0)
                {
                    var item = Get(data[x, y]);
                    item.transform.position = new Vector2(x, y + 1);

                }

            }

        }
    }

    GameObject Get(int data)
    {
        switch (data)
        {
            case 1:
                var sb = Instantiate(LevelConfig.SoftBlock);
                //sb.GetComponent<SpriteRenderer>().color = LevelConfig.SoftBlockColour;
                if (sb.GetComponent<SpriteRenderer>() != null)
                {
                    sb.GetComponent<SpriteRenderer>().color = LevelConfig.SoftBlockColour;
                }
                else
                {
                    foreach (SpriteRenderer sr in sb.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sr.color = LevelConfig.SoftBlockColour;
                    }
                }
                return sb;
            case 2:
                var hb = Instantiate(LevelConfig.HardBlock);
                hb.GetComponent<SpriteRenderer>().color = LevelConfig.HardBlockColour;
                return hb;
            case 3:
                return Instantiate(LevelConfig.Clutter.GetRandom());
            case 4:
                var mob = Instantiate(Configuration.Data.SentityPrefab);
                mob.Create(LevelConfig.Gang);
                return mob.gameObject;
            case 5:
                var pickup = Instantiate(LevelConfig.PickUps.GetRandom());
                return pickup;
            case 6:
                var hazb = Instantiate(LevelConfig.Liquid);

                return hazb;
            case 7:
                var ladder = Instantiate(LevelConfig.Ladder);
                ladder.GetComponent<SpriteRenderer>().color = LevelConfig.LadderColour;
                return ladder;
            
            case 100:
                var plat = Instantiate(LevelConfig.Platform);

                return plat;
        }

        return null;
    }
}
