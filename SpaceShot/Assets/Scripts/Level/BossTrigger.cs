using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("BossTrigger");
        Camera.main.GetComponent<SmoothCamera2D>().target = this.transform;
        Camera.main.GetComponent<SmoothCamera2D>().dampTime = 1f;
    }
}
