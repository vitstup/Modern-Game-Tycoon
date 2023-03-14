using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuScript : MonoBehaviour
{
    public static GameMenuScript instance;

    [SerializeField] private GameObject menuCanvas;

    private void Awake()
    {
        instance = this;
    }

    public void OpenMenu(bool open)
    {
        menuCanvas.gameObject.SetActive(open);
        TimeManager.instance.NecessaryPause(open);
    }

    public void Settings()
    {
        SettingsScript.instance.OpenSettings();
    }

    public void Exit()
    {
        LoadingScript.instance.LoadScene(1);
    }
}