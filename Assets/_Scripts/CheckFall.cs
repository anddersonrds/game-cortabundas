using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFall : MonoBehaviour {
    public GameObject cutSceneFall;
    public Player playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cutSceneFall.active = true;            
            StartCoroutine(WaitToReload());            
        }
    }

    IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(3);
        playerScript.ReloadCheckpoint();
        cutSceneFall.active = false;
        
    }
}
