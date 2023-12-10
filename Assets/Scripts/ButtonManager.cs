using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Image selectedButtonImage;
    public GameObject playerSkin;

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

    public void SelectedButton(GameObject clickedButton)
    {
        Image buttonImage = clickedButton.GetComponent<Image>();

        if (buttonImage != null)
        {
            selectedButtonImage = buttonImage;


            GameManager.selectedSkin = selectedButtonImage;

            playerSkin.GetComponent<SpriteRenderer>().sprite = selectedButtonImage.sprite;
        }
        else
        {
            Debug.LogError("Button does not have an Image component.");
        }
    }
}