using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour {

    private int layerMask;
    private bool kinematicObject;
    private GameObject objectReached;
    private float distance;
    private Camera playerCamera;
    // Use this for initialization
    void Start () {
        objectReached = null;
        layerMask = LayerMask.GetMask("Hit");
        playerCamera = GameObject.FindObjectOfType<Camera>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (objectReached != null)
            MoveObject();
    }

    public void GrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            objectReached = hit.collider.gameObject;
            distance = 0.8f*(playerCamera.transform.position - objectReached.transform.position).magnitude;
            objectReached.GetComponent<Rigidbody>().useGravity = false;
            kinematicObject = objectReached.GetComponent<Rigidbody>().isKinematic;
            if (kinematicObject)
                objectReached.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void ReleaseObject()
    {
        objectReached.GetComponent<Rigidbody>().useGravity = true;
        if (kinematicObject)
            objectReached.GetComponent<Rigidbody>().isKinematic = true;
        objectReached = null;
    }


    private void MoveObject()
    {
        if (objectReached != null)
        {
            objectReached.GetComponent<Rigidbody>().MovePosition(playerCamera.transform.position + distance * playerCamera.transform.TransformDirection(Vector3.forward));
        }
    }
}
