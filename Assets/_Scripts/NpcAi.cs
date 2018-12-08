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
    public float accuracy = 0.5f;
    private int currentWP = 0;
    private bool stopped = false;
    private AudioSource npcSource;

    //IA Memory
    private float searchTime = 3;
    private bool isChasing = false;
    private bool attacking = false;
    private bool arrivedWaypoint = false;


    //IA Hearing 
    Vector3 noisePosition;
    public bool canHear = false;
    public float noiseDistance = 50f;
    private bool canLooking = false;
    public GlassTrigger glassTrigger;


    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
        npcSource = GetComponent<AudioSource>();
    }

    void Update()
    {      
        fov = GetComponent<FieldOfView>().ItsInFoV;
        CheckPossibleDestinyPlayer();
        if (stopped)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f && fov == false && isChasing == false)
        {           
            GoToNextPoint();            
        }
        else if (canHear == true && fov == false)
        {
            NoiseCheck();
            canLooking = true;
            GoToNoisePosition();
        }
        else if (fov == true && isChasing == false)
        {            
            Chase();
        }
        else if(isChasing == true && !attacking)
        {            
            SearchingPlayer();
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f && fov)
            if (!attacking)
                AttackPlayer();
    }

    void GoToNextPoint()
    {
        agent.destination = wayPoints[currentWP].transform.position;
        
        if (Vector3.Distance(transform.position, wayPoints[currentWP].transform.position) <= 0.5f)
        {
            if (!arrivedWaypoint)
            {
                arrivedWaypoint = true;
                anim.SetBool("Looking", true);
                StartCoroutine(WaitToNextWayPoint());
            }
        }
    }

    void Chase()
    {
        if (fov == true && !attacking)
        {
            anim.SetBool("Looking", false);
            agent.SetDestination(player.transform.position);
            isChasing = true;            
        }
    }

    void NoiseCheck()
    {
        if (canHear)
        {
            noisePosition = glassTrigger.GetComponent<GlassTrigger>().npcDestiny.transform.position;            
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

        if (Vector3.Distance(transform.position, noisePosition) <= 1f && canLooking == true)
        {
            anim.SetBool("Looking", true);
            StartCoroutine(WaitTimeLooking());
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
        anim.SetBool("StabPlayer", true);
        agent.speed = 0;
        StartCoroutine(WaitTimeAttack());
        StartCoroutine(WaitAttackAgain());
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

    IEnumerator WaitTimeLooking()
    {
        npcSource.Stop();
        yield return new WaitForSeconds(5);

        npcSource.Play();

        anim.SetBool("Looking", false);
        canLooking = false;
        canHear = false;
    }

    IEnumerator WaitTimeAttack()
    {
        npcSource.Stop();
        yield return new WaitForSeconds(0.90f);
        player.GetComponent<Player>().DamagePlayer();
    }

    IEnumerator WaitAttackAgain()
    {
        yield return new WaitForSeconds(2.10f);
        npcSource.Play();
        agent.speed = 1.8f;

        attacking = false;
        anim.SetBool("StabPlayer", false);
        agent.isStopped = false;
    }

    IEnumerator WaitToNextWayPoint()
    {
        npcSource.Stop();
        yield return new WaitForSeconds(Random.Range(5,10));

        npcSource.Play();
        anim.SetBool("Looking", false);
        currentWP++;
         
        if (currentWP >= wayPoints.Length)
        {
            currentWP = 0;
        }
        arrivedWaypoint = false;
    }

    public void Stop()
    {
        npcSource.Stop();
        anim.SetBool("Looking", true);
        agent.isStopped = true;
        stopped = true;
    }

    public void Resume()
    {
        npcSource.Play();
        anim.SetBool("Looking", false);
        agent.isStopped = false;
        stopped = false;
        agent.SetDestination(wayPoints[currentWP].transform.position);
    }

    public bool IsStopped()
    {
        return agent.isStopped;
    }

    public void CheckPossibleDestinyPlayer()
    {      
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(player.transform.position, path);                   
        Debug.Log(path.status);        
    }  
}
