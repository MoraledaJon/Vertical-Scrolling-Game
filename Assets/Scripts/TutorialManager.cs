using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private const string TutorialKey = "HasPlayedTutorial";
    public static TutorialManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadAppropriateScene()
    {
        if (HasPlayedTutorial())
        {
            SceneManager.LoadScene("MainGame");
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
            SetTutorialPlayed();
        }
    }

    private bool HasPlayedTutorial()
    {
        return PlayerPrefs.GetInt(TutorialKey, 0) == 1;
    }

    private void SetTutorialPlayed()
    {
        PlayerPrefs.SetInt(TutorialKey, 1);
        PlayerPrefs.Save();
    }
}