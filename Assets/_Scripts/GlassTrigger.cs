using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTrigger : MonoBehaviour
{    
    public Transform npcDestiny;
    private AudioSource audio;
    [SerializeField]
    private AudioClip fxGlass;
    public NpcAi npc;


	
	void Start ()
    {
        audio = GetComponent<AudioSource>();		
	}   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            audio.PlayOneShot(fxGlass);
            npc.GetComponent<NpcAi>().canHear = true;
        }
    }
}
