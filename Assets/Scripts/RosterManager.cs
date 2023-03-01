using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RosterManager : MonoBehaviour
{
    public static RosterManager instance;

    private int currentId = 0;

    [field: SerializeField] public List<Persona> availableWorkers { get; private set; } = new List<Persona>();
    [field: SerializeField] public List<Persona> hiredWorkers { get; private set; } = new List<Persona>();

    [HideInInspector] public Table selectedTable;

    private void Awake()
    {
        TimeManager.DayUpdateEvent.AddListener(availablesUpdate);
        TimeManager.MonthUpdateEvent.AddListener(MonthUpdate);
        instance = this;
    }

    private void availablesUpdate()
    {
        for (int i = 0; i < availableWorkers.Count; i++)
        {
            if (Random.Range(0, 1f) <= Constans.AvailableFindWorkChance) { availableWorkers.RemoveAt(i); i--; }
        }
        if (Random.Range(0, 1f) <= Constans.AvailableAppearChance)
        {
            if (availableWorkers.Count < Constans.maxAvailableWorkers) availableWorkers.Add(Worker());
        } 
    }

    private Persona Worker()
    {
        currentId++;
        return new Persona(currentId);
    }

    public void HireWorker(Persona persona)
    {
        hiredWorkers.Add(persona);
        availableWorkers.Remove(persona);
    }

    public void FireWorker(Persona persona)
    {
        persona.table.DeAssignedWorker();
        hiredWorkers.Remove(persona);
    }

    public void AssignWorker(Persona persona)
    {
        if (persona.table != null) persona.table.DeAssignedWorker();
        DeasignFromSelecyedTable();
        persona.table = selectedTable;
        persona.table.AssignedWorker(persona);

        RosterUI.instance.CloseRoster();
    }

    private void DeasignFromSelecyedTable()
    {
        for (int i = 0; i < hiredWorkers.Count; i++)
        {
            if (hiredWorkers[i].table == selectedTable)
            {
                hiredWorkers[i].table = null;
            }
        }
    }

    public Persona[] GetAssignWorkers()
    {
        List<Persona> assignWorkers = new List<Persona>();
        for (int i = 0; i < hiredWorkers.Count; i++)
        {
            if (hiredWorkers[i].table != null) assignWorkers.Add(hiredWorkers[i]);
        }
        return assignWorkers.ToArray();
    }

    private void MonthUpdate()
    {
        int expenses = 0;
        for (int i = 0; i < hiredWorkers.Count; i++)
        {
            expenses += hiredWorkers[i].salary;
        }
        Main.instance.MinusMoney(expenses);
        if (ProjectManager.instance.project != null && ProjectManager.instance.project is GameProject) (ProjectManager.instance.project as GameProject).expenses += expenses;
    }

    public int[] GetTotalSkills(Persona[] personas)
    {
        int[] skills = new int[5];
        for (int i = 0; i < personas.Length; i++)
        {
            skills[0] += personas[i].skills.programming;
            skills[1] += personas[i].skills.gameDesign;
            skills[2] += personas[i].skills.artDesign;
            skills[3] += personas[i].skills.soundDesign;
            skills[4] += personas[i].skills.screenwriting;
        }
        return skills;
    }

    public int[] GetAverageSkills(Persona[] personas)
    {
        int[] skills = GetTotalSkills(personas);
        if (personas.Length == 0) { return skills; }
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] /= personas.Length;
        }
        return skills;
    }
}