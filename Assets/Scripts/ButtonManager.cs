using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    private Image selectedButtonImage;

	public GameObject circleContents;
    public GameObject flagContents;
	public GameObject planetContents;

    public ScrollRect scrollRect;

    private Image currentlySelected;
    
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


    public string url = "https://play.google.com/store/apps/dev?id=6745741300491570853&hl=en_US";

    void Start()
    {
        Open_Close_Manager("Main");
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

    public void Open_Close_RateUs()
    {
        Open_Close_Manager("RateUs");
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
                break;
            case "Skin":
                mainPanel.SetActive(false);
                skinPanel.SetActive(true);
                skinButton.transform.localScale = increaseSize;
                playButton.transform.localScale = normalSize;
                shopButton.transform.localScale = normalSize;
                moreGamesButton.transform.localScale = normalSize;
                rateUsButton.transform.localScale = normalSize;
				
				GameObject button = GameObject.Find("1");
			
				Debug.Log(button);
                break;
            case "Shop":
                skinButton.transform.localScale = normalSize;
                playButton.transform.localScale = normalSize;
                shopButton.transform.localScale = increaseSize;
                moreGamesButton.transform.localScale = normalSize;
                rateUsButton.transform.localScale = normalSize;
                break;
            case "MoreGames":
                Application.OpenURL(url);
                skinButton.transform.localScale = normalSize;
                playButton.transform.localScale = normalSize;
                shopButton.transform.localScale = normalSize;
                moreGamesButton.transform.localScale = increaseSize;
                rateUsButton.transform.localScale = normalSize;
                break;
            case "RateUs":
                skinButton.transform.localScale = normalSize;
                playButton.transform.localScale = normalSize;
                shopButton.transform.localScale = normalSize;
                moreGamesButton.transform.localScale = normalSize;
                rateUsButton.transform.localScale = increaseSize;
                break;
        }
    }

    public void CircleSkinClick()
    {
        circleContents.SetActive(true);
        scrollRect.content = circleContents.GetComponent<RectTransform>();
        flagContents.SetActive(false);
		planetContents.SetActive(false);
        category.text = "CIRCLES";
    }

    public void FlagSkinClick()
    {
        flagContents.SetActive(true);
        scrollRect.content = flagContents.GetComponent<RectTransform>();
        circleContents.SetActive(false);
		planetContents.SetActive(false);
        category.text = "FLAGS";
    }

	public void PlanetSkinClick()
    {
        planetContents.SetActive(true);
        scrollRect.content = planetContents.GetComponent<RectTransform>();
        flagContents.SetActive(false);
		circleContents.SetActive(false);
        category.text = "PLANETS";
    }
}