using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public static UnityEvent DayUpdateEvent = new UnityEvent();
    public static UnityEvent MonthUpdateEvent = new UnityEvent();
    public static UnityEvent YearUpdateEvent = new UnityEvent();

    public int day { get; private set; }
    public int month { get; private set; }
    public int year { get; private set; }

    private int[] daysinmonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    private TimeSpeed timeSpeed;
    [HideInInspector]public RunStatus runStatus { get; private set; }

    public KeyCode pauseButton;

    private float seconds;

    [SerializeField] private GameObject BackPanel;

    private void Awake()
    {
        instance = this;
        SetDate(0,0,2000);
    }

    private void SetDate(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;
    }

    private void Update()
    {
        if (runStatus != RunStatus.standart) return;
        seconds += Time.deltaTime;
        if(seconds > 0.5f)
        {
            seconds = 0;
            day++;
            DayUpdateEvent?.Invoke();
        }
        if(day >= daysinmonth[month])
        {
            day = 0;
            month++;
            MonthUpdateEvent?.Invoke();
        }
        if(month >= 12)
        {
            month = 0;
            year++;
            YearUpdateEvent?.Invoke();
        }
    }

    public void ChangeSpeed(int speed)
    {
        timeSpeed = (TimeSpeed)speed;
        if (timeSpeed == TimeSpeed.X1) Time.timeScale = 1;
        else if (timeSpeed == TimeSpeed.X2) Time.timeScale = 2;
        else Time.timeScale = 0;
    }

    public void ChangeRunStatus(RunStatus status)
    {
        runStatus = status;
        if(status == RunStatus.stoped) BackPanel.SetActive(true);
        else BackPanel.SetActive(false);
    }
}