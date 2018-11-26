using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("fall");
        if (other.CompareTag("Player"))
        {
            Debug.Log("passou pelo trigger");
            Player playerScript = other.GetComponent<Player>();
            playerScript.ReloadCheckpoint();
        }
    }
}
