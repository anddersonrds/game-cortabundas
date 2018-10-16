using System.Collections;
using UnityEngine;

public class NpcAi : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public bool fov;
    public Vector3 LastPosPlayer;
    public UnityEngine.AI.NavMeshAgent agent;
    public int counter = 0;

    public GameObject GetPlayer()
    {
        return player;
    }

    void Start()
    {
        anim = GetComponent<Animator>();        
    }

    void Update()
    {
        fov = GetComponent<FieldOfView>().ItsInFoV;
        anim.SetBool("Fov", fov);              
    }

    public void StartAttack()
    {

    }

    public void StopAttack()
    {

    }

    public void Searching()
    {
        StartCoroutine(TimeSearching());
    }

    IEnumerator TimeSearching()
    {
        yield return new WaitForSeconds(5);
        LastPosPlayer = (player.transform.position);
        agent.SetDestination(LastPosPlayer);
        counter = counter +1;           

        if(counter >= 3)
        {
            StopSearching();
        }
    }

    public void StopSearching()
    {
        anim.SetTrigger("NotFound");
        counter = 0;
    }
}
