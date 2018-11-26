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

    private void Start()
    {
        speedWalking = GetComponent<Player>().speed;
        staminaBar.maxValue = maxStamina;
        currentStamina = maxStamina;
        fallStamina = 1f;
        staminaRegen = 0.3f;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Runnig();

        }
        else
        {
            regenStamina();
        }
    }

    private void Runnig()
    {
        if(staminaBar.value > 0) {
            staminaBar.value -= fallStamina * Time.deltaTime;
            maxStamina = staminaBar.value;
            GetComponent<Player>().speed = speedRun;
        }
        else
        {
            GetComponent<Player>().speed = 1f;
        }
    }

    private void regenStamina()
    {
        GetComponent<Player>().speed = speedWalking;
        staminaBar.value += staminaRegen * Time.deltaTime;
        maxStamina = staminaBar.value;

        if (staminaBar.value >= currentStamina)
        {
            maxStamina = currentStamina;
            staminaBar.value = maxStamina;
        }
    }
}
