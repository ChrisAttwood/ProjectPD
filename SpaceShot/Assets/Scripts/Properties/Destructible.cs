using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Destructible : MonoBehaviour ,ITakeDamage {

    Color[,] Orignal;

    SpriteRenderer spriteRenderer;
   

    System.Diagnostics.Stopwatch sw;

    public int Health = 100;
    float maxHealth;

    public Sprite[] Cracks;
    public SpriteRenderer CrackDisplay;


    void Awake()
    {
        maxHealth = Health * 1f;

        CrackDisplay.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 4) * 90f);
        spriteRenderer = GetComponent<SpriteRenderer>();
      
    }


    public void TakeDamage(Vector2 source, float radius, int amount)
    {

        
        Health -= amount;

        float percent = (Health*1f) / maxHealth;
        int index = (int)(percent * Cracks.Length);
        index = Cracks.Length - index;
        if (index >= Cracks.Length)
        {
            index = Cracks.Length - 1;
        }

        if (index < 0)
        {
            index = 0;
        }
           
        CrackDisplay.sprite = Cracks[index];

        if (Health <= 0)
        {
            for(int x = -5; x < 5; x++)
            {
                for (int y = -5; y < 5; y++)
                {
                    var pixel = PixelPool.instance.Get();
                    pixel.Set();
                    pixel.SpriteRenderer.color = spriteRenderer.color;
                    pixel.transform.position = new Vector2(transform.position.x + x/10f, transform.position.y + y / 10f);
                }
            }


            Destroy(gameObject);
        }
        
        
    }

}
