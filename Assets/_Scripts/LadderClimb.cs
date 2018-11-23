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
            Debug.Log(other.attachedRigidbody.useGravity);
            player.gameObject.GetComponent<Player>().enabled = false;            
            canClimb = true;              
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            other.attachedRigidbody.useGravity = true;
            other.attachedRigidbody.isKinematic = false;
            player.gameObject.GetComponent<Player>().enabled = true;
            canClimb = false;
        }
    }

    void Update ()
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
