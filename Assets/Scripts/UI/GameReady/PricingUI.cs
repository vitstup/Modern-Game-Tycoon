using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PricingUI : MonoBehaviour
{
    public static PricingUI instance;

    [SerializeField] private GameObject pricingPanel;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Slider priceSlider;
    [SerializeField] private TextMeshProUGUI correctOfPricingText;

    private void Awake() => instance = this;

    public void OpenPricing()
    {
        pricingPanel.SetActive(true);
        priceSlider.value = 20;
        (ProjectManager.instance.project as Game).price = 20;
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        Game game = ProjectManager.instance.project as Game;
        priceText.text = Localization.Localize("gameprice") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText((int)game.price), Constans.GreenColor);
        correctOfPricingText.text = Localization.Localize("corofpricing") + " " + TextConvertor.ChangeColor(TextConvertor.corrOfPricText(game), Constans.GreenColor);
    } 

    public void SetPrice(float value)
    {
        (ProjectManager.instance.project as Game).price = value;
        UpdateTexts();
    } 

    public void Release()
    {
        pricingPanel.SetActive(false);
        GameReadyUI.instance.Release();
    }
}