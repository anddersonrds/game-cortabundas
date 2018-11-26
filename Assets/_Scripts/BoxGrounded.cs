using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrounded : MonoBehaviour {

    private BoxCollider boxCollider;
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsGrounded())
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
	}

    private bool IsGrounded()
    {
        if (Physics.Raycast(boxCollider.bounds.center, Vector3.down, boxCollider.size.y / 2 - 0.05f))
            return true;

        return false;
    }
}
