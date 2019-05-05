using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Destructible : MonoBehaviour ,ITakeDamage {

    Color[,] Orignal;
    public GameObject Pixel;

    int scale = 32;


    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;

    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }


    void SnapShot()
    {
       var sp = spriteRenderer.sprite;

       Orignal = new Color[scale, scale];

        for (int x = 0; x < scale; x++)
        {
            for (int y = 0; y < scale; y++)
            {
                Orignal[x, y] = sp.texture.GetPixel(x, y);
            }
        }

    }


    public void TakeDamage(Vector2 source, float radius, int amount)
    {
        SnapShot();

        for (int x = 0; x < scale; x++)
        {
            for (int y = 0; y < scale; y++)
            {
                float X = transform.position.x + ((x - 16) / 32f);
                float Y = transform.position.y + ((y - 16) / 32f);
                //float X = transform.position.x + ((x - 8) / 16f);
                //float Y = transform.position.y + ((y - 8) / 16f);


                if (Vector2.Distance(source, new Vector2(X, Y)) < radius)
                {
                    if (Orignal[x, y].a > 0f)
                    {

                        if (Random.Range(0f, 1f) > 0.9f)
                        {
                            GameObject pixel = Instantiate(Pixel);
                            pixel.GetComponent<SpriteRenderer>().color = spriteRenderer.color;//Orignal[x, y];
                            pixel.transform.position = new Vector2(X, Y);
                        }

                    }

                    Orignal[x, y] = new Color(0f, 0f, 0f, 0f);
                }
            }
        }
        Paint(Orignal);
    }


    void Paint(Color[,] colours)
    {
        Texture2D texture = new Texture2D(scale, scale);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;

        bool HasMat = false;

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, colours[x, y]);

                if (!HasMat)
                {
                    if (colours[x, y].a > 0f)
                    {
                        HasMat = true;
                    }
                }

            }
        }
        texture.Apply();
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, scale, scale), new Vector2(0.5f, 0.5f),32);

        if (polygonCollider2D != null)
        {
            Destroy(polygonCollider2D);
        }

        if (HasMat)
        {
            polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
        }


    }

   
}
