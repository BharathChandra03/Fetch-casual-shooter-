using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ScenesManager : MonoBehaviour
{
    const string mainmenu = "MainMenu";
    const string game = "Game";
    const string titleScreen = "TitleScreen";
    const string gameAudio = "Audio";

    private GameManager gameManager;
    private AudioManager audioManager;

    private void Start()
    {
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;
    }

    public void play()
    {
        SceneManager.LoadScene(game);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        gameManager.ResetScore();
        SceneManager.LoadScene(game);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainmenu);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(titleScreen);
    }

    public void Audio()
    {
        SceneManager.LoadScene(gameAudio);
    }
}
