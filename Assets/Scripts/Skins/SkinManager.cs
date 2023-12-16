using System;
using UnityEngine;
using UnityEngine.UI;
public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; 
    public Sprite[] skins;
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
        int selectedButtonIndex = PlayerPrefs.GetInt("SelectedButtonIndex", 0);
        currentButton = GetButton(selectedButtonIndex);
        currentButton.GetComponent<Image>().sprite = selectedSprite;
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
        PlayerPrefs.SetInt("SelectedSkinIndex", index);
        PlayerPrefs.SetInt("SelectedButtonIndex", index);

        currentButton.GetComponent<Image>().sprite = unselectedSprite;
        currentButton = GetButton(index);
        currentButton.GetComponent<Image>().sprite = selectedSprite;


        PlayerPrefs.Save();
    }
}
