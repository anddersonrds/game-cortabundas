using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderExit : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.gameObject.GetComponent<Player>().enabled = true;
            player.gameObject.GetComponent<Rigidbody>().isKinematic = false;            
        }
    }

}
