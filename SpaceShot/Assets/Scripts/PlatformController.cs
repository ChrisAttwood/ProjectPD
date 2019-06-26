using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject platform;
    public int platformLength;
    public List<PlatformNode> platformNodes;
    public List<int> pathing;
    public PlatformNode nextNode;
    public int currentNodeNumber = 0;
    public float speed = 1f;

    private void Awake()
    {
        MakePlatform();
    }

    private void Start()
    {
        UnparentNodes();
        if (pathing == null || pathing.Count == 0)
        {
            DefaultNodePath();
        }
        nextNode = platformNodes[pathing[0]];
    }

    private void MakePlatform()
    {
        for (int i = 0; i < platformLength; i++)
        {
            GameObject p = Instantiate(platform);
            p.transform.SetParent(this.transform);
            float width = p.GetComponent<SpriteRenderer>().bounds.size.x;
            float offset = (0.5f + i) - (0.5f * platformLength);
            float trueOffset = width * offset;
            p.transform.position = new Vector2(this.transform.position.x + trueOffset, this.transform.position.y);
        }
    }

    private void UnparentNodes()
    {
        foreach (PlatformNode p in platformNodes){
            p.transform.SetParent(null);
        }
    }

    private void DefaultNodePath()
    {
        pathing = new List<int>();
        for (int i = 0; i < platformNodes.Count; i++)
        {
            pathing.Add(i);
        }
    }

    public void SelectNextNode()
    {
        if (currentNodeNumber == (pathing.Count -1))
        {
            currentNodeNumber = 0;
        } else
        {
            currentNodeNumber++;
        }
        nextNode = platformNodes[currentNodeNumber];
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextNode.transform.position, speed * Time.deltaTime);
        if (transform.position == nextNode.transform.position)
        {
            SelectNextNode();
        }
    }

}
