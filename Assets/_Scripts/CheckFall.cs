using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFall : MonoBehaviour {
    public GameObject cutSceneFall;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cutSceneFall.active = true;
            Player playerScript = other.GetComponent<Player>();
            //playerScript.ReloadCheckpoint();
        }
    }
}
