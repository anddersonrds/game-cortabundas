using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class GameMaster : MonoBehaviour {

    private static GameMaster instance;
    public GameObject deadCutScene,CutSceneModel,deadCam;
    public Vector3 lastCheckPointPos;
    public bool hasFlashlight;
    private UnityEngine.UI.Text text;
    private Transform playerPos,RagReset;
    // Use this for initialization
    void Start () {

        text = GameObject.Find("DialogText").GetComponent<UnityEngine.UI.Text>();        

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
	}
    
    public void GameOver()
    {        
        text.text = "Fim de Jogo";
        text.color = Color.red;
        deadCutScene.SetActive(true);        
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        CutSceneModel.transform.position = playerPos.transform.position * transform.position.y ;        
        StartCoroutine(WaitToLoad());
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(5);
        RagReset = GameObject.Find("TransformReset").GetComponent<Transform>();
        CutSceneModel.transform.position = RagReset.transform.position;
        deadCutScene.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       
    }   
}


