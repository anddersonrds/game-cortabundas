using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBaseFsM : StateMachineBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject NPC,player;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;
    public float chaseSpeed = 5.0f;
    public float rotChaseSpeed = 20.0f;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        player = NPC.GetComponent<NpcAi>().GetPlayer();
        agent = NPC.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
}
