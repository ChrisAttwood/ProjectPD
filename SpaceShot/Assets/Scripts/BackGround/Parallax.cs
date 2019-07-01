using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Camera cam;
    SpriteRenderer sr;

    [Range(1,100)]
    public int Depth = 1;

    float height;
    float xLocation;

    private void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Configuration.Data.CurrentLevel().ParallaxSprites.GetRandom();

        sr.sortingOrder = -Depth * 2;
      
        height = transform.localPosition.y;
        xLocation = transform.localPosition.x;
    }

    void Update()
    {
       

        if(Mathf.Abs(cam.transform.position.x - transform.localPosition.x) < 50)
        {
            sr.enabled = true;
            transform.localPosition = new Vector2(xLocation + cam.transform.position.x * (Depth) * 0.01f, height);
        }
        else
        {
            sr.enabled = false;
        }

      
    }
}
