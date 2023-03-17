using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public static OfficeManager instance;

    [field: SerializeField] public Office[] offices { get; private set; }
    public int currentOffice { get; private set; } = 0;


    private void Awake()
    {
        instance = this;
        SetOfficeObj();
        TimeManager.MonthUpdateEvent.AddListener(TakeRent);
    }

    private void Start() => OfficesUI.instance.SetOfficePanels(offices);

    public Office GetCurrentOffice()
    {
        return offices[currentOffice];
    }

    public void SetOfficeObj(bool deleteItems = true)
    {
        for (int i = 0; i < offices.Length; i++)
        {
            if (i == currentOffice) { offices[i].officeObj.SetActive(true); continue; }
            offices[i].officeObj.SetActive(false);
        }
        if (deleteItems) Helpers.DeleteChilds(BuildingManager.instance.itemsParent);
    }

    public void ChangeCurrentOffice(Office office)
    {
        bool changed = false;
        for (int i = 0; i < offices.Length; i++)
        {
            if (office == offices[i]) { currentOffice = i; changed = true; break; }
        }
        if(!changed) Debug.LogError("Wrong selected office");
        else SetOfficeObj();
    }

    private void TakeRent()
    {
        for (int i = 0; i < offices.Length; i++)
        {
            if (offices[i].state == OfficeState.rented) Main.instance.MinusMoney(offices[i].rentPrice);
        }
    }

    public bool OwnAllOffices()
    {
        bool own = true;
        for (int i = 0; i < offices.Length; i++)
        {
            if (offices[i].state != OfficeState.bought) { own = false; break; }
        }
        return own;
    }
}