using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineEvent : MonoBehaviour
{
    public GameObject CutScenePlayer;
    
    public void DeletePlayer()
    {
        CutScenePlayer.SetActive(false);
    }
	
}
