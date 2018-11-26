using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour {

    bool hasFlashlight;
    Light playerLight;
    bool flashing;
    float flashingTimer;
    float chanceTimer;
    string flashEffect;
    // Use this for initialization
    void Start () {
        playerLight = GetComponent<Light>();
        flashing = false;
        chanceTimer = 0.0f;
        hasFlashlight = false;
        
        playerLight.intensity = 1.0f;
        playerLight.color = new Color(1.0f, 1.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        bool flashlightPressed = Input.GetKeyDown(KeyCode.F);
        if (flashlightPressed && hasFlashlight && !flashing)
        {
            if (playerLight.intensity == 0.0f)
                playerLight.intensity = 1.0f;
            else
                playerLight.intensity = 0.0f;
        }
        
        if (playerLight.enabled || flashing)
        {
            if (!flashing && chanceTimer == 0.0f)
            {
                float chance = Random.Range(0.0f, 1.0f);
                if (chance > 0.99f)
                {
                    flashingTimer = Random.Range(0.0f, 2.0f);
                    chanceTimer = 10.0f;
                    flashing = true;

                    if (flashingTimer > 1.0f)
                        flashEffect = "blink";
                    else
                        flashEffect = "off";
                }
            }
            else
            {
                applyFlash();
            }
        }
        checkTimer();
	}

    private void applyFlash()
    {
        if (flashing)
        {
            flashingTimer -= Time.deltaTime;

            if (flashEffect == "off")
                playerLight.intensity = 0.0f;
            else
            {
                if (playerLight.intensity == 0.0f)
                    playerLight.intensity = 1.0f;
                else
                    playerLight.intensity = 0.0f;
            }
        }
        if (flashingTimer < 0)
        {
            playerLight.intensity = 1.0f;
            flashing = false;
            flashingTimer = 0.0f;
        }
    }


    private void checkTimer()
    {
        chanceTimer -= Time.deltaTime;
        if (chanceTimer < 0.0f)
            chanceTimer = 0.0f;
    }

    public void SetFlashlight(bool value)
    {
        hasFlashlight = value;
    }
}
