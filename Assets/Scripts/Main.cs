using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;

    [field: SerializeField] public long money { get; private set; }

    private void Awake() => instance = this;

    public void AddMoney(int money)
    {
        this.money += money;
    }

    public void MinusMoney(int money)
    {
        this.money -= money;
    }
}