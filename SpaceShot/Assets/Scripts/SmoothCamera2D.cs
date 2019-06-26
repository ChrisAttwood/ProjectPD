using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothCamera2D : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;


    void FixedUpdate()
    {
        if (target)
        {
            Vector3 t = new Vector3(target.position.x, target.position.y, 0f);
            Vector3 point = Camera.main.WorldToViewportPoint(t);
            Vector3 delta = t - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            destination.y =8.5f;// 4.5f;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }


}
