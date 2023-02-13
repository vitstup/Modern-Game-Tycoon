using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameUI : MonoBehaviour
{
    public static NewGameUI instance;

    [SerializeField] private TMP_InputField gameName;

    [SerializeField] private TextMeshProUGUI engine;
    [SerializeField] private TextMeshProUGUI theme;
    [SerializeField] private TMP_Dropdown size;
    [SerializeField] private TMP_Dropdown genre;

    [SerializeField] private TMP_Dropdown sequel;

    [SerializeField] private PlatformSelector[] platforms;

    private Game game;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializeSizeDropdown();
        InitializeGenreDropdown();
        ResetInfo();
    }

    private void InitializeSizeDropdown()
    {
        size.ClearOptions();
        var sizes = Constans.gameSizes;
        for (int i = 0; i < sizes.Length; i++)
        {
            size.options.Add(new TMP_Dropdown.OptionData() { text = sizes[i] }); 
        }
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
    }
    public void ResetInfo()
    {
        game = new Game();
        size.value = 0;
        genre.value = 0;
        // engine
        // theme
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].SetInfo(null);
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
}