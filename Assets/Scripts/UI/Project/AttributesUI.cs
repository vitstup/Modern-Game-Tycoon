using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributesUI : MonoBehaviour
{
    public static AttributesUI instance;

    [SerializeField] private GameObject EnginePanel;
    [SerializeField] private GameObject PlatformPanel;
    [SerializeField] private GameObject ThemePanel;

    [SerializeField] private Transform engineContent;
    [SerializeField] private Transform platformContent;
    [SerializeField] private Transform themeContent;

    [SerializeField] private EnginePanel enginePrefab;
    [SerializeField] private PlatformPanel platformPrefab;
    [SerializeField] private ThemePanel themePrefab;

    private EnginePanel[] engines;
    private PlatformPanel[] platforms;
    private ThemePanel[] themes;

    private void Awake() => instance = this;

    private void Start()
    {
        InitializeEngines();
        InitializePlatforms();
        InitializeThemes();
    }

    private void InitializeEngines()
    {
        engines = new EnginePanel[10];
        for (int i = 0; i < engines.Length; i++)
        {
            engines[i] = Instantiate(enginePrefab, engineContent);
        }
    }

    private void InitializePlatforms()
    {
        platforms = new PlatformPanel[10];
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = Instantiate(platformPrefab, platformContent);
        }
    }

    private void InitializeThemes()
    {
        themes = new ThemePanel[AttributesManager.instance.themes.Length];
        for (int i = 0; i < themes.Length; i++)
        {
            themes[i] = Instantiate(themePrefab, themeContent);
        }
    }

    public void UpdateEngines()
    {
        var available = AttributesManager.instance.availableEngines();
        if (available.Length > engines.Length) Debug.LogError("Small amout of engine panels");
        for (int i = 0; i < engines.Length; i++)
        {
            if (i >= available.Length) { engines[i].gameObject.SetActive(false); continue; }
            engines[i].gameObject.SetActive(true);
            engines[i].SetInfo(available[i]);
        }
    }

    public void UpdatePlatforms()
    {
        var available = AttributesManager.instance.availablePlatform();
        if (available.Length > platforms.Length) Debug.LogError("Small amout of platform panels");
        for (int i = 0; i < platforms.Length; i++)
        {
            if (i >= available.Length) { platforms[i].gameObject.SetActive(false); continue; }
            platforms[i].gameObject.SetActive(true);
            platforms[i].SetInfo(available[i]);
        }
    }

    public void UpdateThemes()
    {
        for (int i = 0; i < themes.Length; i++)
        {
            themes[i].SetTheme(AttributesManager.instance.themes[i]);
        }
    }
}