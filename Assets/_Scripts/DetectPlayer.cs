using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    private bool playerInside;
    private bool warned;
    private Color color;
    private Color transparentColor;
    private MeshRenderer renderer;
	// Use this for initialization
	void Start () {
        playerInside = true;
        warned = false;
        renderer = GetComponent<MeshRenderer>();
        color = renderer.materials[0].color;
        color.a = 0.5f;
        transparentColor = new Color(color.r, color.g, color.b, 0.0f);
        renderer.materials[0].color = transparentColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            renderer.materials[0].color = transparentColor;
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (warned)
                renderer.materials[0].color = color;
            playerInside = false;
        }
    }

    public void Warn()
    {
        warned = true;
        renderer.materials[0].color = color;
    }

    public void ClearWarning()
    {
        renderer.materials[0].color = transparentColor;
    }

    public bool IsPlayerInside()
    {
        return playerInside;
    }
}
