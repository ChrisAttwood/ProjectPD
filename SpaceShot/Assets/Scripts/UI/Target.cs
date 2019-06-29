using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    private Camera cam;
    public Sentity Sentity { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Sentity != null)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;

            transform.position = Vector3.MoveTowards(transform.position, point, 1f);
            Sentity.Target = transform.position;
        }
     
    }
}
