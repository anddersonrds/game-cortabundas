using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutsceneTrigger : MonoBehaviour {

    public GameObject cutSceneTimeLine;
    public GameObject gameOverTrigger;

    private void OnTriggerEnter(Collider other)
    {
        cutSceneTimeLine.SetActive(true);
        gameOverTrigger.SetActive(true);
    }
}
