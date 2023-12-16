using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkin : MonoBehaviour
{
 void Start()
    {
        int selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0); // Default to 0 if not set

        Sprite selectedSkin = SkinManager.Instance.GetSkin(selectedSkinIndex);

        transform.GetComponent<SpriteRenderer>().sprite = selectedSkin;
    }
}