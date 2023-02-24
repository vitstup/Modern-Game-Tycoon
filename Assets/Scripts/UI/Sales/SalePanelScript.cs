using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SalePanelScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameName;
    [SerializeField] private TextMeshProUGUI todaySalesValue;
    [SerializeField] private TextMeshProUGUI todayProfitValue;

    [SerializeField] private Image[] SaleGraphs;

    [SerializeField] private TextMeshProUGUI bugsText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hypeText;

    [SerializeField] private TextMeshProUGUI profitText;

    private Game game;

    public void SetInfo(Game game)
    {
        gameName.text = game.projectName;
        todaySalesValue.text = TextConvertor.numText(game.todaySales);
        todayProfitValue.text = TextConvertor.moneyText(game.todayProfit);

        UpdateGraphic(game);

        bugsText.text = TextConvertor.BugsText(game.bugs);
        scoreText.text = TextConvertor.percentText(game.reviews.UserScore());
        hypeText.text = TextConvertor.percentText(game.hype);

        string profitString = TextConvertor.moneyText(game.TotalProfit());
        if (game.TotalProfit() > 0) profitText.text = Localization.Localize("netprofit") + ": " + TextConvertor.ChangeColor(profitString, Constans.GreenColor);
        else profitText.text = Localization.Localize("expenses") + ": " + TextConvertor.ChangeColor(profitString, Color.red);

        this.game = game;
    }

    private void UpdateGraphic(Game game)
    {
        if (SaleGraphs.Length != game.recentProfits.Length) Debug.LogError("Different amount of graphs and game.recentProfits");
        for (int i = 0; i < SaleGraphs.Length; i++)
        {
            SaleGraphs[i].fillAmount = game.recentProfits[i] / (game.firstDaySales * 2f);
        }
    }


    public void OpenMarketing()
    {

    }
}