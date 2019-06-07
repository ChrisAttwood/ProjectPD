using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    int[,] data;

    public Sentity SentityPrefab;
   // public SentityData PlayerData;


    public GameObject[] HardBlocks;
    public GameObject[] SoftBlocks;
    public GameObject[] Clutter;
    public Equipment[] CommonWeapons;
    public Equipment[] RareWeapons;

    //public GameObject[] Blocks;

    //public SentityData[] Badies;

    Building[] Buildings;

    public GameObject bodyArmourPickup;


    public Chunk[] Chunks;


    private void Awake()
    {

        Buildings = CreateBuildings();


        data = new int[256, 8];

      

        for (int x = 0; x < 256; x+= 4)
        {

            for (int y = 0; y < 8; y+=4)
            {

                var chunk = Chunks[Random.Range(0, Chunks.Length)];

                for (int cX = 0; cX < 4; cX++)
                {
                    for (int cY = 0; cY < 4; cY++)
                    {

                        data[x + cX, y + cY] = chunk.ChunkData.rows[cY].row[cX];
                    }
                }


            }
        }


        for (int x = 0; x < 256; x++)
        {
            for (int y = 0; y < 8; y++)
            {

                if (data[x, y] > 0)
                {
                    var item = Get(data[x, y], Buildings[x/16]);
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
                sb.GetComponent<SpriteRenderer>().color = building.SoftColour;
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
                var pickup = Instantiate(bodyArmourPickup);
                return pickup;



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

        for(int i = 0; i < 32; i++)
        {
            buildings[i] = new Building();
            buildings[i].SoftBlock = SoftBlocks[Random.Range(0, SoftBlocks.Length)];
            buildings[i].HardBlock = HardBlocks[Random.Range(0, HardBlocks.Length)];
            buildings[i].SoftColour = new Color(Random.Range(5, 8) * 0.1f, Random.Range(5, 8) * 0.1f, Random.Range(5, 8) * 0.1f);
            buildings[i].HardColour = new Color(Random.Range(2, 5) * 0.1f, Random.Range(2, 5) * 0.1f, Random.Range(2, 5) * 0.1f);
            buildings[i].Clutter = Clutter[Random.Range(0, Clutter.Length)];

            buildings[i].Gang = new Gang();
            buildings[i].Gang.CommonWeapon = CommonWeapons[Random.Range(0, CommonWeapons.Length)];
            buildings[i].Gang.RareWeapon = RareWeapons[Random.Range(0, RareWeapons.Length)];

            buildings[i].Gang.Awareness = Random.Range(5,6+i);
            buildings[i].Gang.Engage = Random.Range(5, 6+i);
            buildings[i].Gang.TriggerRate = Random.Range(1f, (8f / (i + 1f)) + 1f); // Random.Range(100/(i +1),200/ (i + 1));
            buildings[i].Gang.Speed = Random.Range(1 + i, 11 + i);
            buildings[i].Gang.Health = Random.Range(2 + i, 12 + i);
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
    public Color SoftColour;
    public Color HardColour;

    public GameObject Clutter;

    public Gang Gang;

}

public class Gang
{

    public Equipment CommonWeapon;
    public Equipment RareWeapon;

    public float Awareness = 10f;
    public float Engage = 5f;
    public float TriggerRate = 1f;
    public float Speed = 5f;
    public int Health = 10;

    public Color BodyColour = Color.white;
    public Color LegColour = Color.white;

}
