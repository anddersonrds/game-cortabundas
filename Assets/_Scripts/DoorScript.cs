using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open,key;
    private Animator anim;

    void Start()
    {
        key = false;
        anim = GetComponent<Animator>();  
    }

    private void Update()
    {
        if (open)
        {
            anim.SetBool("Open", open);  
        }
        else
        {
            anim.SetBool("Open",open);
        }
    }

    public void ChangeDoorState()
    {       
        open = !open;
    }  

    public void KeyDoorOpen()
    {
        if (key)
        {
            open = !open;
        }        
    }

}
