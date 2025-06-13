using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string PlayerName;
    public string HighScorePlayerName;
    public float HighScorePoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadHighScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHighScore(string playerName, float highScore)
    {
        HighScorePlayerName = playerName;
        HighScorePoints = highScore;
    }

    public void SaveHighScore()
    {
        var filename = GetSaveFileName();

        var saveData = new GameSaveData
        {
            PlayerName = HighScorePlayerName,
            Points = HighScorePoints
        };

        var json = JsonUtility.ToJson(saveData);

        File.WriteAllText(filename, json);
    }

    public void LoadHighScore()
    {
        var filename = GetSaveFileName();
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            var highScore = JsonUtility.FromJson<GameSaveData>(json);

            HighScorePlayerName = highScore.PlayerName;
            HighScorePoints = highScore.Points;
        }
    }

    private string GetSaveFileName()
    {
        return $"{Application.persistentDataPath}/savefile.json";
    }

    [Serializable]
    public class GameSaveData
    {
        public string PlayerName;
        public float Points;
    }
}
