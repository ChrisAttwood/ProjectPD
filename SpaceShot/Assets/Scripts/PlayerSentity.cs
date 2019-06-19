using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSentity : MonoBehaviour
{
    Sentity Sentity;
 


    bool Jumping = false;
    float throwCharge = 0f;
    float throwLimit = 75f;

    private void Awake()
    {
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
            y = 1f;
            Jumping = true;
        }

     

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {


            Jumping = false;

        }

        Sentity.Move(new Vector2(x, y), Jumping);



        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Sentity.PickUp();
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

            Sentity.ThrowEffect.color = new Color(percent, percent/2f, 0f, (percent/2f) + 0.1f);

          

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
}
