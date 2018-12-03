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
    private Transform playerPos,RagReset;
    private Animator anim;
    private AudioSource source;
    

    // Use this for initialization
    void Start () {
               
        source = GetComponent<AudioSource>();


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
    public void EndGame()
    {
        anim = GameObject.Find("LevelFade").GetComponent<Animator>();
        anim.SetTrigger("FadeOut");
        source.Play();

        StartCoroutine(WaitToCredits());
    }

    IEnumerator WaitToCredits()
    {
        yield return new WaitForSeconds(5.45f);

        source.Stop();
        SceneManager.LoadScene(2);
    }
    

}


