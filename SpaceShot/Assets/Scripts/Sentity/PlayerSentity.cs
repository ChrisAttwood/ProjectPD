using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSentity : MonoBehaviour
{
    Sentity Sentity;
 


    bool Jumping = false;
    float throwCharge = 0f;
    float throwLimit;
    bool OnLadder = false;

    private void Awake()
    {
        throwLimit = Configuration.Data.Config.GrenadeThrowMax;
        Sentity = GetComponent<Sentity>();
    }
   

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Sentity.TriggerWeapon();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Sentity.Melee();
        }

        if (Input.GetKey(KeyCode.E) && Sentity.Grenades>0)
        {
            if (throwCharge < throwLimit)
            {
                throwCharge += 1f;
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && Sentity.Grenades > 0)
        {
            Sentity.ThrowSomething(throwCharge);
            throwCharge = 0f;
            Sentity.Grenades--;
            GrenadeCounter.instance.UpdateCounter(Sentity.Grenades);
        }

        var x = Input.GetAxis("Horizontal");
        var y = 0f;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!OnLadder)
            {
                y = 1f;
                Jumping = true;
            }
        }

        if (x == 0f && OnLadder)
        {
            Vector2 vel = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity.Set (0f, vel.y);
        }
     

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (!OnLadder)
            {

                Jumping = false;
            }

        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (OnLadder)
            {
                ClimbLadder(1);
            }
        }

        Sentity.Move(new Vector2(x, y), Jumping);



        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!OnLadder)
            {
                Sentity.PickUp();
            } else
            {
                ClimbLadder(-1);
            }
        }

        if (Input.GetAxis ("Vertical") < 0 && OnLadder)
        {
            ClimbLadder(-1);
        }

        UpdateThrowChargeDisplay();

    }

    void UpdateThrowChargeDisplay()
    {
        if (throwCharge > 0)
        {
            float percent = throwCharge / throwLimit;
            int index = (int)(percent * Sentity.ThrowCharge.Length);

            if(index>= Sentity.ThrowCharge.Length)
            {
                index = Sentity.ThrowCharge.Length -1;
            }
            Sentity.ThrowEffect.sprite = Sentity.ThrowCharge[index];

            Sentity.ThrowEffect.color = new Color(percent, percent/2f, 0f, (percent/4f) + 0.3f);

          

            ThrowAim();
        }
        else
        {
            Sentity.ThrowEffect.sprite = null;
           
        }
    }

    void ThrowAim()
    {
        bool flip = Sentity.Target.x < Sentity.transform.position.x;

        Vector3 vectorToTarget = Sentity.Target - Sentity.transform.position;

        float angle = 0f;
        if (flip)
        {
            angle = Mathf.Atan2(vectorToTarget.y, -vectorToTarget.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        }




        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        Sentity.ThrowEffect.transform.localRotation = Quaternion.Slerp(Sentity.ThrowEffect.transform.localRotation, q, Time.deltaTime  * 10f);
       
    }

    void ClimbLadder(int direction)
    {
        //transform.Translate(0.0f, direction * Time.deltaTime * 10f, 0f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, direction * Time.deltaTime * 10f), ForceMode2D.Impulse);
    }

    public void PutSentityOnLadder()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        OnLadder = true;
    }

    public void TakeSentityOffLadder()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        OnLadder = false;
    }
}
