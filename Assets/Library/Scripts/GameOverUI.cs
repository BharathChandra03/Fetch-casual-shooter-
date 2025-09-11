using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject HighScoresPanel;  // Panel to display high scores

    private bool isVisible = false;

    private GameManager gameManager;


    private void Awake()
    {
        
    }
    void Start()
    {
        gameManager = GameManager.gameManager; // Reference to the GameManager instance

        ShowFinalScore(); // Display the final score
        //ShowLeaderBoardScores(); // Display the leaderboard scores

    }

    public void ShowFinalScore()
    {
        if (gameManager != null)
        {
            int lastScore = PlayerPrefs.GetInt("LastScore", 0); // Default to 0 if not found
            finalScoreText.text = "Your Score :" + " " + lastScore; // Display the last score
        }
            
    }

    public void ShowHighScores()
    {
        if (gameManager != null)
        {
            var scores = gameManager.GetTopScores(); // Get the top scores from GameManager
            StringBuilder sb = new StringBuilder("High Scores :\n"); // Initialize the leaderboard text

            for (int i = 0; i < scores.Count; i++) // Iterate through the top scores
            {
                sb.AppendLine(i + 1 + " " + "." + " " + scores[i]); // Append each score to the leaderboard text
            }
            highScoreText.text = sb.ToString(); // Set the leaderboard text
        }
    }

    public void ToggleHighScores()
    {
        isVisible = !isVisible; // Toggle the visibility state
        HighScoresPanel.SetActive(isVisible); // Show or hide the high scores panel based on the visibility state
        
        if(isVisible)
        {
            ShowHighScores(); // Update and show high scores when the panel is made visible
        }
    }

    public void CloseHighScoresPanel()
    {
        isVisible = false; // Set visibility to false
        HighScoresPanel.SetActive(false); // Hide the high scores panel
    }
}
