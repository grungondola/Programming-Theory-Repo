using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text highScoreDisplay;

    public void Awake()
    {
        var highScore = Math.Round(GameManager.Instance.HighScorePoints, 1);
        highScoreDisplay.text = $"High Score: {GameManager.Instance.HighScorePlayerName}: {highScore:0.0}";
    }

    public void StartNew()
    {
        GameManager.Instance.PlayerName = nameInput.text;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        GameManager.Instance.SaveHighScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
