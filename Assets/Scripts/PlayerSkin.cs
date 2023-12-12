using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkin : MonoBehaviour
{

    void Start()
    {
        GameManager.instance.canvas.SetActive(false);

        transform.GetComponent<SpriteRenderer>().sprite = GameManager.instance.selectedSkin.sprite;
    }
}