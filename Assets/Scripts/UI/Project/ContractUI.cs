using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContractUI : MonoBehaviour
{
    [SerializeField] private GameObject ContractPanel;

    [SerializeField] private TextMeshProUGUI contractName;

    [SerializeField] private TextMeshProUGUI size;
    [SerializeField] private TextMeshProUGUI genre;
    [SerializeField] private TextMeshProUGUI engine;
    [SerializeField] private TextMeshProUGUI theme;

    [SerializeField] private PlatformSelector[] platforms;

    [SerializeField] private TextMeshProUGUI payment;
    [SerializeField] private TextMeshProUGUI bonus;
    [SerializeField] private TextMeshProUGUI minScore;
    [SerializeField] private TextMeshProUGUI term;

    [SerializeField] private TextMeshProUGUI errorText;

    private Contract contract;

    private void Awake()
    {
        EnginePanel.SelectedEngine.AddListener(EngineSelected);
        ThemePanel.SelectedTheme.AddListener(ThemeSelected);
    }

    private void Start()
    {
        NewContract();
    }

    public void NewContract()
    {
        int maxSize = Random.Range(0, ProjectManager.instance.maxSizeGameCreated + 1);
        contract = new Contract(maxSize);
        contractName.text = contract.projectName;
        size.text = TextConvertor.GameSizeText(contract.size);
        genre.text = Localization.Localize(contract.genre.localizationKey);
        engine.text = Localization.Localize("none");
        theme.text = Localization.Localize("none");


        if (contract.platforms.Length > 4) { Debug.LogError("Too much platforms"); }
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].SetInfo(contract.platforms[i]);
        }

        payment.text = Localization.Localize("payment") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(contract.payment), Constans.GreenColor);
        bonus.text = Localization.Localize("bonus") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(contract.bonus), Constans.GreenColor);
        minScore.text = Localization.Localize("minuserscore") + ": " + TextConvertor.ChangeColor(TextConvertor.percentText(contract.minScore), Constans.GreenColor);
        term.text = Localization.Localize("term") + ": " + TextConvertor.ChangeColor(contract.term + " " + Localization.Localize("days"), Constans.GreenColor);
    }

    private void EngineSelected(Engine engine)
    {
        if (RedactingThisPanel())
        {
            contract.engine = engine;
            this.engine.text = engine.info.name;
        }
    }

    private void ThemeSelected(ThemeInfo theme)
    {
        if (RedactingThisPanel())
        {
            contract.theme = theme;
            this.theme.text = Localization.Localize(theme.localizationKey);
        }
    }

    public void SelectEngine()
    {
        AttributesUI.instance.OpenEngines(contract.engine);
    }

    public void SelectTheme()
    {
        AttributesUI.instance.OpenThemes();
    }

    public void StartDeveloping()
    {
        if (contract.engine == null) StartCoroutine(ShowError(1));
        else if (contract.theme == null) StartCoroutine(ShowError(2));
        else
        {
            ProjectUI.instance.OpenProjectCanvas(false);
            ProjectManager.instance.DevelopmentStarted(contract);
            NewContract();
        }
    }

    private bool RedactingThisPanel()
    {
        return ContractPanel.activeSelf;
    }

    private IEnumerator ShowError(int errorId)
    {
        errorText.text = Localization.Localize("startError." + errorId);
        errorText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        errorText.gameObject.SetActive(false);
    }

}