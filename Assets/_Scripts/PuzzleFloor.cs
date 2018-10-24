using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFloor : MonoBehaviour
{
    public GameObject crakedFloor;

      void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(crakedFloor, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }   
}
