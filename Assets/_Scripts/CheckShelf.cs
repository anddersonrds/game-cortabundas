using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckShelf : MonoBehaviour {

	private BoxCollider box;
	private MeshCollider mesh;
	private bool changedProperties = false;
	private List<Collider> collisions = new List<Collider>();
	// Use this for initialization
	void Start () {
		box = GetComponent<BoxCollider>();
		mesh = GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		int numberObjects = CheckObjectsInShelf();

		if (numberObjects == 0 && !changedProperties)
		{
			changedProperties = true;
			box.enabled = true;
			this.tag = "Grabbable";
			mesh.convex = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		collisions.Add(other);
	}

	void OnTriggerExit(Collider other)
	{
		collisions.Remove(other);
	}

	int CheckObjectsInShelf()
	{
		return collisions.Count;
	}
}
