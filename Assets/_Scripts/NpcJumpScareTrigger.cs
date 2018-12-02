using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcJumpScareTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        other.gameObject.SetActive(false);
    }
}
