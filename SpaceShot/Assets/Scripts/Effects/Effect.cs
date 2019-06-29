using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    float size;
    public SpriteRenderer SpriteRenderer;
    public Color[] Colours;

    bool skip;

    public void Create(float size)
    {
        this.size = size;
        Set();

    }

    void Set()
    {
        SpriteRenderer.color = Colours[Random.Range(0, Colours.Length)];
        transform.localScale = new Vector3(size, size, 1f);
    }

    void Update()
    {
        skip = !skip;
        if (skip) return;

        if (size < 0.1f)
        {
            Destroy(gameObject);
        }

        size = size / 1.5f;
        Set();

    }
}
