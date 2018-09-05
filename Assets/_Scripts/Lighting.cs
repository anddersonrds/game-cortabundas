using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour {

    Light playerLight;
    bool flashing;
    float flashingTimer;
    float chanceTimer;
    string flashEffect;
    Color lightColor = new Color(1.0f, 1.0f, 1.0f);
    Color candleColor = new Color(0.96f, 0.38f, 0.03f);
    // Use this for initialization
    void Start () {
        playerLight = GetComponent<Light>();
        flashing = false;
        chanceTimer = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        bool flashlightPressed = Input.GetKeyDown(KeyCode.F);
        bool candlePressed = Input.GetKeyDown(KeyCode.C);
        if ((flashlightPressed || candlePressed) && !flashing)
        {
            string lightType;
            if (flashlightPressed)
                lightType = "flashlight";
            else
                lightType = "candle";
            setLightType(lightType);
            playerLight.enabled = !playerLight.enabled;
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
                playerLight.enabled = false;
            else
                playerLight.enabled = !playerLight.enabled;
        }
        if (flashingTimer < 0)
        {
            playerLight.enabled = true;
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

    private void setLightType(string lightType)
    {
        if (lightType == "flashlight")
        {
            playerLight.type = LightType.Spot;
            playerLight.intensity = 5.0f;
            playerLight.color = lightColor;
        }
        else
        {
            playerLight.type = LightType.Point;
            playerLight.intensity = 1.0f;
            playerLight.color = candleColor;
        }
    }
}
