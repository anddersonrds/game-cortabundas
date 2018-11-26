using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : MonoBehaviour {

    public GameObject player;
    public float speedRun;
    	
	void Update () {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Player>().speed = speedRun;
        }
        else
        {
            GetComponent<Player>().speed = 2f;
        }
    }
}
