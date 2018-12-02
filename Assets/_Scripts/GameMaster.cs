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
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver()
    {
        text.text = "Fim de Jogo";
        text.color = Color.red;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
