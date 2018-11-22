using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private float interactionDistance = 3f;
    public float jumpForce = 3.5f;
    private Rigidbody rb;
    private bool canJump;
    private CapsuleCollider playerColider;
    public Text interactText;
    private Camera cam;
    public GameObject keyDoor;
    
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
        cam = GetComponentInChildren<Camera>();       
        
    }

    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;   

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {            
            if (hit.collider.CompareTag("Door"))
            {                
                interactText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                    hit.collider.transform.parent.GetComponent<DoorScript>().ChangeDoorState();
            }
            else if (hit.collider.CompareTag("KeyDoor"))
            {               
                interactText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                    interactText.text = "Está trancada";
                    hit.collider.transform.parent.GetComponent<DoorScript>().KeyDoorOpen();
            }
            else if (hit.collider.CompareTag("Key"))
            {
                hit.collider.gameObject.SetActive(false);
                keyDoor.GetComponent<DoorScript>().key = true;
            }
            else
            {
                interactText.text = "Pressione (E) para interagir";
                interactText.gameObject.SetActive(false);
            }
        }

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

    private bool IsGrounded()
    {
        //Debug.DrawRay(playerColider.bounds.center,Vector3.down, Color.red);

        if(Physics.Raycast(playerColider.bounds.center,Vector3.down, playerColider.height/2))
        return true;

        return false;
    }

    public void DamagePlayer()
    {
        RadialBlur camera = GetComponentInChildren<RadialBlur>();
        camera.StartShader();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cena1Checkpoint")
        {
            GameObject ai = GameObject.Find("EnemyNPCCena1");
            GameObject area = GameObject.Find("StayArea");

            NpcAi aiScript = ai.GetComponent<NpcAi>();
            CheckPlayer aiCheck = ai.GetComponent<CheckPlayer>();
            DetectPlayer areaScript = area.GetComponent<DetectPlayer>();

            aiScript.Resume();
            aiCheck.ClearWarning();
            areaScript.ClearWarning();

            aiCheck.enabled = false;
        }
    }

}


    



