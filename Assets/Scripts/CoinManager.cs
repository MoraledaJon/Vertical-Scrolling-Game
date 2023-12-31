using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    private int totalCoins = 0;
    private int currentGameCoins;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadTotalCoins();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        currentGameCoins += 1;
    }

    public void ResetCoin()
    {
        currentGameCoins = 0;
    }

    public void RemoveCoin(int value)
    {
        totalCoins -= value;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    public void SaveGame()
    {
        totalCoins += currentGameCoins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
        currentGameCoins = 0;
    }

    private void LoadTotalCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    public int GetCurrentGameCoins()
    {
        return currentGameCoins;
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }

}