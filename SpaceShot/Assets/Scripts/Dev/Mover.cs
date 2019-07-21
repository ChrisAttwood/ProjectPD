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

    public bool OnLadder = false;
    public float LadderSpeed = 5f;


    public void Move(float x, bool JumpPressed, bool JumpHeld)
    {




        //var x = Input.GetAxis("Horizontal");
        var y = Rigidbody2D.velocity.y;
        float dynamicSpeed = xWalkSpeed + ((xRunSpeed - xWalkSpeed) * ((float)AcTimer / (float)RunTime));

        if (JumpPressed && Grounds.Count > 0)
        {
            y = JumpVelocity;
        }

        else if (OnLadder)
        {
            y = Input.GetAxis("Vertical") * LadderSpeed;
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
                x = 0;
            }
            else if (x < 0 && LeftCollisions.Count > 0)
            {
                x = 0;
            }

            Rigidbody2D.velocity = new Vector2(x * xRunSpeed, y);

        }
        else
        {
            if (x > 0 && RightCollisions.Count > 0)
            {
                x = 0;
            }
            else if (x < 0 && LeftCollisions.Count > 0)
            {
                x = 0;
            }

            Rigidbody2D.velocity = new Vector2(x * dynamicSpeed, y);

        }

        if (Rigidbody2D.velocity.y < 0)
        {
            Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (FallMultipler - 1) * Time.deltaTime;
        }
        else if (Rigidbody2D.velocity.y > 0 && !JumpHeld)
        {
            Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }



        //void ClimbLadder(float direction)
        //{
        //    //transform.Translate(0.0f, direction * Time.deltaTime * 10f, 0f);
        //    //GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, direction * Time.deltaTime * 10f), ForceMode2D.Impulse);
        //    Rigidbody2D.AddForce(new Vector2(0f, direction * Time.deltaTime * 100f));
        //}

    }

    public void PutSentityOnLadder(float ThisLadderSpeed)
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        LadderSpeed = ThisLadderSpeed;
        OnLadder = true;
    }

    public void TakeSentityOffLadder()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        OnLadder = false;
    }

}
