using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public GameObject player;
    public bool canClimb = false;
    public float speed = 3;
   
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.isKinematic = true;
            player.gameObject.GetComponent<Player>().enabled = false;            
            canClimb = true;   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            other.attachedRigidbody.isKinematic = false;
            other.attachedRigidbody.useGravity = true;
            player.gameObject.GetComponent<Player>().enabled = true;
            canClimb = false;
        }
    }

    private void FixedUpdate()
    {
        if (canClimb)
        {           

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
            player.GetComponent<Collider>().attachedRigidbody.useGravity = true;
        }
		
	}
}
