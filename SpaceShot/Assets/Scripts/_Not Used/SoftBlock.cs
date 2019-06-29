using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBlock : MonoBehaviour
{
    public SpriteRenderer Pixel;
    int scale = 32;

    void Start()
    {
        var sp = GetComponent<SpriteRenderer>().sprite;


        for (int x = 0; x < scale; x++)
        {
            for (int y = 0; y < scale; y++)
            {

                

                Color c  = sp.texture.GetPixel(x, y);

                if (c.a != 0)
                {
                    var p = Instantiate(Pixel,this.transform);
                    p.transform.localPosition = new Vector2(x, y);

                    p.color = c;
                }
            }
        }
    }

   

}
