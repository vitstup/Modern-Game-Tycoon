using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Persona 
{
    [field: SerializeField] public string personaName { get; private set; }

    [field: SerializeField] public Skills skills { get; private set; }

    [field: SerializeField] public int salary { get; private set; }

    [field: SerializeField] public int modelId { get; private set; }

    [field: SerializeField] public bool startWorker { get; private set; }

    public Table table;

    public Persona(bool startWorker)
    {
        this.startWorker = startWorker;
        RandomPersona(Constans.minSkill, Constans.minSalary);
    }

    public Persona(string personaName, Skills skills, int salary, int modelId)
    {
        startWorker = true;
        this.personaName = personaName;
        this.skills = skills;
        this.salary = salary;
        this.modelId = modelId;
    }

    public Persona(SaveLoad.PersonaSaver saver)
    {
        personaName = saver.name;
        skills = saver.skills;
        salary = saver.salary;
        modelId = saver.modelId;
        startWorker = saver.startWorker;
    }

    private void RandomPersona(int minSkill, int minSalary)
    {
        bool isMale = Random.Range(0, 2) == 1;

        personaName = RandomName(isMale);

        skills = new Skills(minSkill);

        salary = CalculateSalary(1.02f, minSalary);

        if (isMale) modelId = Random.Range(0, Constans.maleModels);
        else modelId = Random.Range(Constans.maleModels, Constans.maleModels + Constans.femaleModels);
    }

    private int CalculateSalary(float coef, int minSalary)
    {
        float salary = 0;
        salary += SalaryProgresy(skills.programming, coef);
        salary += SalaryProgresy(skills.gameDesign, coef);
        salary += SalaryProgresy(skills.artDesign, coef);
        salary += SalaryProgresy(skills.soundDesign, coef);
        salary += SalaryProgresy(skills.screenwriting, coef);
        salary *= Random.Range(0.75f, 1.5f);
        if (salary < minSalary) salary = minSalary;
        return (int)(salary - salary % 10);
    }

    private float SalaryProgresy(int skill, float coef)
    {
        float result = 0;
        float progress = 1;
        for (int i = 0; i < skill; i++)
        {
            progress *= coef;
            result += progress * Constans.basePayPerPoint;
        }
        return result;
    }

    public static string RandomName(bool isMale)
    {
        int nameId = Random.Range(0, Constans.maleNamesLength);
        if (!isMale) nameId = Random.Range(0, Constans.femaleNamesLength);
        int surnameId = Random.Range(0, Constans.surnamesLength);
        StringBuilder sb = new StringBuilder();
        string surname = Localization.Localize("surname." + surnameId);
        string name = (isMale)? name = Localization.Localize("maleName." + nameId) : name = Localization.Localize("femaleName." + nameId);
        sb.Append(name.Substring(0, 1).ToUpper());
        sb.Append(name.Remove(0, 1));
        sb.Append(" ");
        sb.Append(surname.Substring(0, 1).ToUpper());
        sb.Append(surname.Remove(0, 1));
        return sb.ToString();
    }

    public float DevelopmentSpeed(float baseSpeed)
    {
        baseSpeed += ComputerManager.instance.computers[table.currentPc].productionBonus;
        float happines = BuildingManager.instance.happiness;
        baseSpeed += happines > 1? (happines - 1f) / 2f: (happines - 1f);
        if (baseSpeed < 0.33f) baseSpeed = 0.33f;
        return baseSpeed;
    }
}