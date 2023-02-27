using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsUI : MonoBehaviour
{
    [SerializeField] private GameObject statisticsCanvas;

    [SerializeField] private Image gameImage;

    [SerializeField] private FeatureGroupPanel[] prototypingGroups;
    [SerializeField] private FeatureGroupPanel[] developingGroups;
    [SerializeField] private FeatureGroupPanel[] designGroups;

    [SerializeField] private TextMeshProUGUI reviewsText;
    [SerializeField] private TextMeshProUGUI ratingText;

    [SerializeField] private TextMeshProUGUI gamesCountText;

    [SerializeField] private LeftPanelUI leftPanel;
    [SerializeField] private RightPanelUI rightPanel;

    private int currentGame;

    public void TryToOpenStatistics()
    {
        if (Statistics.instance.games.Count > 0) OpenStatistics();
    }

    private void OpenStatistics()
    {
        currentGame = 0;
        statisticsCanvas.SetActive(true);
        TimeManager.instance.NecessaryPause(true);
        Updatetatistics();
    }

    private void Updatetatistics()
    {
        UpdateInfo(Statistics.instance.games[currentGame]);
    }

    public void ChangeCurrentGame(bool positive)
    {
        currentGame += positive ? 1 : -1;
        if (currentGame < 0) currentGame = Statistics.instance.games.Count - 1;
        if (currentGame >= Statistics.instance.games.Count) currentGame = 0;
        Updatetatistics();
    }

    private void UpdateInfo(Game game)
    {
        gameImage.sprite = game.sprite;

        UpdateRevies(game);

        ratingText.text = Localization.Localize("userrating") + ": " + TextConvertor.ChangeColor(TextConvertor.percentText(game.reviews.UserScore()), Constans.GreenColor);

        gamesCountText.text = (currentGame + 1) + " / " + Statistics.instance.games.Count;

        UpdateFeatures(game);

        leftPanel.SetInfo(game, true);

        int[] incomes = new int[game.sales.Length];
        for (int i = 0; i < incomes.Length; i++)
        {
            incomes[i] = game.sales[i].profit;
        }
        rightPanel.SetInfo(game, incomes);
    }

    private void UpdateRevies(Game game)
    {
        string positive = TextConvertor.ChangeColor(game.reviews.positive.ToString(), Constans.GreenColor);
        string negative = TextConvertor.ChangeColor(game.reviews.negative.ToString(), Color.red);

        reviewsText.text = Localization.Localize("reviews") + ": " + positive + TextConvertor.ChangeColor(" / ", Color.white) + negative;
    }

    private void UpdateFeatures(Game game)
    {
        var prototyping = game.GetPrototypingFeaturesInfo();
        var developing = game.GetDevelopingFeaturesInfo();
        var design = game.GetDesignFeaturesInfo();

        for (int i = 0; i < 5; i++)
        {
            prototypingGroups[i].SetInfo(prototyping[i, 0], prototyping[i, 1]);
            developingGroups[i].SetInfo(developing[i, 0], developing[i, 1]);
            designGroups[i].SetInfo(design[i, 0], design[i, 1]);
        }
    }
}