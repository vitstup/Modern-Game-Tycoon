using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameInfo : MonoBehaviour
{
    public static NewGameInfo instance;

    public string companyName;

    public string personaName;

    public int personaType;

    public int personaModel;

    public int year;

    public int money;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2) SetInfo();
        else ResetInfo();
    }

    private void SetInfo()
    {
        CompanyStats.instance.SetCompanyName(companyName);
        TimeManager.instance.SetYear(year);
        Main.instance.SetSumm(money);

        Skills skills = new Skills(personaType, Constans.minSkill);

        Persona persona = new Persona(personaName, skills, 400, personaModel);

        RosterManager.instance.hiredWorkers.Add(persona);

        BuildingManager.instance.AutoFurniture(false);

        var tables = FindObjectsOfType<Table>();
        if (tables == null || tables.Length == 0 || tables.Length > 1) Debug.Log("something wrong with start table");
        RosterManager.instance.selectedTable = tables[0];
        RosterManager.instance.AssignWorker(persona);
    }

    private void ResetInfo()
    {
        companyName = "";
        personaName = "";
        personaType = 0;
        // dont update model, year and money info 
    }
}