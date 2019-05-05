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

    Sentity Sentity;

    private void Awake()
    {
        Sentity = GetComponent<Sentity>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance < Awareness)
        {
            Sentity.Target = Player.transform.position;
        }

        if (!Engaged)
        {
            if (distance < Engage)
            {
                Fire();
                Engaged = true;
            }
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
}
