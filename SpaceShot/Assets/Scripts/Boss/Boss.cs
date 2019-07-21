using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Keep this script generic. Use it to check if the boss is triggered and to report it is complete. 
//Other stuff should go in other MonoBehaviours
public class Boss : MonoBehaviour
{
    public bool IsAwake = false;


    public void Complete()
    {
        GameObject.Find("Level").GetComponent<Level>().Complete();
    }
}
