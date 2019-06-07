using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentity : MonoBehaviour, ITakeDamage
{

    public Vector3 Target { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }
    public bool IsPlayer = true;

    public Equipment Equipment;
    public GameObject SoulHandLeft;
    public GameObject SoulHandRight;
    public GameObject SoulFootLeft;
    public GameObject SoulFootRight;

    public GameObject BodyArmor;

    public SpriteRenderer BodySprite;
    public SpriteRenderer LegSprite;

    public SpriteRenderer[] Skins;

    public Color[] SkinTones;

    public SpriteRenderer Hair;
    public Color[] HairTones;
    public Sprite[] HairStyles;

    public Vector2? PunchLeft;
    public Vector2? PunchRight;

    public bool IsGrounded { get; set; }

    public bool IsDead;

    public float Speed { get; set; }
    public int Health { get; set; }
    public int Armour { get; set; }

    public List<Collider2D> Grounds;

    public Throwable throwableEquipped;

    public bool scoreLogged = false;

    public void Create(Gang sd)
    {
      


        Speed = sd.Speed;
        Health = sd.Health;
        IsPlayer = false;
        BodySprite.color = sd.BodyColour;
        LegSprite.color = sd.LegColour;

        if (Random.Range(0f, 1f) > 0.75f)
        {
            Equipment = Instantiate(sd.RareWeapon);
        }
        else
        {
            Equipment = Instantiate(sd.CommonWeapon);
        }



        var ai = gameObject.AddComponent<AISentity>();
        ai.Awareness = sd.Awareness;
        ai.Engage = sd.Engage;
        ai.TriggerRate = sd.TriggerRate;

       
    }

    public void CreatePlayer(Equipment equipment)
    {
        gameObject.tag = "Player";
        var player = gameObject.AddComponent<PlayerSentity>();
        GameObject.Find("Target").GetComponent<Target>().Sentity = this;
        Camera.main.GetComponent<SmoothCamera2D>().target = this.transform;
        Speed = 5f;
        Health = 10;
        Equipment = Instantiate(equipment);
    }


    private void Awake()
    {
        Grounds = new List<Collider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();

        var st = SkinTones[Random.Range(0, SkinTones.Length)];
        foreach(var sr in Skins)
        {
            sr.color = st;
        }

        Hair.color =  HairTones[Random.Range(0, HairTones.Length)];
        Hair.sprite = HairStyles[Random.Range(0, HairStyles.Length)];
    }


    private void Start()
    {
        BodyArmor.SetActive(false);
        if (Equipment != null)
        {
            Equip(Equipment);
        }
    }

    public void Equip(Equipment equipment)
    {
        
        equipment.Equip(this);
    }


    public void PickUp()
    {
        var cs = Physics2D.OverlapCircleAll(transform.position, 2f);

        Equipment Found = null;

        for (int i = 0; i < cs.Length; i++)
        {
            var e = cs[i].gameObject.GetComponent<Equipment>();
            if (e != null)
            {
                Found = e;
                
            }

        }

        if (Found != null)
        {
            if (Equipment != null)
            {
                Equipment.Drop();
            }
            Found.transform.localScale = transform.localScale;
            Equipment = Found;
            Found.Equip(this);
        }

    }


    void Update()
    {
        if (IsPlayer && !IsDead)
        {
            CheckWin();
        }

        if (IsDead) return;

        if (Target.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (Equipment != null)
            {
                Equipment.Aim(Target,true);
            }

        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (Equipment != null)
            {
                Equipment.Aim(Target,false);
            }

        }

        

        if (Equipment == null)
        {
            if (PunchLeft != null)
            {
                if(Vector2.Distance(SoulHandLeft.transform.localPosition, PunchLeft.Value) == 0)
                {
                    PunchLeft = null;
                    LandPunch(SoulHandLeft);
                }
                else
                {
                    Punch(SoulHandLeft, PunchLeft);
                }
            }
            else
            {
                Punch(SoulHandLeft, new Vector2(-0.2f,0f));
            }


            if (PunchRight != null)
            {
                if (Vector2.Distance(SoulHandRight.transform.localPosition, PunchRight.Value) == 0)
                {
                    PunchRight = null;
                    LandPunch(SoulHandRight);
                }
                else
                {
                    Punch(SoulHandRight, PunchRight);
                }
            }
            else
            {
                Punch(SoulHandRight, new Vector2(0.2f, 0f));
            }
        }

        if (!IsDead)
        {
            Step();
        }

        if (Armour > 0)
        {
            BodyArmor.SetActive(true);
        } else
        {
            BodyArmor.SetActive(false);
        }
    }

    void LandPunch(GameObject hand)
    {
        Vector2 pos = hand.transform.position;

        var cs = Physics2D.OverlapCircleAll(pos, 0.1f);

        for (int i = 0; i < cs.Length; i++)
        {
            var s = cs[i].gameObject.GetComponentInParent<Sentity>();
            if (s != null && s != this)
            {
               // var direction = (Vector2)cs[i].transform.position - pos;

               // s.TakeDamage(pos, direction * 100f , 10);
                s.TakeDamage(pos, 0.1f, 10);
            }

        }
    }

    void Punch(GameObject hand,Vector2? Target)
    {
         hand.transform.localPosition = Vector2.MoveTowards(hand.transform.localPosition, Target.Value, 0.05f);
    }

    void CheckWin()
    {
        if (transform.position.x > 260f && !scoreLogged)
        {
            Scoreboard.scoreboard.IncreaseScore(100);
            RunScore.instance.Display();
            Scoreboard.scoreboard.LogScore();
            Disarm();
            UIManager.instance.Victory();
        }
        if (transform.position.x < -20f)
        {
            Kill();
        }
    }

    public void TriggerWeapon()
    {
        if (Equipment != null)
        {
            Equipment.Trigger(IsPlayer);
        }
    }

    public void Melee()
    {
        if (Equipment != null)
        {
            SoulHandLeft.transform.parent = this.transform;
            SoulHandRight.transform.parent = this.transform;
            Equipment.Throw(Target);
            Equipment = null;
        }
        else
        {
            LandPunch(SoulHandLeft);
            LandPunch(SoulHandRight);

            PunchLeft = new Vector2(Random.Range(0.2f, 0.5f), Random.Range(-0.1f, 0.2f));
            PunchRight = new Vector2(Random.Range(0.2f, 0.5f), Random.Range(-0.1f, 0.2f));
        }

    }

    public void ThrowSomething(float force)
    {
        float mod = 0f;
        if (transform.localScale.x < 0)
        {
            mod = -0.3f;
        } else
        {
            mod = 0.3f;
        }
        Vector2 throwPosition = transform.position;
        throwPosition.x = throwPosition.x + mod;
        Throwable throwable = GameObject.Instantiate(throwableEquipped, throwPosition, throwableEquipped.transform.rotation);
        throwable.ThrowMe(Target, force);
    }



    public void TakeDamage(Vector2 source, float radius, int amount)
    {
        var direction = (Vector2)transform.position - source;
        if (Armour < 1)
        {
            var bloodPos = (source + (Vector2)transform.position * 3) / 4f;
            //GameObject.Instantiate(Resources.Load("Effects/BloodEffect"), bloodPos, transform.rotation);
            Health -= amount;
            if (Health <= 0)
            {
                Kill();
                GameObject.Instantiate(Resources.Load("Effects/KillBloodEffect"), bloodPos, transform.rotation);
            } else
            {
                GameObject.Instantiate(Resources.Load("Effects/BloodEffect"), bloodPos, transform.rotation);
            }
        }
        else
        {
            Armour -= amount;
            if (Armour < 0)
            {
                Armour = 0;
            }
        }
        Knock(source, direction * radius * 100f);

    }


    void Kill()
    {
        Disarm();
        if (IsPlayer)
        {
            UIManager.instance.GameOver();
            RunScore.instance.Display();
            Scoreboard.scoreboard.LogScore();
            scoreLogged = true;
        } else if (!IsDead)
        {
            Scoreboard.scoreboard.IncreaseScore(10);
        }
        IsDead = true;
    }

    void Knock(Vector2 Source, Vector2 force)
    {
        if (IsDead)
        {
            Rigidbody2D.constraints = RigidbodyConstraints2D.None;
        }

        Rigidbody2D.AddForceAtPosition(force, Source);
    }
   

    void Disarm()
    {
        SoulHandLeft.transform.parent = this.transform;
        SoulHandRight.transform.parent = this.transform;

        if (Equipment != null)
        {
            Equipment.Drop();
            Equipment = null;
        }
    }



    public void Move(Vector2 vector2,bool Jumping)
    {
        if (IsDead) return;


        var v2 = vector2;
        if (Mathf.Abs(v2.x) ==0f && Mathf.Abs(Rigidbody2D.velocity.y) == 0f)
        {
            var vel = Rigidbody2D.velocity;
            vel.x = 0f;
            Rigidbody2D.velocity = vel;
        }
        if (Mathf.Abs(Rigidbody2D.velocity.x) > Speed)
        {
            v2.x = 0f;
        }
        if (Mathf.Abs(Rigidbody2D.velocity.y) > Speed)
        {
            v2.y = 0f;
        }
        if (Grounds.Count>0)
        {
            if (v2.y > 0f)
            {
                v2.y = 4f;
            }
            Rigidbody2D.AddForce(v2 * 100f);
            IsGrounded = false;
        }
        else
        {
            if (Rigidbody2D.velocity.y < 0)
            {
                v2.y = -0.5f;
            }
            else if (!Jumping)
            {
                v2.y = -0.5f;
            }
            Rigidbody2D.AddForce(v2 * 50f);
        }
        if (Jumping)
        {
            Grounds = new List<Collider2D>();
        }
    }


   
    public void Step()
    {
        float speed = Mathf.Abs(Rigidbody2D.velocity.x);



        if (Grounds.Count > 0 && speed>0f)
        {

            float s = Mathf.Sin(Time.time * 20f) / 20f;
           
            SoulFootLeft.transform.localPosition = new Vector2(-0.15f , -0.3f + s);
            SoulFootRight.transform.localPosition = new Vector2(0.15f, -0.3f - s);
            
           
           
        }else if (Grounds.Count == 0)
        {
            SoulFootLeft.transform.localPosition = new Vector2(-0.15f, -0.37f);
            SoulFootRight.transform.localPosition = new Vector2(0.15f, -0.27f);
        }
        else if(speed == 0f)
        {
            SoulFootLeft.transform.localPosition = new Vector2(-0.15f,  -0.3f);
            SoulFootRight.transform.localPosition = new Vector2(0.15f,  -0.3f);
        }



    }

   
}
