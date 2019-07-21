using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject BlockerRight;
    public GameObject BlockerLeft;

    bool Triggered;

    public Boss Boss;

    private void Start()
    {
        BlockerLeft.transform.localPosition = new Vector2(-17, 18);
        BlockerRight.transform.localPosition = new Vector2(17, 18);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Triggered = true;
        Camera.main.GetComponent<SmoothCamera2D>().target = this.transform;
        Camera.main.GetComponent<SmoothCamera2D>().dampTime = 1f;

        Boss.IsAwake = true;
    }

    

    private void Update()
    {
        if (Triggered)
        {
            BlockerLeft.transform.localPosition = Vector2.MoveTowards(BlockerLeft.transform.localPosition, new Vector2(-17, 0), 1f);
            BlockerRight.transform.localPosition = Vector2.MoveTowards(BlockerRight.transform.localPosition, new Vector2(17, 0), 1f);
        }
    }


}
