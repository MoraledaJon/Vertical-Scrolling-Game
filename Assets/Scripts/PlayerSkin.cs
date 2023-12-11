using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkin : MonoBehaviour
{

    void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = GameManager.selectedSkin.sprite;
    }
}