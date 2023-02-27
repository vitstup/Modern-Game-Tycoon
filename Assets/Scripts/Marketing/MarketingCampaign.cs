using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MarketingCampaign 
{
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public string localizationKey { get; private set; }
    [field: SerializeField] public int basePrice { get; private set; }
    [field: SerializeField] public float hypeBonus { get; private set; }
    [field: SerializeField] public float maxHype { get; private set; }

    public int GetPrice(Game game)
    {
        return basePrice * Constans.sizesScale[game.size];
    }

    public float GetAvailableBonus(Game game)
    {
        float availableHype = maxHype - game.hype;
        if (availableHype > 0f)
        {
            float bonus = hypeBonus;
            if (availableHype < bonus) bonus = availableHype;
            return bonus;
        }
        return 0f;
    }
}