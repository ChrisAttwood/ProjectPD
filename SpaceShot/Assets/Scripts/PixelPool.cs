using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPool : MonoBehaviour
{
    public static PixelPool instance;
    public List<Pixel> pooledObjects;
    public Pixel objectToPool;
    public int amountToPool;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pooledObjects = new List<Pixel>();
        for (int i = 0; i < amountToPool; i++)
        {
            Pixel obj = Instantiate(objectToPool);
            obj.transform.parent = transform;
            obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    public Pixel Get()
    {

        if (pooledObjects.Count <= 0)
        {
            Pixel obj = Instantiate(objectToPool);
            return obj;
        }

        var item = pooledObjects[pooledObjects.Count - 1];
        pooledObjects.RemoveAt(pooledObjects.Count - 1);
        if (item == null)
        {
            Pixel obj = Instantiate(objectToPool);
            return obj;
        }
        item.gameObject.SetActive(true);
        return item;


    }

    public void Return(Pixel item)
    {
        item.gameObject.SetActive(false);
        pooledObjects.Add(item);
    }
}
