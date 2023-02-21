using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyPlatformUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI platformName;
    [SerializeField] private TextMeshProUGUI computingValue;
    [SerializeField] private TextMeshProUGUI graphicsValue;
    [SerializeField] private TextMeshProUGUI valueText;

    [SerializeField] private Image platformImage;

    public void SetInfo(Platform platform, GameProject game)
    {
        SetBase(platform, game);

        float pointsEff = platform.GetPointsPenalty(game.GetComputingUsage(), game.GetGraphicsUsage());
        valueText.text = Localization.Localize("pointseff") + " " + TextConvertor.ChangeColor(TextConvertor.percentText(pointsEff), pointsEff < 1? Color.red : Constans.GreenColor);
    }

    public void SetInfo(Platform platform, GameProject game, int income)
    {
        SetBase(platform, game);

        valueText.text = Localization.Localize("income") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(income), Constans.GreenColor);
    }

    private void SetBase(Platform platform, GameProject game)
    {
        platformName.text = platform.info.name;

        int computeUsage = game.GetComputingUsage();
        int compute = platform.info.computeCapabilities;

        int graphicsUsage = game.GetGraphicsUsage();
        int graphics = platform.info.graphicCapabilities;

        computingValue.text = computeUsage + " / " + compute;
        graphicsValue.text = graphicsUsage + " / " + graphics;

        computingValue.color = computeUsage > compute ? Color.red : Color.black;
        graphicsValue.color = graphicsUsage > graphics ? Color.red : Color.black;

        platformImage.sprite = platform.info.sprite;
    }
}