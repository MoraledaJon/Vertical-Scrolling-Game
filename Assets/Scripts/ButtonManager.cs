using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Image selectedButtonImage;
    public PlayerSkin playerSkin;
    public GameManager gameManager;

    public void ToMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void SelectedButton(GameObject clickedButton)
    {
        // Get the Image component of the clicked button
        Image buttonImage = clickedButton.GetComponent<Image>();

        if (buttonImage != null)
        {
            // Store the sprite of the clicked button in the variable
            selectedButtonImage = buttonImage;

            // Now you can use 'selectedButtonImage' as needed
            gameManager.selectedSkin = selectedButtonImage;
            playerSkin.GetComponent<SpriteRenderer>().sprite = selectedButtonImage.sprite;
        }
        else
        {
            Debug.LogError("Button does not have an Image component.");
        }
    }
}