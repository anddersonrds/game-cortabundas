using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour {

    private bool grabbing;
    private bool onReach;
    private GameObject objectReached;
    private float distance;
    private UnityEngine.UI.RawImage handIcon;
    // Use this for initialization
    void Start () {
        objectReached = null;
        Canvas canvas = FindObjectOfType<Canvas>();
        handIcon = canvas.GetComponentInChildren<UnityEngine.UI.RawImage>();
    }
	
	// Update is called once per frame
	void Update () {
        if (onReach)
        {
            if (Input.GetKeyDown("e"))
            {
                if (grabbing)
                    ReleaseObject();
                else
                    GrabObject();
            }
        }

        if (objectReached != null)
            MoveObject();
    }
    
    private void GrabObject()
    {
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Obstacles");
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            objectReached = hit.collider.gameObject;
            distance = 0.9f*(transform.position - objectReached.transform.position).magnitude;
            objectReached.GetComponent<Rigidbody>().useGravity = false;
            objectReached.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void ReleaseObject()
    {
        objectReached.GetComponent<Rigidbody>().useGravity = true;
        objectReached.GetComponent<Rigidbody>().isKinematic = true;
        objectReached = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            onReach = true;
            handIcon.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            handIcon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            ReleaseObject();
        }
    }

    private void MoveObject()
    {
        if (objectReached != null)
        {
            objectReached.GetComponent<Rigidbody>().MovePosition(transform.position + distance * transform.TransformDirection(Vector3.forward));
            //objectReached.transform.position = transform.position + distance * transform.TransformDirection(Vector3.forward);
        }
    }
}
