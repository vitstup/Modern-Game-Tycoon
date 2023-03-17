using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingManager : MonoBehaviour
{
    public static MarketingManager instance;

    [SerializeField] private MarketingCampaign[] campaigns;

    private void Awake()
    {
        instance = this;
    }

    public MarketingCampaign[] GetCampaigns()
    {
        return campaigns;
    }

    public void StartCampaign(MarketingCampaign campaign, Game game)
    {
        game.hype += campaign.GetAvailableBonus(game);
        game.daysTillMarketingCampaign = 90;
        Main.instance.MinusMoney(campaign.GetPrice(game));

        AchievementsManager.instance.SetAchievment(8);
    }

    public void HypeDecrease(Game game)
    {
        game.hype -= Constans.hypeDecrease;
        if (game.hype < 0 ) game.hype = 0;

        game.daysTillMarketingCampaign--;
        // may add check for zero, but it's useless
    }
}