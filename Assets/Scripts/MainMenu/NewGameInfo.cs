using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameInfo : MonoBehaviour
{
    public static NewGameInfo instance;

    [HideInInspector] public bool loadingSave;

    public string companyName;

    public string personaName;

    public int personaType;

    public int personaModel;

    public int year;

    public int money;

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            if (loadingSave) SaveLoad.SaveLoadManager.instance.Load();
            else SetInfo();
        }
        else ResetInfo();
    }

    private void SetInfo()
    {
        CompanyStats.instance.SetCompanyName(companyName);
        TimeManager.instance.SetYear(year);

        Skills skills = new Skills(personaType, Constans.minSkill);

        Persona persona = new Persona(personaName, skills, 400, personaModel);

        RosterManager.instance.hiredWorkers.Add(persona);

        BuildingManager.instance.AutoFurniture(false);

        var tables = FindObjectsOfType<Table>();
        if (tables == null || tables.Length == 0 || tables.Length > 1) Debug.LogError("something wrong with start table");
        tables[0].UpgradePc();
        RosterManager.instance.selectedTable = tables[0];
        RosterManager.instance.AssignWorker(persona);

        Main.instance.SetSumm(money);
    }

    private void ResetInfo()
    {
        companyName = "";
        personaName = "";
        personaType = 0;
        // dont update model, year and money info 
    }
}