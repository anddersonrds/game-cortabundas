﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideOpen : MonoBehaviour
{
    public GameObject dS;
    private bool inside = true;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dS.GetComponent<DoorScript>().inside = inside;            
        }            
    }

}
