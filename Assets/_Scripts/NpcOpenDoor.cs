using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOpenDoor : MonoBehaviour
{
    public DoorScript OpenDoor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {            
            OpenDoor.GetComponent<DoorScript>().NpcOpenDoor();
        }
    }
}
