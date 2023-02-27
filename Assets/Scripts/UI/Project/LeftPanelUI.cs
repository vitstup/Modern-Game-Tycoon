using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LeftPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameName;
    [SerializeField] private TextMeshProUGUI gameSize;
    [SerializeField] private TextMeshProUGUI gameGenre;
    [SerializeField] private TextMeshProUGUI gameTheme;

    [SerializeField] private TextMeshProUGUI plot;
    [SerializeField] private TextMeshProUGUI gamedesign;
    [SerializeField] private TextMeshProUGUI gameplay;
    [SerializeField] private TextMeshProUGUI graphics;
    [SerializeField] private TextMeshProUGUI sound;
    [SerializeField] private TextMeshProUGUI bugs;


    [SerializeField] private GameObject expensesPanel;
    [SerializeField] private TextMeshProUGUI gameExpenses;

    [SerializeField] private GameObject gameSalesPanel;
    [SerializeField] private TextMeshProUGUI profitValue;
    [SerializeField] private TextMeshProUGUI profitText;
    [SerializeField] private TextMeshProUGUI salesValue;

    public void SetInfo(GameProject game, bool allInfo = false)
    {
        gameName.text = game.projectName;
        gameSize.text = TextConvertor.GameSizeText(game.size);
        gameGenre.text = Localization.Localize(game.genre.localizationKey);
        gameTheme.text = Localization.Localize(game.theme.localizationKey);

        plot.text = ((int)game.plot).ToString();
        gamedesign.text = ((int)game.gameDesign).ToString();
        gameplay.text = ((int)game.gameplay).ToString();
        graphics.text = ((int)game.graphics).ToString();
        sound.text = ((int)game.sound).ToString();
        bugs.text = TextConvertor.BugsText(game.bugs);

        SetSalesInfo(game, allInfo);
    }

    private void SetSalesInfo(GameProject game, bool allInfo)
    {
        expensesPanel.SetActive(!allInfo);
        gameSalesPanel.SetActive(allInfo);

        var info = game as Game;
        if (!allInfo) gameExpenses.text = TextConvertor.moneyText(game.expenses);
        else
        {
            salesValue.text = TextConvertor.numText(info.TotalSales());
            if (info.TotalProfit() > 0)
            {
                profitText.text = Localization.Localize("netprofit");
                profitValue.text = TextConvertor.ChangeColor(TextConvertor.moneyText(info.TotalProfit()), Constans.GreenColor);
            }
            else
            {
                profitText.text = Localization.Localize("expenses");
                profitValue.text = TextConvertor.ChangeColor(TextConvertor.moneyText(info.TotalProfit()), Color.red);
            }
        }
    }
}