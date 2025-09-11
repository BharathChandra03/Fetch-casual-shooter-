using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    const string mainmenu = "MainMenu";
    const string game = "Game";
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject AudioMenuUI;

    private GameManager gameManager;

    // Update is called once per frame
    private void Start()
    {
        gameManager = GameManager.gameManager;
    }

    public void Pause_Menu()
    {
        Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        AudioMenuUI.SetActive(false);
        gameManager.SetGameState(GameState.Playing);
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameManager.SetGameState(GameState.Paused);
        gameIsPaused = true;
    }

    public void Audio()
    {
        pauseMenuUI.SetActive(false);
        AudioMenuUI.SetActive(true);
    }

    public void Restart()
    {
        gameManager.SetGameState(GameState.Playing);
        Time.timeScale = 1f;
        gameManager.ResetScore();
        SceneManager.LoadScene(game);
    }

    public void Menu()
    {
        gameManager.ResetScore();
        SceneManager.LoadScene(mainmenu);
        //SceneManager.LoadScene(mainmenu);
        //gameManager.currentState = GameState.MainMenu;
      /*  if (gameManager != null)
        {
            gameManager.currentState = GameState.MainMenu;
        }
        else
        {
            Debug.LogWarning("GameManager is null in MainMenu().");
        }*/

    }
}
