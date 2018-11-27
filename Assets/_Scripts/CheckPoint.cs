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
            gm.lastCheckPointPos = transform.position;
            gm.hasFlashlight = other.GetComponentInChildren<Light>().enabled;

            if (gameObject.name == "CheckPointCena3")
            {
                GameObject forroTrigger = GameObject.Find("ForroTrigger");
                forroTrigger.SetActive(false);
            }
        }
    }
}
