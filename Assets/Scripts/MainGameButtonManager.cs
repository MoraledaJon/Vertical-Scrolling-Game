using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameButtonManager : MonoBehaviour
{
    public void ToMenuScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
