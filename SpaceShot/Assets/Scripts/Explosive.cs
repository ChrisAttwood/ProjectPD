using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour, ITakeDamage
{
    public float Blast;
    public int Damage;
    public Effect Explosion;
    public AudioClip SoundEffect;

    bool Exploding = false;


    public void Explode()
    {
        if (Exploding) return;

        Exploding = true;

        Vector2 pos = transform.position;
        var cs = Physics2D.OverlapCircleAll(pos, Blast);
        for (int i = 0; i < cs.Length; i++)
        {
            if(cs[i].gameObject != this.gameObject)
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

        Destroy(this.gameObject);

    }

    public void TakeDamage(Vector2 source, float radius, int amount)
    {
        Explode();
    }
}
