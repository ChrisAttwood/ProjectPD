using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

   
    public Effect Explosion;
    public Rigidbody2D Rigidbody2D;
    public SpriteRenderer SpriteRenderer;

    float Mass = 1f;
    float Gravity = 0f;
    float Blast = 1f;
    int Damage = 1;
    AudioClip SoundEffect;
    bool DamageAll;
    bool playerProjectile;


    void Awake()
    {
        //Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.mass = Mass/100f;
        Rigidbody2D.gravityScale = Gravity;
    }

    public void Create(Ammo ammo, bool playerBullet)
    {
        Rigidbody2D.velocity = Vector2.zero;
        DamageAll = ammo.DamageAll;
        playerProjectile = playerBullet;
        Mass = ammo.Mass;
        Gravity = ammo.Gravity;
        Blast = ammo.Blast;
        Damage = ammo.Damage;
        SoundEffect = ammo.SoundEffect;
        SpriteRenderer.sprite = ammo.Sprite;
        SpriteRenderer.color = ammo.Color;

        Rigidbody2D.mass = Mass / 100f;
        Rigidbody2D.gravityScale = Gravity;
       
    }



    public void Launch(Vector3 target,float power)
    {
        Rigidbody2D.AddForce(target * power * 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Hit();
    }

   

    private void Hit()
    {
        Explode(transform.position);

        ProjectilePool.instance.Return(this);

    }

    private void Explode(Vector2 pos)
    {
        var cs = Physics2D.OverlapCircleAll(pos, Blast);
        for(int i = 0;i< cs.Length; i++)
        {
            bool doDamage = true;

            if (!DamageAll)
            {
                Sentity sentity = cs[i].gameObject.GetComponent<Sentity>();
                if (sentity != null)
                {
                    if (sentity.IsPlayer && playerProjectile)
                    {
                        doDamage = false;
                    }
                }
            }
           
            if (doDamage)
            {
                var d = cs[i].gameObject.GetComponent<ITakeDamage>();
                if (d != null)
                {
                    d.TakeDamage(pos, Blast, Damage);
                }
            }

       
        }
        var ex = Instantiate(Explosion);
        ex.Create(Blast * 2f);
        ex.transform.position = pos;

        AudioSource.PlayClipAtPoint(SoundEffect, transform.position, 0.5f);

    }
}

[System.Serializable]
public class Ammo
{
    public Sprite Sprite;
    public Color Color = Color.white;
    public AudioClip SoundEffect;
    public float Mass = 1f;
    public float Gravity = 0f;
    public float Blast = 1f;
    public int Damage = 1;
    public bool DamageAll = false;
}
