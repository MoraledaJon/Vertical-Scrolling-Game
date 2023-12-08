using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Image selectedSkin;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
