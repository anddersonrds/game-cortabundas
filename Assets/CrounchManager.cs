using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrounchManager : MonoBehaviour
{
    public Player player;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().isChounching = true;
            player.GetComponent<Player>().CrouchControll(true);
        }
    }

}
