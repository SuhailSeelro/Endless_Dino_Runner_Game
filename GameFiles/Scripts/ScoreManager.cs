using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Assign your TextMeshPro object here in the Inspector
    //public TMP_Text highScoreText; // Optional: Assign a TextMeshPro object to display the high score

    private float score; // Current score
    //private float highScore; // High score
    private bool isGameOver = false; // To stop scoring when the game is over

    void Start()
    {
        // Initialize score and load high score from PlayerPrefs
        score = 0f;
        //highScore = PlayerPrefs.GetFloat("HighScore", 0f);

        // Update high score display
        //UpdateHighScoreText();
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Increment score based on time
            score += Time.deltaTime * 5; // Adjust multiplier for desired scoring speed

            // Update the score display
           // UpdateScoreText();
        }
    }

    // public void GameOver()
    // {
    //     isGameOver = true;

    //     // Check if the current score is higher than the saved high score
    //     // if (score > highScore)
    //     // {
    //     //     highScore = score;
    //     //     PlayerPrefs.SetFloat("HighScore", highScore);
    //     //     PlayerPrefs.Save();
    //     // }
    // }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    // private void UpdateHighScoreText()
    // {
    //     highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore).ToString();
    // }
}
