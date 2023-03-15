using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;

    private void Start()
    {
        versionText.text = Application.version;
    }
    
    public void Settings()
    {
        SettingsScript.instance.OpenSettings();
    }

    public void Exit()
    {
        Application.Quit();
    }
}