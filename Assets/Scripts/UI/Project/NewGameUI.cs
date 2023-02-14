using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameUI : MonoBehaviour
{
    public static NewGameUI instance;

    [SerializeField] private GameObject NewGamePanel;

    [SerializeField] private TMP_InputField gameName;

    [SerializeField] private TextMeshProUGUI engine;
    [SerializeField] private TextMeshProUGUI theme;
    [SerializeField] private TMP_Dropdown size;
    [SerializeField] private TMP_Dropdown genre;

    [SerializeField] private TMP_Dropdown sequel;

    [SerializeField] private PlatformSelector[] platforms;

    [SerializeField] private TextMeshProUGUI errorText;

    private int openedPlatformSelectorId;

    private Game game;

    private void Awake()
    {
        instance = this; 
        EnginePanel.SelectedEngine.AddListener(EngineSelected);
        PlatformPanel.SelectedPlatform.AddListener(PlatformSelected);
        ThemePanel.SelectedTheme.AddListener(ThemeSelected);
    }

    private void Start()
    {
        InitializeSizeDropdown();
        InitializeGenreDropdown();
        ResetInfo();
    }

    private void InitializeSizeDropdown()
    {
        LocalizedTMProDropdown loc = size.GetComponent<LocalizedTMProDropdown>();
        size.ClearOptions();
        var sizes = Constans.gameSizes;
        for (int i = 0; i < sizes.Length; i++)
        {
            size.options.Add(new TMP_Dropdown.OptionData() { text = sizes[i] });
        }
        loc.SetKeys(new string[] { "indie" });
        size.value = 0;
    }

    private void InitializeGenreDropdown()
    {
        LocalizedTMProDropdown loc = genre.GetComponent<LocalizedTMProDropdown>();
        genre.ClearOptions();
        var genres = AttributesManager.instance.genres;
        string[] locKeys = new string[genres.Length];
        for (int i = 0; i < genres.Length; i++)
        {
            genre.options.Add(new TMP_Dropdown.OptionData() { text = genres[i].name });
            locKeys[i] = genres[i].localizationKey;
        }
        loc.SetKeys(locKeys);
        genre.value = 0;
    }
    public void ResetInfo()
    {
        game = new Game();
        gameName.text = "";
        size.value = 0;
        genre.value = 0;
        SetEngine(null);
        SetTheme(null);
        for (int i = 0; i < platforms.Length; i++)
        {
            SetPlatform(null, i);
        }
    }

    // maybe some sequel void 

    public void SetSize(int value)
    {
        game.size = value;
    }

    public void SetGenre(int value)
    {
        game.genre = AttributesManager.instance.genres[value];
    }

    public void SetName(string value)
    {
        game.projectName = value;
        Debug.Log(game.projectName);
    }

    public void RandomGameName()
    {
        gameName.text = Constans.gameNames[Random.Range(0, Constans.gameNames.Length)];
    }


    private void SetEngine(Engine engine)
    {
        if (engine == null) this.engine.text = Localization.Localize("none");
        else this.engine.text = engine.info.name;
        game.engine = engine;
    }

    private void SetTheme(ThemeInfo theme)
    {
        if (theme == null) this.theme.text = Localization.Localize("none");
        else this.theme.text = Localization.Localize(theme.localizationKey);
        game.theme = theme;
    }

    private void SetPlatform(Platform platform, int platformSelector)
    {
        game.platforms[platformSelector] = platform;
        platforms[platformSelector].SetInfo(platform);
    }

    private void EngineSelected(Engine engine)
    {
        if (RedactingThisPanel())
        {
            SetEngine(engine);
        }
    }

    private void PlatformSelected(Platform platform)
    {
        if (RedactingThisPanel())
        {
            SetPlatform(platform, openedPlatformSelectorId);
        }
    }

    private void ThemeSelected(ThemeInfo theme)
    {
        if (RedactingThisPanel())
        {
            SetTheme(theme);
        }
    }

    private bool RedactingThisPanel()
    {
        return NewGamePanel.activeSelf;
    }

    public void SelectEngine()
    {
        AttributesUI.instance.OpenEngines(game.engine);
    }

    public void SelectPlatform(int id)
    {
        openedPlatformSelectorId = id;
        AttributesUI.instance.OpenPlatforms(game.platforms);
    }

    public void SelectTheme()
    {
        AttributesUI.instance.OpenThemes();
    }

    public void UnselectPlatform(int id)
    {
        SetPlatform(null, id);
    }

    public void StartDevelopment()
    {
        if (string.IsNullOrEmpty(game.projectName)) StartCoroutine(ShowError(0));
        else if (game.engine == null) StartCoroutine(ShowError(1));
        else if (game.theme == null) StartCoroutine(ShowError(2));
        else if (game.platforms[0] == null) StartCoroutine(ShowError(3));
        else
        {
            ProjectManager.instance.project = game;
            ResetInfo();
            MainUI.instance.OpenProjectCanvas(false);
        }
    }

    private IEnumerator ShowError(int errorId)
    {
        errorText.text = Localization.Localize("startError." + errorId);
        errorText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1); 
        errorText.gameObject.SetActive(false);
    }
}