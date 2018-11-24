using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float chounchSpeed,speed,jumpForce = 3.5f;
    [SerializeField]
    private Text interactText;
    [SerializeField]
    private GameObject keyDoor;

    private float interactionDistance = 3f;    
    private Rigidbody rb;
    private bool canJump, isChounching = false;
    private CapsuleCollider playerColider;
    private Camera cam;
    private Transform trCrounch;
    
    
    //public AudioSource walking;
    //public AudioSource running;

    //public Slider staminaSlider;
    //public int maxStamina;
    //private int staminaFall;
    //public int staminaFallMult;
    //private int staminaRegen;
    //public int staminaRegenMult;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //staminaSlider.maxValue = maxStamina;
        //staminaSlider.value = maxStamina;

        //staminaFall = 1;
        //staminaRegen = 1;        
        rb = GetComponent<Rigidbody>();
        playerColider = GetComponent<CapsuleCollider>();
        cam = GetComponentInChildren<Camera>();
        trCrounch = this.transform;
    }

    private void FixedUpdate()
    {        

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
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isChounching = !isChounching;
            CrouchControll(isChounching);
        }

        /*
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
        */

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(playerColider.bounds.center,Vector3.down, Color.red);
        if(Physics.Raycast(playerColider.bounds.center,Vector3.down, playerColider.height/2))
        return true;

        return false;
    }
    
    private void CrouchControll(bool crounch)
    {
        if (crounch)
        {
            trCrounch.localScale = new Vector3(1, 0.42f, 1);
            speed = chounchSpeed;
        }
        else
        {
            trCrounch.localScale = new Vector3(1, 0.84f, 1);
            speed = 2;
        }
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
            GameObject ai = GameObject.Find("EnemyNPC");
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


    



