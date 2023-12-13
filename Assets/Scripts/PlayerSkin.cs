using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkin : MonoBehaviour
{
	private Image storeImage;
	
    void Start()
    {
		storeImage = GameManager.instance.selectedSkin;
		
        transform.GetComponent<SpriteRenderer>().sprite = storeImage.sprite;
		
		GameManager.instance.selectedSkin = storeImage;
    }
}