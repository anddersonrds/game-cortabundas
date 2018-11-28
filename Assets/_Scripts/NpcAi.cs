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
    public Transform[] wayPoints;
    public float accuracy = 3.0f;
    private int currentWP = 0;
    private bool stopped = false;

    //IA Memory
    private float searchTime = 3;
    private bool seeLastPosition = false;
    private bool isChasing = false;
    private bool attacking = false;


    //IA Hearing 
    Vector3 noisePosition;
    private bool canHear = false;
    public float noiseDistance = 50f;
    private bool canLooking = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (wayPoints.Length == 0)
            return;

        agent.destination = wayPoints[currentWP].position;
        currentWP = (currentWP + 1) % wayPoints.Length;
        
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f && fov)
            AttackPlayer();
        else
        {
            if (attacking)
            {
                anim.SetBool("StabPlayer", false);
                agent.isStopped = false;
                attacking = false;
            }
        }
    }

    void Update()
    {      
        fov = GetComponent<FieldOfView>().ItsInFoV;

        if (stopped)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f && fov == false && canHear == false && seeLastPosition == false && isChasing == false)
        {
            GoToNextPoint();
            NoiseCheck();
        }
        else if (canHear == true && fov == false)
        {
            canLooking = true;
            GoToNoisePosition();
            Debug.Log("Escutou");
        }
        else if (fov == true && isChasing == false)
        {
            Chase();
        }
        else if(isChasing == true && !attacking)
        {            
            SearchingPlayer();
        }
    }  

    void Chase()
    {
        if (fov == true && !attacking)
        {           
            agent.SetDestination(player.transform.position);
            isChasing = true;            
        }
    }

    void NoiseCheck()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Apertou mouse1");
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

    void AttackPlayer()
    {
        attacking = true;
        player.GetComponent<Player>().DamagePlayer();
        anim.SetBool("StabPlayer", true);
        agent.speed = 0;
        StartCoroutine(WaitTimeAttack());
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
            searchTime = 3;
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

    IEnumerator WaitTimeAttack()
    {
        yield return new WaitForSeconds(2.10f);
        agent.speed = 1.8f;
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

    public bool isStopped()
    {
        return agent.isStopped;
    }
}
