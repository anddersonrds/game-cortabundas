using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public GameObject player;
    public bool canClimb = false;
    public float speed = 1;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {           
            player.gameObject.GetComponent<Player>().enabled = false;            
            canClimb = true;              
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            player.gameObject.GetComponent<Player>().enabled = true;
            canClimb = false;
        }
    }

    void Update ()
    {
        if (canClimb)
        {
            player.GetComponent<Rigidbody>().useGravity = false;

            if (Input.GetKey(KeyCode.W))
            {               
                player.transform.Translate ( new Vector3 (0, 1, 0) * Time.deltaTime * speed);               
            }

            if (Input.GetKey(KeyCode.S))
            {
                player.transform.Translate (new Vector3 (0,-1, 0) * Time.deltaTime * speed);
            }
        }

        else
        {
            player.GetComponent<Rigidbody>().useGravity = true;
        }
		
	}
}
