using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseObject;
    
    public PlayerBehavior playerBehavior;
    public LineDrawer lineDrawer;
    public SpawnManager spawnManager;
	
	public GameObject musicButton;
    public GameObject effectButton;
    public GameObject vibrationButton;

    public Sprite soundImage;
    public Sprite soundImageOff;
    public Sprite effectImage;
    public Sprite effectImageOff;
    public Sprite vibrationImage;
    public Sprite vibrationImageOff;

    public TextMeshProUGUI score;
	public CloudSpawner cloudSpawner;
	public GameScore gameScore;
	
    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            playerBehavior.rb.simulated = false;
            lineDrawer.canDraw = false;
            spawnManager.PauseSpawning();
            pauseObject.SetActive(true);
			cloudSpawner.PauseSpawning();
			cloudSpawner.PauseAllClouds();
			score.text = gameScore.score.ToString();
			
            if (SettingsManager.Instance.IsMusicEnabled())
            {
				musicButton.GetComponent<Image>().sprite = soundImage;
            }
            else
            {
				musicButton.GetComponent<Image>().sprite = soundImageOff;
            }

            if (SettingsManager.Instance.AreSoundEffectsEnabled())
            {
				effectButton.GetComponent<Image>().sprite = effectImage;
            }
            else
            {
				effectButton.GetComponent<Image>().sprite = effectImageOff;
            }

            if (SettingsManager.Instance.IsVibrationEnabled())
            {
                vibrationButton.GetComponent<Image>().sprite = vibrationImage;
            }
            else
            {
                vibrationButton.GetComponent<Image>().sprite = vibrationImageOff;
            }

        }
        else
        {
			cloudSpawner.ResumeSpawning();
			cloudSpawner.ResumeAllClouds();
            playerBehavior.rb.simulated = true;
            lineDrawer.canDraw = true;
            spawnManager.ResumeSpawning();
            pauseObject.SetActive(false);
        }
    }
	
	public void SoundClick()
    {
        if(SettingsManager.Instance.IsMusicEnabled())
        {
            musicButton.GetComponent<Image>().sprite = soundImageOff;
            SettingsManager.Instance.ToggleMusic(false);
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = soundImage;
            SettingsManager.Instance.ToggleMusic(true);
        }
    }

    public void EffectClick()
    {
        if (SettingsManager.Instance.AreSoundEffectsEnabled())
        {
            effectButton.GetComponent<Image>().sprite = effectImageOff;
            SettingsManager.Instance.ToggleSoundEffects(false);
        }
        else
        {
            effectButton.GetComponent<Image>().sprite = effectImage;
            SettingsManager.Instance.ToggleSoundEffects(true);
        }
    }

    public void VibrationClick()
    {
        if (SettingsManager.Instance.IsVibrationEnabled())
        {
            vibrationButton.GetComponent<Image>().sprite = vibrationImageOff;
            SettingsManager.Instance.ToggleVibration(false);
        }
        else
        {
            vibrationButton.GetComponent<Image>().sprite = vibrationImage;
            SettingsManager.Instance.ToggleVibration(true);
        }
    }
}