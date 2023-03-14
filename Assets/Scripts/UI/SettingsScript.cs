using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    public static SettingsScript instance;


    [field: SerializeField] public Canvas settingsCanvas { get; private set; }
    private Camera mainCamera;
    private Resolution[] resolutions;
    private string[] textlanguageNames = new string[] { "English", "Русский", "Fran?ais", "Deutsch", "Espa?ol", "Italiano" };
    [SerializeField] private Color[] bgColors;

    [SerializeField] TextMeshProUGUI backgroundText;
    [SerializeField] TextMeshProUGUI languageText;
    [SerializeField] TextMeshProUGUI resolutionText;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [field: SerializeField] public Button clsBtn { get; private set; }

    private int baseBg;
    private int baseLang;
    private int baseRes;
    private float baseMus;
    private float baseSfx;

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);

        mainCamera = Camera.main;
        resolutions = Screen.resolutions;
        if (!PlayerPrefs.HasKey("Background")) PlayerPrefs.SetInt("Background", 0);
        if (!PlayerPrefs.HasKey("Language")) PlayerPrefs.SetInt("Language", 0);
        if (!PlayerPrefs.HasKey("Resolutuion")) PlayerPrefs.SetInt("Resolutuion", resolutions.Length - 1);
        if (!PlayerPrefs.HasKey("MusicVolume")) PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        if (!PlayerPrefs.HasKey("SfxVolume")) PlayerPrefs.SetFloat("SfxVolume", 0.5f);

        SetRenderCamera();
    }

    private void Start()
    {
        ApplyBackground();
        ApplyResolution();
        ApplyMusic();
        ApplySfx();
        ChangeTexts();
    }

    public void SetBackground(bool positive)
    {
        int id = PlayerPrefs.GetInt("Background");
        id += positive ? 1 : -1;
        if (id < 0) id = bgColors.Length - 1;
        if (id >= bgColors.Length) id = 0;
        PlayerPrefs.SetInt("Background", id);
        ChangeTexts();
        ApplyBackground();
    }

    public void SetLanguage(bool positive)
    {
        int id = PlayerPrefs.GetInt("Language");
        id += positive ? 1 : -1;
        if (id < 0) id = textlanguageNames.Length - 1;
        if (id >= textlanguageNames.Length) id = 0;
        PlayerPrefs.SetInt("Language", id);
        ChangeTexts();
        ApplyLanguage();
    }

    public void SetResolution(bool positive)
    {
        int id = PlayerPrefs.GetInt("Resolutuion");
        id += positive ? 1 : -1;
        if (id < 0) id = resolutions.Length - 1;
        if (id >= resolutions.Length) id = 0;
        PlayerPrefs.SetInt("Resolutuion", id);
        ChangeTexts();
    }

    public void SetMusic(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        ChangeTexts();
        ApplyMusic();
    }

    public void SetSFX(float value)
    {
        PlayerPrefs.SetFloat("SfxVolume", value);
        ChangeTexts();
        ApplySfx();
    }

    private void ChangeTexts()
    {
        // bg
        backgroundText.text = Localization.Localize("bgColor." + PlayerPrefs.GetInt("Background"));
        // language 
        languageText.text = textlanguageNames[PlayerPrefs.GetInt("Language")];
        // resolution 
        Resolution resolution = resolutions[PlayerPrefs.GetInt("Resolutuion")];
        resolutionText.text = resolution.width + "x" + resolution.height;
        // music
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        // sfx
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
    }

    private void ApplyBackground()
    {
        mainCamera.backgroundColor = bgColors[PlayerPrefs.GetInt("Background")];
    }

    private void ApplyLanguage()
    {
        Localization.LanguageChanged();
    }

    private void ApplyResolution()
    {
        Resolution resolution = resolutions[PlayerPrefs.GetInt("Resolutuion")];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void ApplyMusic()
    {
        AudioManager.instance.SetMusic(PlayerPrefs.GetFloat("MusicVolume"));
    }

    private void ApplySfx()
    {
        AudioManager.instance.SetSfx(PlayerPrefs.GetFloat("SfxVolume"));
    }

    public void OpenSettings()
    {
        settingsCanvas.gameObject.SetActive(true);
        ChangeTexts();
        baseBg = PlayerPrefs.GetInt("Background");
        baseLang = PlayerPrefs.GetInt("Language");
        baseRes = PlayerPrefs.GetInt("Resolutuion");
        baseMus = PlayerPrefs.GetFloat("MusicVolume");
        baseSfx = PlayerPrefs.GetFloat("SfxVolume");
    }

    public void Revert()
    {
        settingsCanvas.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Background", baseBg);
        PlayerPrefs.SetInt("Language", baseLang);
        PlayerPrefs.SetInt("Resolutuion", baseRes);
        PlayerPrefs.SetFloat("MusicVolume", baseMus);
        PlayerPrefs.SetFloat("SfxVolume", baseSfx);
        ApplyBackground();
        ApplyLanguage();
        ApplyResolution();
        ApplyMusic();
        ApplySfx();
        ChangeTexts();
    }

    public void Apply()
    {
        ApplyResolution();
        settingsCanvas.gameObject.SetActive(false);
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Level loaded");
        mainCamera = Camera.main;
        SetRenderCamera();
        ApplyBackground();
    }

    private void SetRenderCamera()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        if (level != 2) settingsCanvas.worldCamera = mainCamera;
        else settingsCanvas.worldCamera = mainCamera.transform.GetChild(0).GetComponent<Camera>();
    }
}