using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour {

    private bool onReach;
    private GameObject objectReached;
    // Use this for initialization
    void Start () {
        objectReached = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e") && onReach)
        {
            GrabObject();
        }

        if (objectReached != null)
            MoveObject();
    }
    
    private void GrabObject()
    {
        if (objectReached == null)
        {
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Hit");
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                objectReached = hit.collider.gameObject;
                objectReached.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        else
        {
            objectReached.GetComponent<Rigidbody>().useGravity = true;
            objectReached = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
            onReach = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
            onReach = false;
    }

    private void MoveObject()
    {
        if (objectReached != null)
            objectReached.transform.position = transform.position + 2 * transform.TransformDirection(Vector3.forward);
    }
}
