using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAi : MonoBehaviour
{
    public GameObject player;
    public Animator anim;


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
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
    }

    public void StartAttack()
    {

    }

    public void StopAttack()
    {

    }


}
