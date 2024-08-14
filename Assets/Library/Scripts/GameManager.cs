using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    
    public TextMeshProUGUI scoreText;
    public static int score = 0;
    
    public int health = 3;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;

    public PlayerContoller player;

    private int currentScore = score;

    const string gameOver = "GameOver";

    
    private void Awake()
    {
        gameManager = this;

    }

    public static GameManager Instance
    {
        get
        {
            return gameManager;
        }
    }


    private void Start()
    {
        UpdateScoreText();
       
    }

    private void HeartSystem()
    {
        numOfHearts = health;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
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


    public void UpdateScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
     
    public void loseHealth()
    {
        health--;
        HeartSystem();
        PlayerDead();
    }
    
    public void IncreaseHealth()
    {
        if (health < 3)
        {
           health++;
           HeartSystem();
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    
    public void PlayerDead()
    {
        if (health == 0)
        {
            SceneManager.LoadScene(gameOver);
            scoreText.text = "Score : " + currentScore.ToString();
        }
    }
}
