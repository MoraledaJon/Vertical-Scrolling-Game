using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    private const string MusicPrefKey = "MusicEnabled";
    private const string SoundEffectsPrefKey = "SoundEffectsEnabled";

    // Add private variables to hold the current state
    private bool isMusicEnabled;
    private bool areSoundEffectsEnabled;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMusic(bool isOn)
    {
        isMusicEnabled = isOn;
        SoundManager.Instance.SetMusicEnabled(isOn);
        SavePreference(MusicPrefKey, isOn);
    }

    public void ToggleSoundEffects(bool isOn)
    {
        areSoundEffectsEnabled = isOn;
        SoundManager.Instance.SetSoundEffectsEnabled(isOn);
        SavePreference(SoundEffectsPrefKey, isOn);
    }

    // Public getter methods
    public bool IsMusicEnabled()
    {
        return isMusicEnabled;
    }

    public bool AreSoundEffectsEnabled()
    {
        return areSoundEffectsEnabled;
    }

    private void SavePreference(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        // Add null checks
        if (SoundManager.Instance == null)
        {
            Debug.LogError("SoundManager instance is not set. Make sure SoundManager is initialized before SettingsManager.");
            return;
        }

        isMusicEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;
        areSoundEffectsEnabled = PlayerPrefs.GetInt(SoundEffectsPrefKey, 1) == 1;

        SoundManager.Instance.SetMusicEnabled(isMusicEnabled);
        SoundManager.Instance.SetSoundEffectsEnabled(areSoundEffectsEnabled);
    }
}