using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkin : MonoBehaviour
{
    private GameManager gameManager;
    
    void Start()
    {
        if(GetComponent<GameManager>())
        {
            gameManager = GetComponent<GameManager>();
            transform.GetComponent<SpriteRenderer>().sprite = gameManager.selectedSkin.sprite;
        }
    }
}
