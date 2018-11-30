using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioSource andando, correndo;
    private float horizontal, vertical;

	void Start () {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("vertical");
    }
	
	void Update () 
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("vertical");

        if(horizontal != 0 && vertical != 0)
        {
            MovimentSound();
        }  
	}

    private void MovimentSound()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            correndo.Play();
        }
        else{
            andando.Play();
        }
    }
}
