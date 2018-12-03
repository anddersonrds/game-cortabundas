using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private GameMaster gm;
    public GameObject cutSceneOne;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            cutSceneOne.active = false;
            gm.lastCheckPointPos = transform.position;
            gm.hasFlashlight = other.GetComponentInChildren<Light>().enabled;
        }
    }
}
