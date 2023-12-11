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

    public Sprite[] circleSprites;
    public Sprite[] flagSprites;
    public GameObject[] skinButtons;
    public TextMeshProUGUI skinText;

    public void ToMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Open_Close_Skin()
    {
        if(!skinMenuIsOpen)
        {
            canvasAnimation.SetTrigger("openSkin");
            skinMenuIsOpen = true;
        }
        else
        {
            canvasAnimation.SetTrigger("closeSkin");
            skinMenuIsOpen = false;
        }
    }
    public void SelectedButton(GameObject clickedButton)
    {
        Image buttonImage = clickedButton.GetComponent<Image>();

        if (buttonImage != null)
        {
            selectedButtonImage = buttonImage;


            GameManager.selectedSkin = selectedButtonImage;
        }
        else
        {
            Debug.LogError("Button does not have an Image component.");
        }
    }

    public void FlagSkinClick()
    {
        skinText.text = "FLAG SKINS:";
        for(int i = 0; i < skinButtons.Length; i++)
        {
            skinButtons[i].GetComponent<Image>().sprite = flagSprites[i];
        }
    }

    public void CircleSkinClick()
    {
        skinText.text = "CIRCLE SKINS:";
        for (int i = 0; i < skinButtons.Length; i++)
        {
            skinButtons[i].GetComponent<Image>().sprite = circleSprites[i];
        }
    }
}