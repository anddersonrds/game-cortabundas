using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private GameMaster gm;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("setting checkpoint cena 2");
            gm.lastCheckPointPos = transform.position;
            gm.hasFlashlight = other.GetComponentInChildren<Light>().enabled;
        }
    }
}
