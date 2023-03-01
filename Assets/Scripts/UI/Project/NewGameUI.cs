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

    [SerializeField] private SequelDropdown sequel;

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
        sequel.SetValue(0);
        size.value = 0;
        genre.value = 0;
        SetSize(size.value);
        SetGenre(genre.value);
        SetEngine(null);
        SetTheme(null);
        for (int i = 0; i < platforms.Length; i++)
        {
            SetPlatform(null, i);
        }
    }

    public void SetSequel(int value)
    {
        game.sequelOf = sequel.GetCurrentValue();
        if (game.sequelOf != null) SetSequelInfo();
    }

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
            ProjectUI.instance.OpenProjectCanvas(false);
            ProjectManager.instance.DevelopmentStarted(game);
            ResetInfo();
        }
    }

    private IEnumerator ShowError(int errorId)
    {
        errorText.text = Localization.Localize("startError." + errorId);
        errorText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1); 
        errorText.gameObject.SetActive(false);
    }

    private void SetSequelInfo()
    {
        string newName = game.sequelOf.projectName;
        char last_char = newName[newName.Length - 1];
        int SequelNumber = 2;
        if (last_char == '2') { newName = newName.Remove(newName.Length - 1); SequelNumber = 3; }
        else if (last_char == '3') { newName = newName.Remove(newName.Length - 1); SequelNumber = 4; }
        else if (last_char == '4') { newName = newName.Remove(newName.Length - 1); SequelNumber = 5; }
        else if (last_char == '5') { newName = newName.Remove(newName.Length - 1); SequelNumber = 6; }
        else if (last_char == '6') { newName = newName.Remove(newName.Length - 1); SequelNumber = 7; }
        else if (last_char == '7') { newName = newName.Remove(newName.Length - 1); SequelNumber = 8; }
        else if (last_char == '8') { newName = newName.Remove(newName.Length - 1); SequelNumber = 9; }
        else if (last_char == '9') { newName = newName.Remove(newName.Length - 1); SequelNumber = 10; }
        newName += " ";
        newName += SequelNumber.ToString();
        gameName.text = newName;

        // maybe set genre and theme
    }
}