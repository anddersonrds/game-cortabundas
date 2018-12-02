using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {

    public static bool gameIsPaused = false;
    public CamMouseLook camMouse;
    public GameObject pauseMenu;
    private float volumeMaster;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        camMouse.GetComponent<CamMouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        camMouse.GetComponent<CamMouseLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        gameIsPaused = true;
    }

    public void VolumeMaster(float volume)
    {
        volumeMaster = volume;
        AudioListener.volume = volumeMaster;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
