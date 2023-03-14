using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameScript : MonoBehaviour
{
    [SerializeField] private PersonaModel model;

    [SerializeField] private TMP_InputField companyInput;
    [SerializeField] private TMP_InputField nameInput;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI yearText;

    [SerializeField] private Image maleBtn;
    [SerializeField] private Image femaleBtn;

    [SerializeField] private Color inactiveColor;

    [SerializeField] private Slider yeatSlider;

    private int currentMoneySelector;

    private bool isMale = true;

    private void Start()
    {
        NewGameInfo.instance.year = (int)yeatSlider.value;
        UpdateMoneyInfo();
        RandomModel();
        UpdateGenderBtns();
        UpdateTypeText();
        UpdateYearText();
    }

    public void SetCompanyName(string value)
    {
        NewGameInfo.instance.companyName = value;
    }

    public void SetPersonaName(string value)
    {
        NewGameInfo.instance.personaName = value;
    }

    public void SetType(float value)
    {
        NewGameInfo.instance.personaType = (int)value;
        UpdateTypeText();
    }

    public void SetYear(float value)
    {
        NewGameInfo.instance.year = (int)value;
        UpdateYearText();
    }

    public void SetMoney(bool positive)
    {
        currentMoneySelector += positive ? 1 : -1;
        if (currentMoneySelector < 0) currentMoneySelector = Constans.startMoneys.Length - 1;
        if (currentMoneySelector >= Constans.startMoneys.Length) currentMoneySelector = 0;
        UpdateMoneyInfo();
    }

    public void RandomModel()
    {
        int modelId = 0;
        if (isMale) modelId = Random.Range(0, Constans.maleModels);
        else modelId = Random.Range(Constans.maleModels, Constans.maleModels + Constans.femaleModels);
        model.SetModel(modelId);
        NewGameInfo.instance.personaModel = modelId;
    }

    public void SetGender(bool male)
    {
        isMale = male;
        UpdateGenderBtns();
        RandomModel();
    }

    private void UpdateMoneyInfo()
    {
        NewGameInfo.instance.money = Constans.startMoneys[currentMoneySelector];
        moneyText.text = TextConvertor.moneyText(NewGameInfo.instance.money);
    }

    private void UpdateGenderBtns()
    {
        maleBtn.color = isMale ? Color.black : inactiveColor;
        femaleBtn.color = !isMale ? Color.black : inactiveColor;
    }

    private void UpdateTypeText()
    {
        typeText.text = Localization.Localize("proffesion." + NewGameInfo.instance.personaType);
    }

    private void UpdateYearText()
    {
        yearText.text = NewGameInfo.instance.year.ToString();
    }

    public void RandomCompanyName()
    {
        companyInput.text = Constans.Companies[Random.Range(0, Constans.Companies.Length)];
    }

    public void RandomName()
    {
        nameInput.text = Persona.RandomName(isMale);
    }

    public void NewGame()
    {
        if (string.IsNullOrEmpty(NewGameInfo.instance.companyName)) return;
        if (string.IsNullOrEmpty(NewGameInfo.instance.personaName)) return;

        LoadingScript.instance.LoadScene(2);
    }
}