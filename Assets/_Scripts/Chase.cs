using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NpcBaseFsM {

	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
	}
	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        agent.SetDestination(player.transform.position);               
	}
	
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC.GetComponent<NpcAi>().fov = false;

    }
	
}
