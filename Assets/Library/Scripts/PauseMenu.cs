using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject AudioMenuUI;
    

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Pause_Menu()
    {
        Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        AudioMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Audio()
    {
        pauseMenuUI.SetActive(false);
        AudioMenuUI.SetActive(true);
    }
}
