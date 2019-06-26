using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{


    public static ProjectilePool instance;
    public List<Projectile> pooledObjects;
    public Projectile objectToPool;
    public int amountToPool;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pooledObjects = new List<Projectile>();
        for (int i = 0; i < amountToPool; i++)
        {
            Projectile obj = Instantiate(objectToPool);
            obj.transform.parent = transform;
            obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    public Projectile Get()
    {

        if (pooledObjects.Count <= 0)
        {
            Projectile obj = Instantiate(objectToPool);
            return obj;
        }

        var item = pooledObjects[pooledObjects.Count - 1];
        pooledObjects.RemoveAt(pooledObjects.Count - 1);
        if (item == null)
        {
            Projectile obj = Instantiate(objectToPool);
            return obj;
        }
        item.gameObject.SetActive(true);
        return item;


    }

    public void Return(Projectile item)
    {
        item.gameObject.SetActive(false);
        pooledObjects.Add(item);
    }

}
