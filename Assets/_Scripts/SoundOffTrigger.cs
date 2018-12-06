using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOffTrigger : MonoBehaviour {

    public GameObject musicalBox;
    public GameObject finalTrigger;

    private void OnTriggerEnter(Collider other)
    {
        finalTrigger.SetActive(true);
        musicalBox.GetComponent<AudioSource>().Stop();
    }
}
