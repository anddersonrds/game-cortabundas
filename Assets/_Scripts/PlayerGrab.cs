using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour {

    private int layerMask;
    private bool grabbing;
    private bool onReach;
    private bool kinematicObject;
    private GameObject objectReached;
    private float distance;
    private UnityEngine.UI.RawImage handIcon;
    private Camera camera;
    // Use this for initialization
    void Start () {
        grabbing = false;
        objectReached = null;
        GameObject handObject = GameObject.Find("HandIcon");
        handIcon = handObject.GetComponent<UnityEngine.UI.RawImage>();
        layerMask = LayerMask.GetMask("Obstacles");
        camera = GameObject.FindObjectOfType<Camera>();
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
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            grabbing = true;
            objectReached = hit.collider.gameObject;
            distance = 0.8f*(camera.transform.position - objectReached.transform.position).magnitude;
            objectReached.GetComponent<Rigidbody>().useGravity = false;
            kinematicObject = objectReached.GetComponent<Rigidbody>().isKinematic;
            if (kinematicObject)
                objectReached.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void ReleaseObject()
    {
        grabbing = false;
        objectReached.GetComponent<Rigidbody>().useGravity = true;
        if (kinematicObject)
            objectReached.GetComponent<Rigidbody>().isKinematic = true;
        objectReached = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject.name == other.name)
                {
                    onReach = true;
                    handIcon.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject.name == other.name)
                {
                    onReach = true;
                    handIcon.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            onReach = false;
            handIcon.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            if (objectReached != null)
                ReleaseObject();
        }
    }

    private void MoveObject()
    {
        if (objectReached != null)
        {
            objectReached.GetComponent<Rigidbody>().MovePosition(camera.transform.position + distance * camera.transform.TransformDirection(Vector3.forward));
            //objectReached.transform.position = transform.position + distance * transform.TransformDirection(Vector3.forward);
        }
    }
}
