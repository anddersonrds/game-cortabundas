using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu,optionsMenu,credits;
    public Animator anim;



    public void Play()
    {
        anim.SetTrigger("FadeOut");        
    }

    public void Exit()
    {
        Application.Quit();

        Debug.Log("Saiu");
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);

        optionsMenu.SetActive(true);
    }

    public void BackMenu()
    {
        optionsMenu.SetActive(false);

        mainMenu.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
