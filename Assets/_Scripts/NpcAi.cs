using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcAi : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public bool fov;
    public Vector3 LastPosPlayer, playerPos;
    public NavMeshAgent agent;
    public GameObject[] wayPoints;
    public float accuracy = 3.0f;
    private int currentWP;
    private bool stopped = false;

    //IA Memory
    private float searchTime = 5;
    private bool seeLastPosition = false;
    private bool isChasing = false;


    //IA Hearing 
    Vector3 noisePosition;
    private bool canHear = false;
    public float noiseDistance = 50f;
    private bool canLooking = false;


    void Awake()
    {
        //wayPoints = GameObject.FindGameObjectsWithTag("waypoints");
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        currentWP = 0;
    }

    private void FixedUpdate()
    {
        AtackPlayer();
    }

    void Update()
    {      
        fov = GetComponent<FieldOfView>().ItsInFoV;

        if (stopped)
            return;

        if (fov == false && canHear == false && seeLastPosition == false && isChasing == false)
        {
            Patrol();
            NoiseCheck();
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
        else if(fov == false && isChasing == true)
        {            
            SearchingPlayer();
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
    }

    void Chase()
    {
        if (fov == true)
        {           
            agent.SetDestination(player.transform.position);
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
        agent.SetDestination(noisePosition);

        if (Vector3.Distance(transform.position, noisePosition) <= 2f && canLooking == true)
        {
            anim.SetBool("Looking", true);
            StartCoroutine(WaitTimeLokking());
        }
        else if (Vector3.Distance(transform.position, noisePosition) <= 2f && fov == true)
        {
            canHear = false;
            canLooking = false;
            Chase();
        }
    }   

    void AtackPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 2f && fov == true)
        {
            player.GetComponent<Player>().DamagePlayer();
            anim.SetBool("StabPlayer", true);
            agent.isStopped = true;
        }
        else
        {
            anim.SetBool("StabPlayer", false);
            agent.isStopped = false;
        }
    }

    void SearchingPlayer()
    {
        if (searchTime > 0 && fov == false)
        {          
            searchTime -= Time.deltaTime;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            searchTime = 5;
            isChasing = false;           
        }        
    }

    IEnumerator WaitTimeLokking()
    {
        yield return new WaitForSeconds(5);

        anim.SetBool("Looking", false);
        canLooking = false;
        canHear = false;
    }

    public void Stop()
    {
        anim.SetBool("Looking", true);
        agent.isStopped = true;
        stopped = true;
    }

    public void Resume()
    {
        anim.SetBool("Looking", false);
        agent.isStopped = false;
        stopped = false;
        agent.SetDestination(wayPoints[currentWP].transform.position);
    }
}
