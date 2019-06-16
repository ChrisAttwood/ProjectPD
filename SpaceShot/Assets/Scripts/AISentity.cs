using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISentity : MonoBehaviour
{
    GameObject Player;

    public float Awareness = 10f;
    public float Engage = 5f;
    public float TriggerRate = 1f;
    bool Engaged = false;
    public bool Runner;
    public bool Chase;
    public float jumpTimer = 1f;
    public float runnerChance = 0.5f;

    Sentity Sentity;

    private void Awake()
    {
        Sentity = GetComponent<Sentity>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RunnerCheck();
    }

    void RunnerCheck()
    {
        float randomRoll = Random.Range(0f, 1f);
        if (randomRoll < runnerChance)
        {
            Runner = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance < Awareness)
        {
            Sentity.Target = Player.transform.position;
            if (Runner)
            {
                Chase = true;
            }
        }

        if (!Engaged)
        {
            if (distance < Engage)
            {
                Fire();
                Engaged = true;
            }
        }
        
        if (Chase)
        {
            MoveItMoveIt();
        }
    }

    void Fire()
    {
        if (RoomToShoot())
        {
            Sentity.TriggerWeapon();
        }

       
        Invoke("Fire", TriggerRate);
    }

    public bool RoomToShoot()
    {
        if (Sentity.Equipment == null) return false;

        var direction = Sentity.Target - Sentity.Equipment.LaunchPoint.position;
        RaycastHit2D hit = Physics2D.Raycast(Sentity.Equipment.LaunchPoint.position, direction, 0.5f);

        if (hit.collider == null)
        {
            return true;
        }

        return false;
    }

    public void MoveItMoveIt()
    {
        jumpTimer -= Time.deltaTime;
        Vector2 playerPos = Player.transform.position;
        Vector2 entityPosition = this.transform.position;
        int dir = 0;
        if (playerPos.x > entityPosition.x)
        {
            dir = 1;
        } else
        {
            dir = -1;
        }
        bool jump = false;
        float jumpPower = 0;
        if (jumpTimer < 0)
        {
            jump = true;
            jumpPower = 1f;
            if (jumpTimer < -1)
            {
                jumpTimer = Random.Range(3f, 10f);
            }
        }
        Sentity.Move(new Vector2(dir, jumpPower), jump);
    }
}
