using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcAi : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public bool fov;
    public Vector3 LastPosPlayer;
    public NavMeshAgent agent;    
    public GameObject[] wayPoints;
    public float accuracy = 3.0f;
    private int currentWP;    

    //IA Memory
    private int Count = 0;
    private bool seeLastPosition = false;
    private bool isChasing = false;


    //IA Hearing 
    Vector3 noisePosition;
    private bool canHear = false;
    public float noiseDistance = 50f;    
    private bool canLooking = false;

  
    void Awake()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoints");
    }

    void Start()
    {      
        anim = GetComponent<Animator>();
        currentWP = 0;
    }
    
    private void FixedUpdate()
    {
        Debug.Log(isChasing);
    }

    void Update()
    {
        fov = GetComponent<FieldOfView>().ItsInFoV;

        if (fov == false && canHear == false && seeLastPosition == false)
        {
            Patrol();
            NoiseCheck();
            CheckPlayerLastPos();
        }
        else if (canHear == true && fov == false)
        {
            canLooking = true;
            GoToNoisePosition();
        }
        else if (fov == true && isChasing == false)
        {
            Chase();            
        }
        else if (fov == false && seeLastPosition == true && canHear == false)
        {
            GoToLastPlayerPosition();
        }
    }

    void Patrol()
    {
        if (wayPoints.Length == 0) return;

        if (Vector3.Distance(wayPoints[currentWP].transform.position, this.transform.position) < accuracy)
        {
            currentWP++;
            if (currentWP >= wayPoints.Length)
            {
                currentWP = 0;
            }
        }
        agent.SetDestination(wayPoints[currentWP].transform.position);
        Debug.Log("Patrulhando");
    }

    void Chase()
    {
        if (fov)
        {
            Debug.Log("Perseguindo");
            agent.SetDestination(player.transform.position);
        }              
        else
        {
            isChasing = true;
        }
    }

    void NoiseCheck()
    {
        if (Input.GetButton("Fire1"))
        {
            noisePosition = player.transform.position;
            canHear = true;
        }
        else
        {
            canHear = false;
            canLooking = false;
        }
    }

    void GoToNoisePosition()
    {       
        Debug.Log("Ouvindo");
        agent.SetDestination(noisePosition);
        
        if (Vector3.Distance(transform.position, noisePosition) <= 1f && canLooking == true)
        {
            anim.SetBool("Looking", true);
            StartCoroutine(WaitTimeLokking());
        }
    }
    
    void CheckPlayerLastPos()
    {
        if (fov == false && isChasing == true)
        {
            LastPosPlayer = player.transform.position;
            seeLastPosition = true;
            canHear = false;          
        }
        else
        {
            seeLastPosition = false;
            isChasing = false;
        }
    }

    void GoToLastPlayerPosition()
    {
        
        agent.SetDestination(LastPosPlayer);      

        if(Vector3.Distance(transform.position, LastPosPlayer) <= 1f && seeLastPosition == true)
        {
            anim.SetBool("Looking", true);
            agent.isStopped = true;
            StartCoroutine(WaitForChase());
        }
    }

    IEnumerator WaitTimeLokking()
    {
        yield return new WaitForSeconds(5);

        anim.SetBool("Looking", false);
        canLooking = false;
        canHear = false;        
    }   

    IEnumerator WaitForChase()
    {
        agent.isStopped = false;
       if(Count <= 4)
        {
            Debug.Log("Contador: " + Count);
            for(int i= 0; i <=5; i++)
            {
                Count++;
                LastPosPlayer = player.transform.position;                
                GoToLastPlayerPosition();
                yield return null;
            }         
        }
        seeLastPosition = false;         
    }
}
