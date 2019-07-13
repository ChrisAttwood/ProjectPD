using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public Rigidbody2D Rigidbody2D;
    public float xWalkSpeed = 2f;
    public int RunTime = 60;
    public float xRunSpeed = 4f;
    int AcTimer;

    public Collider2D LeftSide;
    public Collider2D RightSide;



    public float JumpVelocity = 7f;
    public float FallMultipler = 2.5f;
    public float LowJumpMultiplier = 2f;

    public List<Collider2D> RightCollisions;
    public List<Collider2D> LeftCollisions;
    public List<Collider2D> Grounds;


    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Rigidbody2D.velocity.y;
        float dynamicSpeed = xWalkSpeed + ((xRunSpeed - xWalkSpeed) * ((float)AcTimer / (float)RunTime));

        if (Input.GetButtonDown("Jump") && Grounds.Count>0)
        {
            y = JumpVelocity;
        }

        if (x == 0)
        {
            AcTimer = 0;
        }
        else
        {
            AcTimer++;
        }

        if (AcTimer > RunTime)
        {
            if (x > 0 && RightCollisions.Count > 0)
            {
                // Do nothing
            }
            else if (x < 0 && LeftCollisions.Count > 0)
            {
                // Do nothing
            }
            else
            {
                Rigidbody2D.velocity = new Vector2(x * xRunSpeed, y);
            }
        }
        else
        {
            if (x > 0 && RightCollisions.Count > 0)
            {
                // Do nothing
            }
            else if (x < 0 && LeftCollisions.Count > 0)
            {
                // Do nothing
            }
            else
            {
                Rigidbody2D.velocity = new Vector2(x * dynamicSpeed, y);
            }
        }

        if (Rigidbody2D.velocity.y < 0)
        {
            Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (FallMultipler - 1) * Time.deltaTime;
        } else if (Rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

  
}
