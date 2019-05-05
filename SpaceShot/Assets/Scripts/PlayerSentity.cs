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

        if (Input.GetKey(KeyCode.E))
        {
            if (throwCharge < throwLimit)
            {
                throwCharge += 1f;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Sentity.ThrowSomething(throwCharge);
            throwCharge = 0f;
        }

        var x = Input.GetAxis("Horizontal");
        //  var y = Input.GetAxis("Vertical");
        var y = 0f;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            y = 1f;
            Jumping = true;
        }

        //bool JumpHeld = false;
        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        //{
        //    JumpHeld = true;

        //}

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {


            Jumping = false;

        }

        Sentity.Move(new Vector2(x, y), Jumping);



        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Sentity.PickUp();
        }
    }
}
