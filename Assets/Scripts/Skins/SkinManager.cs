using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; 
    public Sprite[] skins;
    public GameObject[] skinBuyButtons;
    public GameObject[] buttons;
    public Sprite selectedSprite;
    public Sprite unselectedSprite;

    private GameObject currentButton;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        // Check if it's the player's first time running the game
        if (!PlayerPrefs.HasKey("SelectedButtonIndex"))
        {
            // Unlock the first skin and set it as selected by default
            UnlockSkin(0);
            currentButton = GetButton(0); // Ensure currentButton is assigned
            currentButton.GetComponent<Image>().sprite = selectedSprite;
        }
        else
        {
            // Load previously selected skin
            int selectedButtonIndex = PlayerPrefs.GetInt("SelectedButtonIndex", 0);
            currentButton = GetButton(selectedButtonIndex);
            if (currentButton != null) // Check if the button is not null
            {
                currentButton.GetComponent<Image>().sprite = selectedSprite;
            }
        }

        InitializeSkins();
        InitializeButtonStates();
    }



    void InitializeSkins()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            // Unlock the first skin by default
            bool isUnlocked = PlayerPrefs.GetInt("SkinUnlocked_" + i, i == 0 ? 1 : 0) == 1;
            GameObject button = GetButton(i);
            button.GetComponent<Button>().interactable = isUnlocked;
            // Additional UI updates for locked/unlocked state can be done here
        }
    }

    void InitializeButtonStates()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            bool isUnlocked = PlayerPrefs.GetInt("SkinUnlocked_" + i, i == 0 ? 1 : 0) == 1;
            bool isBuyButtonActive = PlayerPrefs.GetInt("SkinBuyButtonActive_" + i, i == 0 ? 0 : 1) == 1; // Default to false for first skin

            GameObject button = GetButton(i);
            button.GetComponent<Button>().interactable = isUnlocked;

            if (skinBuyButtons.Length > i) // Check if the button exists in the array
            {
                skinBuyButtons[i].SetActive(isBuyButtonActive);
            }
        }
    }

    public bool IsSkinUnlocked(int index)
    {
        return PlayerPrefs.GetInt("SkinUnlocked_" + index, 0) == 1;
    }

    public void UnlockSkin(int index)
    {
        PlayerPrefs.SetInt("SkinUnlocked_" + index, 1);
        PlayerPrefs.SetInt("SkinBuyButtonActive_" + index, 0); // 0 for false
        PlayerPrefs.Save();

        UpdateButtonStates(index);
    }

    private void UpdateButtonStates(int index)
    {
        GameObject button = GetButton(index);
        button.GetComponent<Button>().interactable = true;
        skinBuyButtons[index].SetActive(false);
    }

    // Call this method when the player purchases a skin
    public void PurchaseSkin(int index)
    {
        int valor = 0;

        if(index >= 0 || index <= 9)
        {
            valor = 50;
        }
        else if (index >= 10 || index <= 19)
        {
            valor = 75;
        }
        else if (index >= 20 || index <= 29)
        {
            valor = 100;
        }

        if (CoinManager.Instance.GetTotalCoins() >= valor)
        {
            SoundManager.Instance.BoughtSkinSound();
            CoinManager.Instance.RemoveCoin(valor);
            UnlockSkin(index);
        }
    }


    public Sprite GetSkin(int index)
    {
        if (index >= 0 && index < skins.Length)
        {
            return skins[index];
        }
        return null;
    }

    public GameObject GetButton(int index)
    {
        if (index >= 0 && index < skins.Length)
        {
            return buttons[index];
        }
        return null;
    }

    public void SelectSkin(int index)
    {
        if (IsSkinUnlocked(index))
        {
            PlayerPrefs.SetInt("SelectedSkinIndex", index);
            PlayerPrefs.SetInt("SelectedButtonIndex", index);

            currentButton.GetComponent<Image>().sprite = unselectedSprite;
            currentButton = GetButton(index);
            currentButton.GetComponent<Image>().sprite = selectedSprite;


            PlayerPrefs.Save();
        }
    }
}
