using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class MainUI : MonoBehaviour
{
    public static MainUI instance;

    [SerializeField] private TextMeshProUGUI dateText;

    [SerializeField] private Image PauseBtn;
    [SerializeField] private Image X1Btn;
    [SerializeField] private Image X2Btn;

    [SerializeField] private Sprite PauseInactive;    [SerializeField] private Sprite PauseActive;

    [SerializeField] private Sprite PlayActive;
    [SerializeField] private Sprite PlayInactive;

    [SerializeField] private GameObject BackPanel;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateDate()
    {
        StringBuilder curDate = new StringBuilder();
        curDate.Append(TimeManager.instance.day + 1 + " ");
        string key = "month." + (TimeManager.instance.month + 1);
        curDate.Append(Localization.Localize(key) + " ");
        curDate.Append(TimeManager.instance.year);
        dateText.text = curDate.ToString();
    }

    public void UpdateSpeedBtns()
    {
        bool paused = TimeManager.instance.runStatus != RunStatus.standart || TimeManager.instance.timeSpeed == TimeSpeed.Pause;
        if (paused)
        {
            PauseBtn.sprite = PauseActive;
            X1Btn.sprite = PlayInactive;
            X2Btn.sprite = PlayInactive;
        }
        else if(!paused && TimeManager.instance.timeSpeed == TimeSpeed.X1)
        {
            PauseBtn.sprite = PauseInactive;
            X1Btn.sprite = PlayActive;
            X2Btn.sprite = PlayInactive;
        }
        else if (!paused && TimeManager.instance.timeSpeed == TimeSpeed.X2)
        {
            PauseBtn.sprite = PauseInactive;
            X1Btn.sprite = PlayActive;
            X2Btn.sprite = PlayActive;
        }

        if (TimeManager.instance.runStatus == RunStatus.stoped) BackPanel.SetActive(true);
        else BackPanel.SetActive(false);
    }

    public void UpdateTimePanel()
    {
        UpdateDate();
        UpdateSpeedBtns();
    }
}