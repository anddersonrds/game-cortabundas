using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class GameMaster : MonoBehaviour {

    private static GameMaster instance;
    public Vector3 lastCheckPointPos;
    public bool hasFlashlight;
    private UnityEngine.UI.Text text;
    public Animator anim;
    private AudioSource source;
    

    // Use this for initialization
    void Start () {

        text = GameObject.Find("DialogText").GetComponent<UnityEngine.UI.Text>();
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
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver()
    {
        text.text = "Fim de Jogo";
        text.color = Color.red;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void EndGame()
    {
        anim.SetTrigger("FadeOut");
        source.Play();

        StartCoroutine(WaitToCredits());
    }

    IEnumerator WaitToCredits()
    {
        yield return new WaitForSeconds(5.0f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
