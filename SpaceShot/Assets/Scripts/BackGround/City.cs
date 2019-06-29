using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public Parallax Towers;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //    for(int x = -10;x< 100; x++)
    //    {
    //        int depth = Random.Range(50, 100);

    //        if (Random.Range(0f, 1f) > 0.95f)
    //        {
    //            depth -= 25;
    //        }

    //        var p = Instantiate(Towers);

    //        p.Depth = depth;

    //        p.transform.position = new Vector2(x * 4 , 8 - (depth / 10));

    //        float scale = 200f / (depth + 100);
    //        p.transform.localScale = new Vector2(scale, scale);

    //        float tone = 0.6f - depth / 100f;

    //        p.GetComponent<SpriteRenderer>().color = new Color(tone, tone, tone);

    //        p.transform.parent = transform;
    //    }


    //}

}
