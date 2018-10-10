using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 10.0F;

    public Slider staminaSlider;
    public int maxStamina;
    private int staminaFall;
    public int staminaFallMult;
    private int staminaRegen;
    public int staminaRegenMult;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;

        staminaFall = 1;
        staminaRegen = 1;
    }

    void Update()
    {
      
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            staminaSlider.value -= Time.deltaTime / staminaFall * staminaFallMult;
            speed = 12.0F;
        }
        else
        {
            staminaSlider.value += Time.deltaTime / staminaRegen * staminaRegenMult;
            speed = 6.0F;
        }

        if (staminaSlider.value >= maxStamina)
        {
            staminaSlider.value = maxStamina;
        }
        else if (staminaSlider.value <= 0)
        {
            staminaSlider.value = 0;
            speed = 3.0F;
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RadialBlur camera = GetComponentInChildren<RadialBlur>();
            camera.StartShader();
        }
    }

}


    



