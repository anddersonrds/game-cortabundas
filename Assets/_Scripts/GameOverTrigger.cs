using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour {

    public GameObject gm;

    private void OnTriggerEnter(Collider other)
    {
        gm.gameObject.GetComponent<GameMaster>().GameOver();
    }   
}
