using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager instance;

    [field: SerializeField] public Computer[] computers { get; private set; }

    private void Awake()
    {
        instance = this;
        TimeManager.YearUpdateEvent.AddListener(CheckForPcUpdates);
    }

    public int GetModernPcId()
    {
        for (int i = computers.Length - 1; i >= 0; i--)
        {
            if (computers[i].unlockYear <= TimeManager.instance.year) return i;
        }
        Debug.LogError("Something wrong with finfding most modern pc");
        return 0;
    }

    public void CheckForPcUpdates()
    {
        int modernPc = GetModernPcId();
        var buildings = BuildingManager.instance.buildings;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i] != null && buildings[i] is Table)
            {
                var table = buildings[i] as Table;
                if (table.currentPc < modernPc) table.SetPcBtn(true);
            }
        }
    }

    public void UpgradeAllPcs()
    {
        int modernPc = GetModernPcId();
        var buildings = BuildingManager.instance.buildings;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i] != null && buildings[i] is Table)
            {
                var table = buildings[i] as Table;
                if (table.currentPc < modernPc) table.UpgradePc();
            }
        }
    }
}