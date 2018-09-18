using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour {

    private bool onReach;
    private GameObject objectReached;

	// Use this for initialization
	void Start () {
        objectReached = null;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e") && onReach)
        {
            Climb();
        }
	}

    private void Climb()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
            onReach = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
            onReach = false;
    }
}
