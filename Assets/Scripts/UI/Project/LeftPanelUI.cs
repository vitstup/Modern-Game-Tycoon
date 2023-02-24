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
    [SerializeField] private TextMeshProUGUI gameExpenses;

    [SerializeField] private TextMeshProUGUI plot;
    [SerializeField] private TextMeshProUGUI gamedesign;
    [SerializeField] private TextMeshProUGUI gameplay;
    [SerializeField] private TextMeshProUGUI graphics;
    [SerializeField] private TextMeshProUGUI sound;
    [SerializeField] private TextMeshProUGUI bugs;

    public void SetInfo(GameProject game)
    {
        gameName.text = game.projectName;
        gameSize.text = TextConvertor.GameSizeText(game.size);
        gameGenre.text = Localization.Localize(game.genre.localizationKey);
        gameTheme.text = Localization.Localize(game.theme.localizationKey);
        gameExpenses.text = TextConvertor.moneyText(game.expenses);

        plot.text = ((int)game.plot).ToString();
        gamedesign.text = ((int)game.gameDesign).ToString();
        gameplay.text = ((int)game.gameplay).ToString();
        graphics.text = ((int)game.graphics).ToString();
        sound.text = ((int)game.sound).ToString();
        bugs.text = TextConvertor.BugsText(game.bugs);
    }
}