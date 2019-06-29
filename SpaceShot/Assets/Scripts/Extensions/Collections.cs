using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collections 
{
  
    public static T GetRandom<T>(this List<T> collection)
    {
        if (collection.Count == 0) return default;

        return collection[Random.Range(0, collection.Count)];
    }

    public static T GetRandom<T>(this T[] collection)
    {
        if (collection.Length == 0) return default;

        return collection[Random.Range(0, collection.Length)];
    }
}
