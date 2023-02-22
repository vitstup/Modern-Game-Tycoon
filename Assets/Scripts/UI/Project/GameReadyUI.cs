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

    private void Awake() => instance = this;

    public void OpenGameReady(GameProject game)
    {
        TimeManager.instance.NecessaryPause(true);
        gameReadyCanvas.SetActive(true);
        SetInfo(game);
    }

    private void SetInfo(GameProject game)
    {
        // img
        // userrating

        leftPanel.SetInfo(game);

        rightPanel.SetInfo(game);
    }

    public void Polish()
    {
        (ProjectManager.instance.project as GameProject).SetEfficiency();
    }

    public void FindPublisher()
    {

    }

    public void Release()
    {

    }
}