using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    public static WinLoseUI instance;

    [SerializeField] private GameObject eventsCanvas;
    [SerializeField] private TextMeshProUGUI theme;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI btnText;

    private State state;

    private void Awake()
    {
        instance = this;
    }

    public enum State
    {
        warning,
        lose,
        win
    }

    public void OpenCanvas(State state)
    {
        eventsCanvas.SetActive(true);
        TimeManager.instance.NecessaryPause(true);

        this.state = state;
    }

    private void SetWarningInfo()
    {
        theme.text = Localization.Localize("wltheme.0");
        text.text = Localization.Localize("wl.0");
        btnText.text = Localization.Localize("continue");
    }

    private void SetLoseInfo()
    {
        theme.text = Localization.Localize("wltheme.1");
        text.text = Localization.Localize("wl.1");
        btnText.text = Localization.Localize("menu");
    }

    private void SetWinInfo()
    {
        theme.text = Localization.Localize("wltheme.2");
        text.text = Localization.Localize("wl.2");
        btnText.text = Localization.Localize("continue");
    }

    public void Interacte()
    {
        if (state != State.lose)
        {
            eventsCanvas.SetActive(false);
            TimeManager.instance.NecessaryPause(false);
        }
        else
        {
            // to main menu
        }
    }
}