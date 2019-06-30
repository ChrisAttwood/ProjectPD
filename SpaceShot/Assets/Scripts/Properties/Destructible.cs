using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
public class Destructible : MonoBehaviour ,ITakeDamage {

    Color[,] Orignal;

    int scale = 32;


    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;

    System.Diagnostics.Stopwatch sw;

    public int Health = 100;

    public Sprite[] Cracks;
    public SpriteRenderer CrackDisplay;


    void Awake()
    {
        CrackDisplay.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 4) * 90f);
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

        if (Configuration.Data.PixelDestructible)
        {

            SnapShot();
            for (int x = 0; x < scale; x++)
            {
                for (int y = 0; y < scale; y++)
                {
                    float X = transform.position.x + ((x - 16) / 32f);
                    float Y = transform.position.y + ((y - 16) / 32f);


                    if (Vector2.Distance(source, new Vector2(X, Y)) < radius)
                    {
                        if (Orignal[x, y].a > 0f)
                        {

                            if (Random.Range(0f, 1f) > 0.9f)
                            {
                            
                                var pixel = PixelPool.instance.Get();
                                pixel.Set();
                                pixel.SpriteRenderer.color = spriteRenderer.color;
                                pixel.transform.position = new Vector2(X, Y);
                            }

                        }

                        Orignal[x, y] = new Color(0f, 0f, 0f, 0f);
                    }
                }
            }

            Paint(Orignal);

        }
        else
        {
            Health -= amount;

            float percent = (Health*1f) / 100f;
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

                //for(int i = 0; i < 20; i++)
                //{
                //    var pixel = PixelPool.instance.Get();
                //    pixel.Set();
                //    pixel.SpriteRenderer.color = spriteRenderer.color;
                //    pixel.transform.position = new Vector2(transform.position.x, transform.position.y);
                //}
               

                Destroy(gameObject);
            }
        }
        
    }


    void Paint(Color[,] colours)
    {
        Texture2D texture = new Texture2D(scale, scale);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;


        int pixelCount = 0;

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, colours[x, y]);

               if(pixelCount<100)
                {
                    if (colours[x, y].a > 0f)
                    {
                        pixelCount++;
                    }
                }

            }
        }
        texture.Apply();
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, scale, scale), new Vector2(0.5f, 0.5f),32);

     

        if (pixelCount>=100)
        {
           
            ReCalculateCollider();
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private struct ColliderSegment
    {
        public Vector2 Point1;
        public Vector2 Point2;
        public ColliderSegment(Vector2 Point1, Vector2 Point2)
        {
            this.Point1 = Point1;
            this.Point2 = Point2;
        }
    }

    public void ReBuildCollider()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        ReCalculateCollider();
    }

    void ReCalculateCollider()
    {

        polygonCollider2D.pathCount = 0;


        List<ColliderSegment> segments;

        segments = GetSegments(spriteRenderer.sprite.texture);
        List<List<Vector2>> paths;

         paths = FindPaths(segments);
        paths = ConvertToLocal(paths, spriteRenderer.sprite);
        paths = CalculatePivot(paths, spriteRenderer.sprite);
        polygonCollider2D.pathCount = paths.Count;

    
        for (int p = 0; p < paths.Count; p++)
        {
            polygonCollider2D.SetPath(p, paths[p].ToArray());
        }

    
    }

    List<List<Vector2>> FindPaths(List<ColliderSegment> segments)
    {
        List<List<Vector2>> output = new List<List<Vector2>>();
        List<Vector2> currentpath;
        while (segments.Count > 0)
        {
            Vector2 currentpoint = segments[0].Point2;
            currentpath = new List<Vector2>();
            currentpath.Add(segments[0].Point1);
            currentpath.Add(segments[0].Point2);
            segments.Remove(segments[0]);
            bool currentpathcomplete = false;
            while (currentpathcomplete == false)
            {

                currentpathcomplete = true;
                for (int s = 0; s < segments.Count; s++)
                {
                    if (segments[s].Point1 == currentpoint)
                    {
                        currentpathcomplete = false;
                        currentpath.Add(segments[s].Point2);
                        currentpoint = segments[s].Point2;
                        segments.Remove(segments[s]);
                    }
                    else if (segments[s].Point2 == currentpoint)
                    {
                        currentpathcomplete = false;
                        currentpath.Add(segments[s].Point1);
                        currentpoint = segments[s].Point1;
                        segments.Remove(segments[s]);
                    }
                }
            }
            output.Add(currentpath);
        }
        return output;
    }

    List<ColliderSegment> GetSegments(Texture2D texture)
    {
        List<ColliderSegment> output = new List<ColliderSegment>();

        for (int height = 0; height < texture.height; height++)
        {
            for (int width = 0; width < texture.width; width++)
            {

                if (texture.GetPixel(width, height).a != 0)
                {

                    if (height + 1 >= texture.height || texture.GetPixel(width, height + 1).a == 0)
                    {
                        output.Add(new ColliderSegment(new Vector2(width, height + 1), new Vector2(width + 1, height + 1)));
                    }
                    if (height - 1 < 0 || texture.GetPixel(width, height - 1).a == 0)
                    {
                        output.Add(new ColliderSegment(new Vector2(width, height), new Vector2(width + 1, height)));
                    }
                    if (width + 1 >= texture.width || texture.GetPixel(width + 1, height).a == 0)
                    {
                        output.Add(new ColliderSegment(new Vector2(width + 1, height), new Vector2(width + 1, height + 1)));
                    }
                    if (width - 1 < 0 || texture.GetPixel(width - 1, height).a == 0)
                    {
                        output.Add(new ColliderSegment(new Vector2(width, height), new Vector2(width, height + 1)));
                    }


                }
            }
        }
        return output;
    }


    private List<List<Vector2>> ConvertToLocal(List<List<Vector2>> original, Sprite sprite)
    {

        for (int p = 0; p < original.Count; p++)
        {
            for (int o = 0; o < original[p].Count; o++)
            {
                Vector2 currentpoint = original[p][o];
                currentpoint.x /= 32f;
                currentpoint.y /= 32f;
                original[p][o] = currentpoint;
            }
        }
        return original;
    }

    private List<List<Vector2>> CalculatePivot(List<List<Vector2>> original, Sprite sprite)
    {
        Vector2 pivot = sprite.pivot;
        pivot.x /= 32f;
        pivot.y /= 32f;
        for (int p = 0; p < original.Count; p++)
        {
            for (int o = 0; o < original[p].Count; o++)
            {
                original[p][o] -= pivot;
            }
        }
        return original;
    }

}
