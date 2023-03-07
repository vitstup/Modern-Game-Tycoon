using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;

    [field: SerializeField] public long money { get; private set; }
    
    private long monthInocme;
    private long monthExpenses;

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
        monthInocme += money;
        MainUI.instance.ChangeMoneyText();
    }

    public void MinusMoney(int money)
    {
        this.money -= money;
        monthExpenses += money;
        MainUI.instance.ChangeMoneyText();
    }

    private void MonthPassed()
    {
        MailManager.instance.NewMail(new MonthBalanceMail("OnAccService", monthInocme, monthExpenses));
        MainUI.instance.ChangeMoneyTriangle(monthInocme >= monthExpenses);
        monthInocme = 0;
        monthExpenses = 0;
    }
}