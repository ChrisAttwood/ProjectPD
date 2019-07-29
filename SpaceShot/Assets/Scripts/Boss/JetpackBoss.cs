using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackBoss : MonoBehaviour
{
    public Boss Boss;
    public List<MovementPath> availablePaths;
    public Rigidbody2D RigidBody2D;
    public float Speed;
    private MovementPath movementPath;
    private int currentPathNode = 0;
    private Vector2 endPoint;

    private void Update()
    {
        if (movementPath == null)
        {
            ChooseNewPath();
        }
        else
        {
            if (PathEndReached())
            {
                ChooseNewPath();
            } else
            {
                if (NextNodeReached())
                {
                    currentPathNode++;
                }
                FollowPath();
            }
        }
    }

    private void ChooseNewPath()
    {
        Debug.Log("Choose New Path");
        int roll = Random.Range(0, availablePaths.Count);
        movementPath = availablePaths[roll];
        endPoint = movementPath.path[movementPath.path.Count - 1];
        currentPathNode = 0;
    }

    private bool PathEndReached()
    {
        bool end = ((Vector2) transform.position == endPoint);
        Debug.Log("Path end check = " + end);
        return end;
    }

    private bool NextNodeReached()
    {
        bool reached = ((Vector2)transform.position == movementPath.path[currentPathNode]);
        Debug.Log("Node reached check = " + reached);
        return reached;
    }

    private void FollowPath()
    {
        Debug.Log("Moving");
        transform.position = Vector2.MoveTowards(transform.position, movementPath.path[currentPathNode], (Speed * Time.deltaTime));

    }
}
