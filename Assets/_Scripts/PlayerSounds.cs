using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioSource walkingSound, runningSound;

	void Start () {
		
	}
	
	void Update () 
    {
        MovimentSound();
	}

    private void MovimentSound()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            runningSound.Play();
        }
        else{
            walkingSound.Play();
        }
    }
}
