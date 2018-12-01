using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public GameObject player;
    public bool canClimb = false;
    public float speed = 3;
    private Collider coll;


    private void Start()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            other.attachedRigidbody.isKinematic = true;            
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
            if (Input.GetKey(KeyCode.W))
            {               
                player.transform.Translate (Vector3.up * speed * Time.deltaTime);               
            }

            if (Input.GetKey(KeyCode.S))
            {
                player.transform.Translate (Vector3.down * Time.deltaTime * speed);
            }
        }		
	}
}
