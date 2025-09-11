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
        if (GameManager.gameManager != null)
            gameManager = GameManager.gameManager;
        else
            Debug.LogError("GameManager is not found in SceneManager");

        if (AudioManager.audioManager != null)
            audioManager = AudioManager.audioManager;
        else
            Debug.LogError("AudioManager is not found in SceneManager");
        

    }

    public void play()
    {
        SceneManager.LoadScene(game);

        if(VolumeSettings.instance != null)
            VolumeSettings.instance.LoadVolume();
        else
            Debug.LogError("VolumeSettings instance is not found in SceneManager");
        
        Time.timeScale = 1f;
        
        if (gameManager != null)
        {
            gameManager.ResetScore();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainmenu);
        if (gameManager != null)
        {
            Time.timeScale = 1f;
            gameManager.SetGameState(GameState.MainMenu);
        }
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(titleScreen);
        if(gameManager != null)
        {
            Time.timeScale = 1f;
            gameManager.SetGameState(GameState.MainMenu);
        }
    }

    public void Audio()
    {
        SceneManager.LoadScene(gameAudio);
       
    }

    public void Quit()
    {
        if(VolumeSettings.instance != null)
            VolumeSettings.instance.LoadVolume();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

   

    

    
}
