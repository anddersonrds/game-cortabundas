using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {

    public static bool gameIsPaused = false;
    public CamMouseLook camMouse;
    public GameObject pauseMenu;
    private static float volumeMaster = 10.0f;

    private void Start()
    {
        AudioListener.volume = volumeMaster;
    }

    private void Update()
    {
        Debug.Log("volume: " + AudioListener.volume);
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
        Cursor.visible = false;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        UnityEngine.UI.Slider volumeSlider = GameObject.Find("VolumeSlider").GetComponent<UnityEngine.UI.Slider>();
        if (volumeSlider != null)
        {
            volumeSlider.value = volumeMaster;
        }
        Cursor.visible = true;
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
