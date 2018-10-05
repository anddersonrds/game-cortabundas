using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NpcBaseFsM
{ 
    GameObject[] wayPoints;
    int currentWP;

    void Awake()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoints");
    }


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWP = 0;	
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wayPoints.Length == 0) return;

        if (Vector3.Distance(wayPoints[currentWP].transform.position, NPC.transform.position) < accuracy)
        {
            currentWP++;
            if (currentWP >= wayPoints.Length)
            {
                currentWP = 0;
            }
        }

        agent.SetDestination(wayPoints[currentWP].transform.position);
	}
	
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	
	}
	
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	
	}
	
	override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	
	}
}
