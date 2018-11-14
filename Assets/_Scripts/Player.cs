using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce = 3.5f;
    private Rigidbody rb;
    private bool canJump;
    private CapsuleCollider playerColider;
    
    public AudioSource walking;
    public AudioSource running;

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

        rb = GetComponent<Rigidbody>();
        playerColider = GetComponent<CapsuleCollider>();
        
    }

    private void FixedUpdate()
    {
        canJump = IsGrounded();        

        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        { 
            staminaSlider.value -= Time.deltaTime / staminaFall * staminaFallMult;
            speed = 6.0F;
        }
        else
        {
            staminaSlider.value += Time.deltaTime / staminaRegen * staminaRegenMult;
            speed = 4.0F;
            this.running.Play();
        }

        if (staminaSlider.value >= maxStamina)
        {
            staminaSlider.value = maxStamina;
        }
        else if (staminaSlider.value <= 0)
        {
            staminaSlider.value = 0;
            speed = 3.0F;
            this.running.Stop();
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

    private bool IsGrounded()
    {
        Debug.DrawRay(playerColider.bounds.center,Vector3.down, Color.red);

        if(Physics.Raycast(playerColider.bounds.center,Vector3.down, playerColider.height/2))
        return true;

        return false;
    }

}


    



