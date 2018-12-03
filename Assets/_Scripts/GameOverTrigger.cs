using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour {

    public GameMaster gm;

    private void OnTriggerEnter(Collider other)
    {
        gm.GameOver();
    }
}
