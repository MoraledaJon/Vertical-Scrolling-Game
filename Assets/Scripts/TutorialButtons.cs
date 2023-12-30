using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButtons : MonoBehaviour
{
    public GameObject[] tutorials;

    public void SwitchToTutorial(int index)
    {
        foreach (var tutorial in tutorials)
        {
            tutorial.SetActive(false);
        }

        if (index >= 0 && index < tutorials.Length)
        {
            tutorials[index].SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid tutorial index: " + index);
        }
    }

    public void ToMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}