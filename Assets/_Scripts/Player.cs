using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float chounchSpeed, jumpForce = 3.5f;
    public float speed;
    [SerializeField]
    private Text instructionText;
    [SerializeField]
    private GameObject keyDoor;

    private Light flashlight;

    private float interactionDistance = 2f;
    private Rigidbody rb;
    public bool pliers,canOpenDoor = true, canJump, isChounching = false, chain = false;
    private CapsuleCollider playerColider;
    private Camera cam;
    private Transform trCrounch;
    private int layerMask;
    private int timesInteractWarned = 0;
    private int timesCrouchedWarned = 0;
    private UnityEngine.UI.RawImage icon;
    private bool grabbing;
    private PlayerGrab grabScript;
    private GameMaster gm;
    private Lighting lightScript;
    public GameObject[] icones;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        playerColider = GetComponent<CapsuleCollider>();
        cam = GetComponentInChildren<Camera>();
        trCrounch = this.transform;
        flashlight = GetComponentInChildren<Light>();
        layerMask = LayerMask.GetMask("Hit");
        icon = GameObject.Find("Icon").GetComponent<UnityEngine.UI.RawImage>();
        grabScript = GetComponent<PlayerGrab>();
        lightScript = GetComponentInChildren<Lighting>();
        
        
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
        if (gm.hasFlashlight)
            TurnOnFlashlight();
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            bool keyPressed = false;
            if (timesInteractWarned == 0)
                showInteractionText("Pressione (E) para interagir com objetos");

            if (hit.collider.CompareTag("Door"))
            {
                icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                icon = icones[2].GetComponent<UnityEngine.UI.RawImage>();
                instructionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && canOpenDoor)
                {
                    hit.collider.transform.parent.GetComponent<DoorScript>().ChangeDoorState();
                    keyPressed = true;
                    canOpenDoor = false;
                    StartCoroutine(DoorAnimarion());
                }
            }
            else if (hit.collider.CompareTag("KeyDoor"))
            {
                instructionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E) && canOpenDoor)
                {
                    if (keyDoor.GetComponent<DoorScript>().key)
                    {
                        icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                        icon = icones[2].GetComponent<UnityEngine.UI.RawImage>();
                        hit.collider.transform.parent.GetComponent<DoorScript>().KeyDoorOpen();
                        keyPressed = true;
                        canOpenDoor = false;
                        StartCoroutine(DoorAnimarion());
                    }
                    else
                    {
                        icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                        icon = icones[3].GetComponent<UnityEngine.UI.RawImage>();
                        showInteractionText("Esta trancada");
                    }
                }
            }
            else if (hit.collider.CompareTag("InsideDoor"))
            {
                instructionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E) && canOpenDoor)
                {
                    if (!keyDoor.GetComponent<DoorScript>().inside)
                    {
                        hit.collider.transform.parent.GetComponent<DoorScript>().InsideDoorOpen();
                        keyPressed = true;
                        canOpenDoor = false;
                        StartCoroutine(DoorAnimarion());
                    }
                    else
                    {
                        showInteractionText("Esta trancada por dentro");
                    }
                }
            }
            else if (hit.collider.CompareTag("Key"))
            {
                icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                icon = icones[4].GetComponent<UnityEngine.UI.RawImage>();
                instructionText.text = "Chave de alguma porta";
                instructionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.SetActive(false);
                    keyPressed = true;
                    keyDoor.GetComponent<DoorScript>().key = true;
                }
            }
            else if (hit.collider.CompareTag("Flashlight"))
            {
                icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                icon = icones[6].GetComponent<UnityEngine.UI.RawImage>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.SetActive(false);
                    keyPressed = true;
                    TurnOnFlashlight();
                }
            }
            else if (hit.collider.CompareTag("Pliers"))
            {
                instructionText.text = "Alicate";
                instructionText.gameObject.SetActive(true);
                icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                icon = icones[9].GetComponent<UnityEngine.UI.RawImage>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.SetActive(false);
                    pliers = true;
                    keyPressed = true;
                }
            }
            else if (hit.collider.CompareTag("Chains"))
            {
                instructionText.text = "Corte a corrente";
                instructionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && pliers)
                {
                    chain = true;                  
                    hit.collider.gameObject.SetActive(false);                    
                    keyPressed = true;
                }
                else
                {
                    instructionText.text = "Falta uma ferramenta";
                }
            }
            else if (hit.collider.CompareTag("IronDoor"))
            {
                instructionText.text = "Abrir porta";
                instructionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && chain)
                {
                    hit.collider.transform.GetComponent<DoorScript>().IronLastDoor();
                    keyPressed = true;
                }
                else
                {
                    instructionText.text = "Porta trancada com corrente";
                }
            }
            else if (hit.collider.CompareTag("JumpScareDoor"))
            {
                Debug.Log("PortaJumpScare");
                if (Input.GetKeyDown(KeyCode.E) && canOpenDoor)
                {
                    Debug.Log("Chegou aqui");
                    hit.collider.transform.GetComponent<JumpScareDoors>().JumpScareSound();
                    keyPressed = true;
                    canOpenDoor = false;
                    StartCoroutine(DoorAnimarion());
                }                
            }
            else if (hit.collider.CompareTag("Grabbable"))
            {
                if (Input.GetKeyDown("e"))
                {
                    icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                    icon = icones[1].GetComponent<UnityEngine.UI.RawImage>();
                    if (timesInteractWarned == 0)
                    {
                        clearInteractionText();
                        setTimesInteractWarned();
                    }
                    if (grabbing)
                    {
                        grabbing = false;
                        grabScript.ReleaseObject();
                    }
                    else
                    {
                        grabbing = true;
                        grabScript.GrabObject();
                    }
                }
                else
                {
                    if (!grabbing)
                    {
                        icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                        icon = icones[0].GetComponent<UnityEngine.UI.RawImage>();
                    }
                }
            }
            else
            {
                icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            }
            icon.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);

            if (keyPressed && timesInteractWarned == 0)
            {
                timesInteractWarned = 1;
                clearInteractionText();
                keyPressed = false;
            }
        }
        else
        {
            if (icon.color.a > 0.0f)
                icon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            if (timesInteractWarned == 0 && instructionText.text != "")
                clearInteractionText();
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

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(playerColider.bounds.center, Vector3.down, Color.red);
        if (Physics.Raycast(playerColider.bounds.center, Vector3.down, playerColider.height / 2))
            return true;

        return false;
    }

    private void CrouchControll(bool crouch)
    {
        if (timesCrouchedWarned == 1)
        {
            clearInteractionText();
            timesCrouchedWarned = 2;
        }
        else if (timesCrouchedWarned == 0)
        {
            timesCrouchedWarned = 2;
        }

        if (crouch)
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

    public void ShowShadow()
    {
        Debug.Log("Show shadow");
        ShadowEffect camera = GetComponentInChildren<ShadowEffect>();
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

            if (aiScript.isStopped())
                aiScript.Resume();
            aiCheck.ClearWarning();
            areaScript.ClearWarning();

            aiCheck.enabled = false;
        }

        else if (other.gameObject.name == "CrouchTrigger")
        {
            if (timesCrouchedWarned == 0)
            {
                showInteractionText("Pressione Control Esquerdo (Ctrl) para agachar");
                timesCrouchedWarned = 1;
            }
        }

        else if (other.CompareTag("ShadowTrigger"))
        {
            ShowShadow();
            other.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Crouch Trigger" && timesCrouchedWarned == 1)
        {
            clearInteractionText();
        }
    }

    public void showInteractionText(string message)
    {
        instructionText.text = message;
        instructionText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void clearInteractionText()
    {
        instructionText.text = "";
        instructionText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    public int getTimesInteractWarned()
    {
        return timesInteractWarned;
    }

    public void setTimesInteractWarned()
    {
        timesInteractWarned += 1;
    }

    public void ReloadCheckpoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TurnOnFlashlight()
    {
        GameObject flashlightGO = GameObject.Find("Flashlight");
        Light spotLight = flashlightGO.GetComponentInChildren<Light>();
        spotLight.enabled = false;
        flashlight.enabled = true;
        lightScript.SetFlashlight(true);
    }

    IEnumerator DoorAnimarion()
    {        
        yield return new WaitForSeconds(4);
        canOpenDoor = true;
    }

}

