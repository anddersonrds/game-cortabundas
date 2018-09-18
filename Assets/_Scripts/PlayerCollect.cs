using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour {

    private bool onReach;
    private GameObject objectReached;

    // Use this for initialization
    void Start() {
        objectReached = null;

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("e") && onReach)
        {
            CollectObject();
        }
    }

    private void CollectObject()
    {
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Hit");
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
            onReach = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible"))
            onReach = false;
    }
}
