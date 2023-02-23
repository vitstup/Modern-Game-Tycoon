using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameReadyUI : MonoBehaviour
{
    public static GameReadyUI instance;

    [SerializeField] private GameObject gameReadyCanvas;

    [SerializeField] private Image gameImg;

    [SerializeField] private TextMeshProUGUI estUserRating;

    [SerializeField] private LeftPanelUI leftPanel;

    [SerializeField] private RightPanelUI rightPanel;

    [SerializeField] private GameObject gameBtns;
    [SerializeField] private GameObject contractBtns;

    private void Awake() => instance = this;

    public void CloseCanvas()
    {
        gameReadyCanvas.SetActive(false);
        TimeManager.instance.NecessaryPause(false);
    }

    public void OpenGameReady(GameProject game)
    {
        TimeManager.instance.NecessaryPause(true);
        gameReadyCanvas.SetActive(true);
        SetInfo(game);
    }

    private void SetInfo(GameProject game)
    {
        gameImg.sprite = game.sprite;
        string userScore = TextConvertor.ReviewChanceText(ReviewChance.PositiveReviewChance(game)[0]);
        estUserRating.text = Localization.Localize("estuserrating") + " " + TextConvertor.ChangeColor(userScore, Constans.GreenColor);

        leftPanel.SetInfo(game);

        rightPanel.SetInfo(game);

        if (game is Game) { gameBtns.SetActive(true); contractBtns.SetActive(false); }
        else if (game is Contract) { gameBtns.SetActive(false); contractBtns.SetActive(true); }
        else Debug.LogError("Not game or contract");
    }

    public void Polish()
    {
        (ProjectManager.instance.project as GameProject).SetEfficiency();
    }

    public void FindPublisher()
    {
        PublishersUI.instance.OpenPublishersPanel(ProjectManager.instance.project as Game);
    }

    public void OpenRelease()
    {
        PricingUI.instance.OpenPricing();
    }

    public void Release()
    {
        CloseCanvas();
        ProjectManager.instance.DoneDevelopment();
    }
}