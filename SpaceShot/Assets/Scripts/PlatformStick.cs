using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStick : MonoBehaviour
{
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    collision.transform.SetParent(this.transform);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    collision.transform.SetParent(null);
    //}

    private GameObject target = null;
    private Vector3 offset;
    void Start()
    {
        target = null;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }
}
