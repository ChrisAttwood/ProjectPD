using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zone : MonoBehaviour
{
    int[,] data;

    public Sentity SentityPrefab;

    public GameObject[] HardBlocks;
    public GameObject[] SoftBlocks;
    public GameObject[] Clutter;
    public Equipment[] CommonWeapons;
    public Equipment[] RareWeapons;
    public GameObject[] hazardBlocks;
    public GameObject platform;

    public GameObject[] PickUps;

    Building[] Buildings;

   // public GameObject bodyArmourPickup;


    public Chunk[] Chunks;

    public int ChunkScale = 8;
    public int LevelLength = 64;

    private void Awake()
    {

        if (GameFileManager.GameFile == null)
        {
            SceneManager.LoadScene("Menu");
            return;
        }


        Buildings = CreateBuildings();


        data = new int[LevelLength, 16];

      

        for (int x = 0; x < LevelLength; x+=8)
        {

            for (int y = 0; y < 16; y+=8)
            {

                var chunk = Chunks[Random.Range(0, Chunks.Length)];

                for (int cX = 0; cX < 8; cX++)
                {
                    for (int cY = 0; cY < 8; cY++)
                    {
                        data[x + cX, y + cY] = chunk.ChunkData.rows[cY].row[cX];
                    }
                }


            }
        }


        for (int x = 0; x < LevelLength; x++)
        {
            for (int y = 0; y < 16; y++)
            {

                if (data[x, y] > 0)
                {
                    var item = Get(data[x, y], Buildings[x / 16]);
                    item.transform.position = new Vector2(x, y + 1);

                }

            }

        }

    }

    GameObject Get(int data,Building building)
    {
        switch (data)
        {
            case 1:
                var sb = Instantiate(building.SoftBlock);
                if (sb.GetComponent<SpriteRenderer>() != null)
                {
                    sb.GetComponent<SpriteRenderer>().color = building.SoftColour;
                }
                else
                {
                    foreach (SpriteRenderer sr in sb.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sr.color = building.SoftColour;
                    }
                }
                return sb;
            case 2:
                var hb = Instantiate(building.HardBlock);
                hb.GetComponent<SpriteRenderer>().color = building.HardColour;
                return hb;
            case 3:
                return Instantiate(building.Clutter);
            case 4:
                var mob = Instantiate(SentityPrefab);
                mob.Create(building.Gang);
                return mob.gameObject;
            case 5:
                var pickup = Instantiate(PickUps[Random.Range(0, PickUps.Length)]);
                return pickup;
            case 6:
                var hazb = Instantiate(building.HazardBlock);

                return hazb;
            case 100:
                var plat = Instantiate(building.Platform);

                return plat;
        }

        return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        var player = Instantiate(SentityPrefab);
        player.transform.position = new Vector2(-5f, 5f);
        player.CreatePlayer(CommonWeapons[Random.Range(0, CommonWeapons.Length)]);
    }


    Building[] CreateBuildings()
    {
        Building[] buildings = new Building[32];

        int level = GameFileManager.GameFile.CurrentLevel;

        for(int i = 0; i < 32; i++)
        {
            buildings[i] = new Building();
            buildings[i].SoftBlock = SoftBlocks[Random.Range(0, SoftBlocks.Length)];
            buildings[i].HardBlock = HardBlocks[Random.Range(0, HardBlocks.Length)];
            // Seb's new code
            buildings[i].HazardBlock = hazardBlocks[Random.Range(0, hazardBlocks.Length)];
            buildings[i].Platform = platform;
            //buildings[i].SoftColour = new Color(Random.Range(5, 8) * 0.1f, Random.Range(5, 8) * 0.1f, Random.Range(5, 8) * 0.1f);
            //buildings[i].HardColour = new Color(Random.Range(2, 5) * 0.1f, Random.Range(2, 5) * 0.1f, Random.Range(2, 5) * 0.1f);

            buildings[i].SoftColour = new Color(Random.Range(5, 8) * 0.1f, Random.Range(5, 8) * 0.1f, Random.Range(5, 8) * 0.1f);
            buildings[i].HardColour = new Color(Random.Range(4, 5) * 0.1f, Random.Range(4, 5) * 0.1f, Random.Range(4, 5) * 0.1f);

            // Seb's new code
            buildings[i].HazardColour = new Color(Random.Range(5, 8) * 0.1f, Random.Range(5, 8) * 0.1f, Random.Range(5, 8) * 0.1f);

            buildings[i].Clutter = Clutter[Random.Range(0, Clutter.Length)];

            buildings[i].Gang = new Gang();
            buildings[i].Gang.CommonWeapon = CommonWeapons[Random.Range(0, CommonWeapons.Length)];
            buildings[i].Gang.RareWeapon = RareWeapons[Random.Range(0, RareWeapons.Length)];

            buildings[i].Gang.Awareness = Random.Range(5,6+i + level);
            buildings[i].Gang.Engage = Random.Range(5, 6+i + level);
            buildings[i].Gang.TriggerRate = Random.Range(1f, (8f / (i + level + 1f)) + 1f); // Random.Range(100/(i +1),200/ (i + 1));
            buildings[i].Gang.Speed = Random.Range(1 + i, 11 + i + level);
            buildings[i].Gang.Health = Random.Range(2 + i, 12 + i + level);
            buildings[i].Gang.BodyColour = new Color(Random.Range(1, 10) * 0.1f, Random.Range(1, 10) * 0.1f, Random.Range(1, 10) * 0.1f);
            buildings[i].Gang.LegColour = new Color(Random.Range(1, 10) * 0.1f, Random.Range(1, 10) * 0.1f, Random.Range(1, 10) * 0.1f);
        }

        return buildings;
    }

   
}

public class Building
{
    public GameObject SoftBlock;
    public GameObject HardBlock;
    public GameObject HazardBlock;
    public GameObject Platform;
    public Color SoftColour;
    public Color HardColour;
    public Color HazardColour;

    public GameObject Clutter;

    public Gang Gang;

}

//public class Gang
//{

//    public Equipment CommonWeapon;
//    public Equipment RareWeapon;

//    public float Awareness = 10f;
//    public float Engage = 5f;
//    public float TriggerRate = 1f;
//    public float Speed = 5f;
//    public int Health = 10;

//    public Color BodyColour = Color.white;
//    public Color LegColour = Color.white;

//}
