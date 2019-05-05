using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public AudioClip SoundEffect;
    public Transform LaunchPoint;
    public Transform LeftHandle;
    public Transform RightHandle;
    public float Response = 1f;
    public int Burst = 1;
    [Min(0.05f)]
    public float Rate = 0.1f;
    [Range(0f, 1f)]
    public float Power = 1f;
    public Ammo Ammo;

    private bool playerShot;

    Rigidbody2D Rigidbody2D;
    Collider2D Collider2D;

   
    public Projectile Projectile;

    bool Flipped = false;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
    }

    public void Equip(Sentity sentity)
    {
        Collider2D.enabled = false;
        Destroy(Rigidbody2D);

        transform.parent = sentity.transform;
        transform.localPosition = Vector2.zero;


        if(LeftHandle !=null && sentity.SoulHandLeft != null)
        {
            sentity.SoulHandLeft.transform.parent = LeftHandle;
            sentity.SoulHandLeft.transform.localPosition = Vector3.zero;
            sentity.SoulHandLeft.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        if (RightHandle != null && sentity.SoulHandRight != null)
        {
            sentity.SoulHandRight.transform.parent = RightHandle;
            sentity.SoulHandRight.transform.localPosition = Vector3.zero;
            sentity.SoulHandRight.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void Drop()
    {
        transform.parent = null;
        Collider2D.enabled = true;
        Rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        Rigidbody2D.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(1f,2f)) * Random.Range(200f,500f));
    }

    public void Trigger(bool isPlayer)
    {
        playerShot = isPlayer;
        if (Burst == 1)
        {
            Fire();
            return;
        }

        float time = 0f;
        for(int i = 0; i < Burst; i++)
        {
            Invoke("Fire", time);
            time += Rate;
        }
       
    }

    void Fire()
    {
        var projectile = Instantiate(Projectile);
        projectile.Create(Ammo, playerShot);
        projectile.transform.position = LaunchPoint.position;
        if (Flipped)
        {
            projectile.Launch(transform.right * -1f, Power);
        }
        else
        {
            projectile.Launch(transform.right, Power);
        }

        AudioSource.PlayClipAtPoint(SoundEffect, transform.position, 1f);
    }

    public void Aim(Vector3 target,bool flip)
    {
        Flipped = flip;
        Vector3 vectorToTarget = target - transform.position;

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
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, Time.deltaTime * Response * 10f);
    }


    public void Throw(Vector3 target)
    {

        transform.parent = null;
        Collider2D.enabled = true;
        Rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        var direction = target - transform.position;
        direction.Normalize();
        Rigidbody2D.AddForce(direction * 1000f);
    }

}
