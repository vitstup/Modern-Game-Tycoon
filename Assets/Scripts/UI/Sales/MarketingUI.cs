using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketingUI : MonoBehaviour
{
    public static MarketingUI instance;

    [SerializeField] private GameObject marketingCanvas;

    [SerializeField] private TextMeshProUGUI gameNameText;

    [SerializeField] private MarketingCampaignPanel[] panels;

    private void Awake()
    {
        instance = this;
    }

    public void OpenMarketing(Game game)
    {
        marketingCanvas.SetActive(true);

        TimeManager.instance.NecessaryPause(true);

        gameNameText.text = game.projectName;

        UpdatePanels(game);
    }

    private void UpdatePanels(Game game)
    {
        var campaigns = MarketingManager.instance.GetCampaigns();
        if (campaigns.Length != panels.Length) Debug.LogError("Different amount of marketing campaigns and their ui panels");
        for (int i = 0; i < campaigns.Length; i++)
        {
            panels[i].SetInfo(campaigns[i], game);
        }
    }

    public void CloseMarketing()
    {
        marketingCanvas.SetActive(false);

        TimeManager.instance.NecessaryPause(false);
    }
}