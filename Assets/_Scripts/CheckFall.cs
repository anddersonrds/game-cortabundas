using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            playerScript.ReloadCheckpoint();
        }
    }
}
