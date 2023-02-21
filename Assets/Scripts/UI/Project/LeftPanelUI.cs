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
        if (game.genre == null) Debug.Log("null");
        gameGenre.text = Localization.Localize(game.genre.localizationKey);
        gameTheme.text = Localization.Localize(game.theme.localizationKey);
        gameExpenses.text = TextConvertor.moneyText(ProjectManager.instance.GetDevelopingExpenses());

        plot.text = game.plot.ToString();
        gamedesign.text = game.gameDesign.ToString();
        gameplay.text = game.gameplay.ToString();
        graphics.text = game.graphics.ToString();
        sound.text = game.sound.ToString();
        bugs.text = TextConvertor.BugsText(game.bugs);
    }
}