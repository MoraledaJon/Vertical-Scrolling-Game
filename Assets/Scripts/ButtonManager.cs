using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ButtonManager : MonoBehaviour
{
	public GameObject circleContents;
    public GameObject flagContents;
	public GameObject planetContents;

    public ScrollRect scrollRect;
    
    public Sprite currentlySelectedSprite;
    public Sprite notSelectedSprite;

    public Image defaultButton;
    public TextMeshProUGUI category;

    public Button playButton;
    public Button skinButton;
    public Button shopButton;
    public Button moreGamesButton;
    public Button rateUsButton;

    public Vector3 increaseSize = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 normalSize = new Vector3(0.9f, 0.9f, 0.9f);

    public GameObject mainPanel;
    public GameObject skinPanel;
    public GameObject shopPanel;
    public GameObject moreGamesPanel;
    
    public GameObject ScoreRankingPanel;
    private bool isScorePanelOpen = false;
    public TextMeshProUGUI rank1;
    public TextMeshProUGUI rank2;
    public TextMeshProUGUI rank3;

    public GameObject settingsPanel;
    private bool isSettingsPanelOpen = false;
    public GameObject musicButton;
    public GameObject effectButton;
    public GameObject vibrationButton;
    public Sprite soundImage;
    public Sprite soundImageOff;
    public Sprite effectImage;
    public Sprite effectImageOff;
    public Sprite vibrationImage;
    public Sprite vibrationImageOff;

    public TextMeshProUGUI coins;

    public string url = "https://play.google.com/store/apps/dev?id=6745741300491570853&hl=en_US";

    void Start()
    {
        Open_Close_Manager("Main");
    }

    private void Update()
    {
       coins.text = CoinManager.Instance.GetTotalCoins().ToString();
    }

    public void ToGame()
	{
		SceneManager.LoadScene("MainGame");
	}

    public void Open_close_MainGame()
    {
        Open_Close_Manager("Main");
    }

    public void Open_Close_Skin()
    {
        Open_Close_Manager("Skin");
    }

    public void Open_Close_Shop()
    {
        Open_Close_Manager("Shop");
    }

    public void Open_Close_MoreGames()
    {
        Open_Close_Manager("MoreGames");
    }

    public void Open_Close_ScoreManager()
    {
        Open_Close_Manager("ScoreManager");
    }

    public void Open_Close_Settings()
    {
        Open_Close_Manager("Settings");
    }

    private void Open_Close_Manager(string menu)
    {
        switch (menu)
        {
            case "Main":
                mainPanel.SetActive(true);
                skinPanel.SetActive(false);
                skinButton.transform.localScale = normalSize;
                playButton.transform.localScale = increaseSize;
                shopButton.transform.localScale = normalSize;
                moreGamesButton.transform.localScale = normalSize;
                rateUsButton.transform.localScale = normalSize;
				SoundManager.Instance.PlayButtonClick();
                break;
            case "Skin":
                mainPanel.SetActive(false);
                skinPanel.SetActive(true);
                skinButton.transform.localScale = increaseSize;
                playButton.transform.localScale = normalSize;
                shopButton.transform.localScale = normalSize;
                moreGamesButton.transform.localScale = normalSize;
                rateUsButton.transform.localScale = normalSize;
				SoundManager.Instance.PlayButtonClick();
                break;
            case "Shop":
                skinButton.transform.localScale = normalSize;
                playButton.transform.localScale = normalSize;
                shopButton.transform.localScale = increaseSize;
                moreGamesButton.transform.localScale = normalSize;
                rateUsButton.transform.localScale = normalSize;
				SoundManager.Instance.PlayButtonClick();
                break;
            case "MoreGames":
                Application.OpenURL(url);
                skinButton.transform.localScale = normalSize;
                playButton.transform.localScale = normalSize;
                shopButton.transform.localScale = normalSize;
                moreGamesButton.transform.localScale = increaseSize;
                rateUsButton.transform.localScale = normalSize;
				SoundManager.Instance.PlayButtonClick();
                break;
            case "RateUs":
                skinButton.transform.localScale = normalSize;
                playButton.transform.localScale = normalSize;
                shopButton.transform.localScale = normalSize;
                moreGamesButton.transform.localScale = normalSize;
                rateUsButton.transform.localScale = increaseSize;
				SoundManager.Instance.PlayButtonClick();
                break;
            case "ScoreManager":
                
                List<int> highScoreList = ScoreManager.Instance.GetHighScores();
				SoundManager.Instance.PlayButtonClick();

                if (!isScorePanelOpen)
                {
                    ScoreRankingPanel.SetActive(true);
                    isScorePanelOpen = true;

                    if (highScoreList[0] <= 9999)
                    {
                        rank1.text = highScoreList[0].ToString();
                    }
                    else
                    {
                        rank1.text = (highScoreList[0] / 1000).ToString() + "k";
                    }
                    
                    if (highScoreList[1] <= 9999)
                    {
                        rank2.text = highScoreList[1].ToString();
                    }
                    else
                    {
                        rank2.text = (highScoreList[1] / 1000).ToString() + "k";
                    }
                    
                    if (highScoreList[2] <= 9999)
                    {
                        rank3.text = highScoreList[2].ToString();
                    }
                    else
                    {
                        rank3.text = (highScoreList[2] / 1000).ToString() + "k";
                    }

                }
                else
                {
                    ScoreRankingPanel.SetActive(false);
                    isScorePanelOpen = false;
                }
                break;  
            case "Settings":
				SoundManager.Instance.PlayButtonClick();

                if(!isSettingsPanelOpen)
                {
                    settingsPanel.SetActive(true);
                    isSettingsPanelOpen = true;

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
                    settingsPanel.SetActive(false);
                    isSettingsPanelOpen = false;
                }
                break;
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

    public void CircleSkinClick()
    {
		SoundManager.Instance.PlayButtonClick();
        circleContents.SetActive(true);
        scrollRect.content = circleContents.GetComponent<RectTransform>();
        flagContents.SetActive(false);
		planetContents.SetActive(false);
        category.text = "CIRCLES";
    }

    public void FlagSkinClick()
    {
		SoundManager.Instance.PlayButtonClick();
        flagContents.SetActive(true);
        scrollRect.content = flagContents.GetComponent<RectTransform>();
        circleContents.SetActive(false);
		planetContents.SetActive(false);
        category.text = "FLAGS";
    }

	public void PlanetSkinClick()
    {
		SoundManager.Instance.PlayButtonClick();
        planetContents.SetActive(true);
        scrollRect.content = planetContents.GetComponent<RectTransform>();
        flagContents.SetActive(false);
		circleContents.SetActive(false);
        category.text = "PLANETS";
    }
}