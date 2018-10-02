using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour
{

    GameObject NPC;
    GameObject[] wayPoints;
    int currentWP;

    void Awake()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoints");
    }


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        currentWP = 0;
	
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wayPoints.Length == 0) return;

        if (Vector3.Distance(wayPoints[currentWP].transform.position, NPC.transform.position) < 3.0f)
        {
            currentWP++;
            if (currentWP >= wayPoints.Length)
            {
                currentWP = 0;
            }
        }

        var direction = wayPoints[currentWP].transform.position - NPC.transform.position;

        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), 1.0f * Time.deltaTime);

        NPC.transform.Translate(0, 0, Time.deltaTime * 1.5f);

	
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
