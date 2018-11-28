using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareDoors : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip[] fxClip;



    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void JumpScareSound()
    {
        audio.PlayOneShot(fxClip[Random.Range(0,fxClip.Length)]);        
    }
}
