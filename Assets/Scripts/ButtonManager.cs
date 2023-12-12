using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    private Image selectedButtonImage;

    public Animator canvasAnimation;
    private bool skinMenuIsOpen = false;

    public GameObject circleContents;
    public GameObject flagContents;

    public ScrollRect scrollRect;

    private Image currentlySelected;
    
    public Sprite currentlySelectedSprite;
    public Sprite notSelectedSprite;

    public Image defaultButton;

    public Button playButton;
    public Button skinButton;
    public Button shopButton;
    public Button moreGamesButton;
    public Button rateUsButton;

    private Vector3 increaseSize = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 normalSize = new Vector3(0.9f, 0.9f, 0.9f);

    void Start()
    {
        if(GameManager.instance.selectedSkin == null)
        {
            defaultButton.sprite = currentlySelectedSprite;

            currentlySelected = defaultButton;

            currentlySelected.sprite = currentlySelectedSprite;

            GameManager.instance.selectedSkin = defaultButton.transform.GetChild(0).gameObject.GetComponent<Image>();

            skinButton.transform.localScale = normalSize;
            playButton.transform.localScale = increaseSize;
            shopButton.transform.localScale = normalSize;
            moreGamesButton.transform.localScale = normalSize;
            rateUsButton.transform.localScale = normalSize;
        }

    }

    public void ToMainGame()
    {
        //SceneManager.LoadScene("MainGame");

        if (skinMenuIsOpen)
        {
            canvasAnimation.SetTrigger("closeSkin");
            skinMenuIsOpen = false;
            skinButton.transform.localScale = normalSize;
            playButton.transform.localScale = increaseSize;
            shopButton.transform.localScale = normalSize;
            moreGamesButton.transform.localScale = normalSize;
            rateUsButton.transform.localScale = normalSize;
        }
    }


    public void Open_Close_Skin()
    {
        if(!skinMenuIsOpen)
        {
            canvasAnimation.SetTrigger("openSkin");
            skinMenuIsOpen = true;
            skinButton.transform.localScale = increaseSize;
            playButton.transform.localScale = normalSize;
            shopButton.transform.localScale = normalSize;
            moreGamesButton.transform.localScale = normalSize;
            rateUsButton.transform.localScale = normalSize;
        }
    }

    public void Open_Close_Shop()
    {
        
    }


    public void SelectedButton(GameObject clickedButton)
    {
        Image buttonImage = clickedButton.transform.GetChild(0).gameObject.GetComponent<Image>();
        GameManager.instance.selectedSkin = clickedButton.transform.GetChild(0).gameObject.GetComponent<Image>();

        if (buttonImage != null)
        {


            if (currentlySelected)
            {
                currentlySelected.sprite = notSelectedSprite;
                currentlySelected = clickedButton.GetComponent<Image>();
                currentlySelected.sprite = currentlySelectedSprite;
            }
            else
            {
                currentlySelected = clickedButton.GetComponent<Image>();
                currentlySelected.sprite = currentlySelectedSprite;
            }
        }
        else
        {
            Debug.LogError("Button does not have an Image component.");
        }
    }

    public void FlagSkinClick()
    {
        flagContents.SetActive(true);
        scrollRect.content = flagContents.GetComponent<RectTransform>();
        circleContents.SetActive(false);    
    }

    public void CircleSkinClick()
    {
        circleContents.SetActive(true);
        scrollRect.content = circleContents.GetComponent<RectTransform>();
        flagContents.SetActive(false);
    }
}