using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int Score { get; private set; }
    
    private List<int> highScores = new List<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadScores();
    }

    public void AddScore(int amount)
    {
        Score = amount;
        UpdateHighScores(Score);
    }

    private void UpdateHighScores(int newScore)
    {
        highScores.Add(newScore);
        highScores = highScores.OrderByDescending(score => score).Take(10).ToList();
        SaveScores();
    }

    private void SaveScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        highScores.Clear();
        for (int i = 0; i < 10; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("HighScore" + i, 0));
        }
        Score = 0; // Reset current score, or set it as needed
    }

    public List<int> GetHighScores()
    {
        return highScores;
    }
}