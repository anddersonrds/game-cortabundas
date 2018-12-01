using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCutScene : MonoBehaviour
{
    public GameObject CutSceneCam;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CutSceneCam.SetActive(true);            
        }
    }

}
