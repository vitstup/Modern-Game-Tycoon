using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MarketingCampaignPanel : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image Bg;
    [SerializeField] private Sprite activeBg;
    [SerializeField] private Sprite inactiveBg;


    [SerializeField] private Image campaignImage;
    [SerializeField] private TextMeshProUGUI campaignName;
    [SerializeField] private TextMeshProUGUI campaignPrice;
    [SerializeField] private TextMeshProUGUI campaignBonus;

    [SerializeField] private GameObject lockedPanel;


    private Game selectedGame;
    private MarketingCampaign campaign;


    public void SetInfo(MarketingCampaign campaign, Game game)
    {
        campaignImage.sprite = campaign.sprite;
        campaignName.text = Localization.Localize(campaign.localizationKey);
        campaignPrice.text = TextConvertor.moneyText(campaign.GetPrice(game));

        selectedGame = game;
        this.campaign = campaign;

        SetBonusText();
        CheckForLock();
    }

    private void SetBonusText()
    {
        int bonus = (int)(campaign.hypeBonus * 100f);
        int maxBonus = (int)(campaign.maxHype * 100f);

        campaignBonus.text = "+" + bonus + "(" + Localization.Localize("max") + ". " + maxBonus + ")";
    }

    private void CheckForLock()
    {
        if (selectedGame.daysTillMarketingCampaign > 0 || campaign.GetAvailableBonus(selectedGame) < 0.01f) lockedPanel.SetActive(true); 
        else lockedPanel.SetActive(false);
    }

    public void SelectCampaign()
    {
        if (lockedPanel.activeSelf) return;
        MarketingManager.instance.StartCampaign(campaign, selectedGame);
        MarketingUI.instance.CloseMarketing();
    }


    private void OnHover()
    {
        Bg.sprite = activeBg;
    }

    public void NotOnHover()
    {
        Bg.sprite = inactiveBg;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        NotOnHover();
    }
}