using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;

public class LocalizationMainMenu : MonoBehaviour
{
    [Header("INTERFACE")]
    public TextMeshProUGUI play;
    public TextMeshProUGUI score;
    public TextMeshProUGUI menu;
    public TextMeshProUGUI skin;
    public TextMeshProUGUI more;
    public TextMeshProUGUI settings;
    [Header ("SKIN CATEGORY")]
    public TextMeshProUGUI balls;
    public TextMeshProUGUI flags;
    public TextMeshProUGUI space;
    [Header("SKIN BALLS")]
    public TextMeshProUGUI red;
    public TextMeshProUGUI re;
    public TextMeshProUGUI tt;

    private void Awake()
    {
        if (!LocalizationSettings.InitializationOperation.IsDone)
            return;

        var selectedLocale = LocalizationSettings.SelectedLocale;

        var language = selectedLocale.Identifier.Code;


        switch (language)
        {
            case "en":
                // Do nothing
                break;
            case "es":
                //score.fontSize = 30;

                break;
            case "it":
                //score.fontSize = 35;
                break;
            case "ja":
                // Do nothing
                break;
            case "ru-RU":
                //play.fontSize = 40;
                more.fontSize = 15;
                settings.fontSize = 45;
                break;
            default:
                break;
        }
    }
}