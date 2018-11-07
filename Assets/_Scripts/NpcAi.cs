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
        AtackPlayer();
    }

    void Update()
    {
        fov = GetComponent<FieldOfView>().ItsInFoV;

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
        else
        {
            Debug.Log("Procurando");
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
        //Debug.Log("Patrulhando");
    }

    void Chase()
    {
        if (fov == true)
        {
            //Debug.Log("Perseguindo");
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
        //Debug.Log("Ouvindo");
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

    void GoToLastPlayerPosition()
    {
        StartCoroutine(WaitForChase());
        if (Vector3.Distance(transform.position, LastPosPlayer) <= 2f)
        {
            agent.isStopped = true;
            anim.SetBool("Looking", true);            
        }
    }

    void AtackPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 2f && fov == true)
        {
            Debug.Log("Atacando");
            anim.SetBool("StabPlayer", true);
            agent.isStopped = true;
        }
        else
        {
            anim.SetBool("StabPlayer", false);
            agent.isStopped = false;
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
        yield return new WaitForSeconds(3);
        agent.isStopped = false;
        anim.SetBool("Looking", false);
        agent.SetDestination(LastPosPlayer);
        LastPosPlayer = player.transform.position;
        GoToLastPlayerPosition();
    }
}
