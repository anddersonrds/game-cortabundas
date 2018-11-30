using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    private AudioSource playerSource;
    public AudioClip andando, correndo;
    private float horizontal, vertical;
    private bool playing;
    private int pace; // -1: andando, 0: parado, 1: andando
    private bool changedPace;
    private PlayerRunning runningScript;

	void Start ()
    {
        playerSource = GetComponent<AudioSource>();
        playing = false;
        pace = 0;
        runningScript = GetComponent<PlayerRunning>();
    }
	
	void Update () 
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        bool running = runningScript.isRunning;

        if ((horizontal != 0 || vertical != 0))
        {
            if (running )
            {
                if (pace != 1)
                    changedPace = true;
                else
                    changedPace = false;
                pace = 1;
            }
            else
            {
                if (pace != -1)
                    changedPace = true;
                else
                    changedPace = false;
                pace = -1;
            }

            if (changedPace || !playing || (playing && !playerSource.isPlaying))
            {
                playing = true;
                MovimentSound(running);
            }
        }
        else
        {
            pace = 0;
            if (playing)
            {
                playerSource.Stop();
                playing = false;
            }
        }
        

	}

    private void MovimentSound(bool running)
    {
        if(running)
        {
            playerSource.clip = correndo;
        }
        else
        {
            playerSource.clip = andando;
        }
        playerSource.Play();
    }
}
