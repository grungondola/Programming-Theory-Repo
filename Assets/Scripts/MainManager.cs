using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ScoreDisplay;
    [SerializeField]
    private TMP_Text NameDisplay;
    [SerializeField]
    private TMP_Text HighScoreDisplay;

    private float scorePoints = 0.0f;
    public bool IsGameOver { get; private set; } = false;
    public GameObject gameOverText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance != null)
        {
            NameDisplay.text = $"Player: {GameManager.Instance.PlayerName}";
            UpdateHighScoreDisplay(GameManager.Instance.HighScorePlayerName, GameManager.Instance.HighScorePoints);
        }
    }

    public void UpdateScore(float score)
    {
        scorePoints = score;
        UpdateScoreDisplay();
    }

    public void UpdateHighScoreDisplay(float score)
    {
        UpdateHighScoreDisplay(GameManager.Instance.PlayerName, score);
    }

    public void GameOver()
    {
        IsGameOver = true;
        gameOverText.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateScoreDisplay()
    {
        var score = (float)Math.Round(scorePoints, 1);
        ScoreDisplay.text = $"Score: {score:0.0}";
    }

    private void UpdateHighScoreDisplay(string name, float score)
    {
        score = (float)Math.Round(score, 1);
        HighScoreDisplay.text = $"High Score: {name}: {score:0.0}";
    }
}
