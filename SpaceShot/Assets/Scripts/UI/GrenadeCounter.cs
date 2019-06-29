using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCounter : MonoBehaviour
{
    public static GrenadeCounter instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject[] Icons;
    
    public void UpdateCounter(int amount)
    {
        for(int i = 0; i< Icons.Length; i++)
        {
            if (i < amount)
            {
                Icons[i].SetActive(true);
            }
            else
            {
                Icons[i].SetActive(false);
            }
        }


    }

}
