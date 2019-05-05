using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExplosive : MonoBehaviour
{

    public Effect Explosion;
    public AudioClip SoundEffect;
    public float Blast = 1f;
    public int Damage = 10;
    public float Seconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Hit", Seconds);
    }

  

    private void Hit()
    {
        Explode(transform.position);

        Destroy(gameObject);
    }

    private void Explode(Vector2 pos)
    {
        var cs = Physics2D.OverlapCircleAll(pos, Blast);
        for (int i = 0; i < cs.Length; i++)
        {
            bool doDamage = true;


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
