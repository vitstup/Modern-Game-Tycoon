using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public static UnityEvent DayUpdateEvent = new UnityEvent();
    public static UnityEvent MonthUpdateEvent = new UnityEvent();
    public static UnityEvent YearUpdateEvent = new UnityEvent();

    [field: SerializeField] public int day { get; private set; }
    [field: SerializeField] public int month { get; private set; }
    [field: SerializeField] public int year { get; private set; }

    [HideInInspector] public TimeSpeed timeSpeed { get; private set; }
    [HideInInspector] public RunStatus runStatus { get; private set; }

    public KeyCode pauseButton;

    private float seconds;

    private void Awake()
    {
        instance = this;
        SetDate(0,0,2000);
        ChangeSpeed(1);
    }

    private void SetDate(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;
        MainUI.instance.UpdateTimePanel();
    }

    private void Update()
    {
        if (runStatus != RunStatus.standart) return;
        seconds += Time.deltaTime;
        if (seconds > 0.5f)
        {
            seconds = 0;
            day++;
            DayUpdateEvent?.Invoke();
        }
        if (day >= Constans.DaysInMonth[month])
        {
            day = 0;
            month++;
            MonthUpdateEvent?.Invoke();
        }
        if (month >= 12)
        {
            month = 0;
            year++;
            YearUpdateEvent?.Invoke();
        }
        if (Input.GetKeyDown(pauseButton) && runStatus == RunStatus.standart)
        {
            Debug.Log(timeSpeed);
            if (timeSpeed != TimeSpeed.Pause) ChangeSpeed(0); 
            else ChangeSpeed(1); 
        }
        MainUI.instance.UpdateDate();
    }

    public void ChangeSpeed(int speed)
    {
        timeSpeed = (TimeSpeed)speed;
        if (timeSpeed == TimeSpeed.X1) Time.timeScale = 1;
        else if (timeSpeed == TimeSpeed.X2) Time.timeScale = 2;
        else Time.timeScale = 0;
        MainUI.instance.UpdateSpeedBtns();
    }

    public void ChangeRunStatus(RunStatus status)
    {
        runStatus = status;
        MainUI.instance.UpdateSpeedBtns();
    }

    public void NecessaryPause(bool pause)
    {
        ChangeRunStatus(pause ? RunStatus.stoped : RunStatus.standart);
    }

    public int GetMonthsFromStart()
    {
        return (year - 2000) * 12 + month;
    }

    public int GetEndDateMonths()
    {
        return (Constans.EndYear - 2000) * 12;
    }

    public void SetYear(int year)
    {
        this.year = year;
    }
}