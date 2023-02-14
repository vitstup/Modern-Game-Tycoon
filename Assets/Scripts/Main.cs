using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;

    [field: SerializeField] public long money { get; private set; }

    private long monthBalance;

    private void Awake()
    {
        instance = this;
        TimeManager.MonthUpdateEvent.AddListener(MonthPassed);
    }

    private void Start()
    {
        MainUI.instance.ChangeMoneyText();
    }

    public void AddMoney(int money)
    {
        this.money += money;
        monthBalance += money;
        MainUI.instance.ChangeMoneyText();
    }

    public void MinusMoney(int money)
    {
        this.money -= money;
        monthBalance -= money;
        MainUI.instance.ChangeMoneyText();
    }

    private void MonthPassed()
    {
        MainUI.instance.ChangeMoneyTriangle(monthBalance >= 0);
        monthBalance = 0;
    }
}