using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideOpen : MonoBehaviour
{
    private GameObject dS;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dS.GetComponent<DoorScript>().inside = true;           
        }            
    }

}
