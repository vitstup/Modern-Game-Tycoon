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
        OfficesUI.instance.SetOfficePanels(offices);
    }


    public Office GetCurrentOffice()
    {
        return offices[currentOffice];
    }

    private void SetOfficeObj()
    {
        for (int i = 0; i < offices.Length; i++)
        {
            if (i == currentOffice) { offices[i].officeObj.SetActive(true); continue; }
            offices[i].officeObj.SetActive(false);
        }
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
}