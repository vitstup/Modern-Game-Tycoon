using UnityEngine;

public class BaseLangSetter : MonoBehaviour
{
    private void Awake()
    {
        SetLang();
    }

    private void SetLang()
    {
        if (PlayerPrefs.HasKey("Language")) return;
        else if (Application.systemLanguage == SystemLanguage.Russian) PlayerPrefs.SetInt("Language", 1);
        else if (Application.systemLanguage == SystemLanguage.French) PlayerPrefs.SetInt("Language", 2);
        else if (Application.systemLanguage == SystemLanguage.German) PlayerPrefs.SetInt("Language", 3);
        else if (Application.systemLanguage == SystemLanguage.Spanish) PlayerPrefs.SetInt("Language", 4);
        else if (Application.systemLanguage == SystemLanguage.Italian) PlayerPrefs.SetInt("Language", 5);
        else PlayerPrefs.SetInt("Language", 0);
    }
}