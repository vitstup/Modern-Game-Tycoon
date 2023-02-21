using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributesUI : MonoBehaviour
{
    public static AttributesUI instance;

    [SerializeField] private GameObject EnginesPanel;
    [SerializeField] private GameObject PlatformsPanel;
    [SerializeField] private GameObject ThemesPanel;

    [SerializeField] private Transform engineContent;
    [SerializeField] private Transform platformContent;
    [SerializeField] private Transform themeContent;

    [SerializeField] private EnginePanel enginePrefab;
    [SerializeField] private PlatformPanel platformPrefab;
    [SerializeField] private ThemePanel themePrefab;

    private EnginePanel[] engines;
    private PlatformPanel[] platforms;
    private ThemePanel[] themes;

    private void Awake()
    {
        instance = this;
        EnginePanel.SelectedEngine.AddListener(EngineSelected);
        PlatformPanel.SelectedPlatform.AddListener(PlatformSelected);
        ThemePanel.SelectedTheme.AddListener(ThemeSelected);
    }

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

    private void UpdateEngines(Engine selected)
    {
        var available = AttributesManager.instance.availableEngines();
        if (available.Length > engines.Length) Debug.LogError("Small amout of engine panels");
        for (int i = 0; i < engines.Length; i++)
        {
            if (i >= available.Length || available[i] == selected) { engines[i].gameObject.SetActive(false); continue; }
            engines[i].gameObject.SetActive(true);
            engines[i].SetInfo(available[i]);
        }
    }

    private void UpdatePlatforms(Platform[] selected)
    {
        var available = AttributesManager.instance.availablePlatforms();
        if (available.Length > platforms.Length) Debug.LogError("Small amout of platform panels");
        for (int i = 0; i < platforms.Length; i++)
        {
            if (i >= available.Length) { platforms[i].gameObject.SetActive(false); continue; }
            if (selected != null)
            {
                bool close = false;
                for (int s = 0; s < selected.Length; s++)
                {
                    if (available[i] == selected[s]) { close = true; break; }
                }
                if (close) { platforms[i].gameObject.SetActive(false); continue; }
            }
            platforms[i].gameObject.SetActive(true);
            platforms[i].SetInfo(available[i]);
        }
    }

    private void UpdateThemes()
    {
        for (int i = 0; i < themes.Length; i++)
        {
            themes[i].SetTheme(AttributesManager.instance.themes[i]);
        }
    }

    public void OpenEngines(Engine engine)
    {
        EnginesPanel.SetActive(true);
        UpdateEngines(engine);
    }

    public void OpenPlatforms(Platform[] platforms)
    {
        PlatformsPanel.SetActive(true);
        UpdatePlatforms(platforms);
    }

    public void OpenThemes()
    {
        ThemesPanel.SetActive(true);
        UpdateThemes();
    }

    private void EngineSelected(Engine engine)
    {
        EnginesPanel.SetActive(false);
    }

    private void PlatformSelected(Platform platform)
    {
        PlatformsPanel.SetActive(false);
    }

    private void ThemeSelected(ThemeInfo theme)
    {
        ThemesPanel.SetActive(false);
    }
}