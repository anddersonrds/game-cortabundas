using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu,optionsMenu;



    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
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



}
