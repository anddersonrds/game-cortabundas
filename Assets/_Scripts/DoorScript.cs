using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open,key;
    private Animator anim;
    private AudioSource audioData;
    public AudioClip[] fxSound;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        key = false;
        anim = GetComponent<Animator>();  
    }

    public void ChangeDoorState()
    {
        
        open = !open;
       
        if (open)
        {
            anim.SetBool("Open", open);            
            audioData.PlayOneShot(fxSound[0]);            
        }
        else
        {
            anim.SetBool("Open", open);
            audioData.PlayOneShot(fxSound[1]);           
        }       
    }
   
    public void KeyDoorOpen()
    {
        if (key)
        {
            open = !open;

            if (open)
            {
                anim.SetBool("Open", open);
                audioData.PlayOneShot(fxSound[0]);
            }
            else
            {
                anim.SetBool("Open", open);
                audioData.PlayOneShot(fxSound[1]);
            }
        }        
    }    
}
