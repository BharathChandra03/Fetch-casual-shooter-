using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Android;


public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    Gameover
}
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int CurrentScore { get; private set; }

    public GameState currentState;

    public TextMeshProUGUI scoreText;
    //public static int score = 0;
    
    public int health = 3;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite Heart;

    public PlayerContoller player;

    //private int currentScore = score;

    const string gameOver = "GameOver";

    private List<int> topScores = new List<int>();
    private const int maxHighScores = 5;

    private void Awake()
    {
        if(gameManager == null) // Singleton pattern to ensure only one instance of GameManager exists
        {
            gameManager = this; // Set the static instance to this instance
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed when loading a new scene
            LoadScore();
        }
        else
        {
            Destroy(gameObject); // Destroy this instance if another instance already exists
        }
    }
    private void Start()
    {
        UpdateScoreText();
        HeartSystem();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            var scoreObj = GameObject.Find("ScoreText");
            if(scoreObj != null)
            {
                scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
                UpdateScoreText();
            }
            else
            {
                Debug.LogWarning("ScoreText not found in scene" + scene.name);
            }
        }
        
    }
    public void ResetScore()
    {
        CurrentScore = 0;
    }
    public void UpdateScore(int value)
    {
        CurrentScore += value;
        UpdateScoreText();
    }
    public void UpdateScoreText()
    {
        scoreText.text = CurrentScore.ToString();
    }

    public void SaveFinalScore() // Save the final score to the top scores list
    {
        PlayerPrefs.SetInt("LastScore", CurrentScore); // Save the last score for display on the game over screen

        topScores.Add(CurrentScore); // Add the new score to the list
        topScores.Sort((a, b) => b.CompareTo(a)); // Sort the list in descending order

        if (topScores.Count > maxHighScores) // If the list exceeds the maximum number of high scores
        {
            topScores.RemoveAt(topScores.Count - 1); // Remove the lowest score
        }

        //PlayerPrefs.Save(); // Ensure the data is written to disk
        SaveScores(); // Save the updated top scores to PlayerPrefs
    }

    private void SaveScores() // Save the top scores to PlayerPrefs
    {
        for (int i = 0; i < topScores.Count; i++) // Iterate through the top scores list
        {
            PlayerPrefs.SetInt("Score" + i, topScores[i]); // Save each score with a unique key
        }

        PlayerPrefs.SetInt("ScoreCount", topScores.Count); // Save the count of scores
        PlayerPrefs.Save(); // Ensure the data is written to disk
    }

    private void LoadScore()
    {
        topScores.Clear(); // Clear the current list of top scores

        int count = PlayerPrefs.GetInt("ScoreCount", 0); // Get the count of saved scores

        for (int i = 0; i < count; i++) // Iterate through the saved scores
        {
            topScores.Add(PlayerPrefs.GetInt("Score" + i)); // Load each score and add it to the list
        }
    }

    public List<int> GetTopScores() // Get a copy of the top scores list
    {
        return new List<int>(topScores); // Return a new list containing the top scores
    }

    private void HeartSystem()
    {
        numOfHearts = health;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = Heart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }
    }
    public void loseHealth()
    {
        health--;
        HeartSystem();
        PlayerDead();
    }
    public void IncreaseHealth()
    {
        if (health < numOfHearts)
        {
           health++;
           HeartSystem();
        }
    }

    public void PlayerDead()
    {
        if (health == 0)
        {
            SaveFinalScore();
            SetGameState(GameState.Gameover);
            SceneManager.LoadScene(gameOver);
        }
    }
    public void SetGameState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.Gameover:
                Time.timeScale = 0f;
                break;
        }

    }
}
