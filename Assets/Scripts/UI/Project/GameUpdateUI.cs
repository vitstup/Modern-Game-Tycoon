using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUpdateUI : MonoBehaviour
{
    public static GameUpdateUI instance;

    [SerializeField] private GameObject updatePanel;

    [SerializeField] private TextMeshProUGUI gameText;
    [SerializeField] private SequelDropdown gameDropdown;
    [SerializeField] private TMP_Dropdown sizeDropdown;

    [SerializeField] private PlatformSelector[] platforms;

    [SerializeField] private TextMeshProUGUI errorText;

    private int openedPlatformSelectorId;

    private GameUpdate gameUpdate;

    private void Awake()
    {
        instance = this;
        PlatformPanel.SelectedPlatform.AddListener(PlatformSelected);
    }

    private void Start()
    {
        ResetInfo();
    }

    private void ResetInfo()
    {
        gameUpdate = new GameUpdate();
        sizeDropdown.value = 0;
        gameDropdown.SetValue(0);
        gameUpdate.updateSize = 0;
        gameUpdate.updateGame = null;
        UpdateInfo();
    }

    public void SetSize(int value)
    {
        gameUpdate.updateSize = value;
    }

    public void SetGame(int value)
    {
        gameUpdate.updateGame = gameDropdown.GetCurrentValue();
        gameUpdate.platformsAdded = new Platform[4]; 
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        Debug.Log("Updating info");
        gameText.text = gameUpdate.updateGame != null ? gameUpdate.updateGame.projectName : Localization.Localize("gamenotselected");

        var used = gameUpdate.GetUsedPlatforms();
        for (int i = 0; i < platforms.Length; i++)
        {
            bool changebale = true;
            if (gameUpdate.updateGame != null) {changebale = gameUpdate.updateGame.platforms[i] == null; Debug.LogWarning("D"); }
            platforms[i].SetInfo(used[i], changebale);
        }
    }

    private void SetPlatform(Platform platform, int platformSelector)
    {
        gameUpdate.platformsAdded[platformSelector] = platform;
        UpdateInfo();
    }

    private void PlatformSelected(Platform platform)
    {
        if (RedactingThisPanel())
        {
            SetPlatform(platform, openedPlatformSelectorId);
        }
    }

    private bool RedactingThisPanel()
    {
        return updatePanel.activeSelf;
    }

    public void SelectPlatform(int id)
    {
        openedPlatformSelectorId = id;
        AttributesUI.instance.OpenPlatforms(gameUpdate.GetUsedPlatforms());
    }

    public void UnselectPlatform(int id)
    {
        SetPlatform(null, id);
    }

    public void StartDevelopment()
    {
        if (gameUpdate.updateGame == null) { StartCoroutine(ShowError()); return; }
        else
        {
            ProjectUI.instance.OpenProjectCanvas(false);
            ProjectManager.instance.DevelopmentStarted(gameUpdate);
            ResetInfo();
        }
    }

    private IEnumerator ShowError()
    {
        errorText.text = Localization.Localize("chooseGame");
        errorText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        errorText.gameObject.SetActive(false);
    }

}