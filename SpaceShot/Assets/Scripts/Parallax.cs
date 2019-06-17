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

        sr.sortingOrder = -Depth * 2;
        if (transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -Depth * 2 +1;
        }
        
        height = transform.localPosition.y;
        xLocation = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = new Vector2(xLocation + cam.transform.position.x * (100- Depth) * 0.01f, height);
        transform.localPosition = new Vector2(xLocation + cam.transform.position.x * (Depth) * 0.01f, height);
    }
}
