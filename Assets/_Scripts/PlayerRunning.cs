using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRunning : MonoBehaviour {

    public GameObject player;
    public float speedRun;
    private float speedWalking;

    public Slider staminaBar;
    public float maxStamina;
    private float currentStamina;
    private float fallStamina;
    private float staminaRegen;
    private bool tiredSteps;

    private void Start()
    {
        speedWalking = GetComponent<Player>().speed;
        staminaBar.value = maxStamina;
        currentStamina = maxStamina;
        fallStamina = 1f;
        staminaRegen = 0.3f;
        tiredSteps = false;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Runnig();
        }
        else if(GetComponent<Player>().isChounching)
        {
            Chounching();
        }
        else
        {
            RegenStamina();
        }
    }

    private void Runnig()
    {
        if (!GetComponent<Player>().isChounching)
        {
            if ((staminaBar.value > 0) && (tiredSteps == false))
            {
                staminaBar.value -= fallStamina * Time.deltaTime;
                maxStamina = staminaBar.value;
                GetComponent<Player>().speed = speedRun;
            }
            else
            {
                tiredSteps = true;
                RegenStamina();
            }
        }
    }

    private void RegenStamina()
    {
        if (tiredSteps)
        {
            GetComponent<Player>().speed = 1f;
            Regen();
        }
        else
        {
            GetComponent<Player>().speed = speedWalking;
            Regen();
        }
            
    }

    private void Regen()
    {
        staminaBar.value += staminaRegen * Time.deltaTime;
        maxStamina = staminaBar.value;

        if (staminaBar.value >= currentStamina)
        {
            maxStamina = currentStamina;
            staminaBar.value = maxStamina;
            tiredSteps = false;
        }
    }

    private void Chounching()
    {
        GetComponent<Player>().speed = 1f;
        Regen();
    }

}
