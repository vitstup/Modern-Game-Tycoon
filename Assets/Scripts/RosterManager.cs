using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosterManager : MonoBehaviour
{
    public static RosterManager instance;

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
        return new Persona(false);
    }

    public void HireWorker(Persona persona)
    {
        if (hiredWorkers.Count >= Constans.maxWorkers) return;
        hiredWorkers.Add(persona);
        availableWorkers.Remove(persona);
        MailManager.instance.NewMail(new WorkerHiredMail(persona.personaName, persona));

        AchievementsManager.instance.SetAchievment(0);
        if (hiredWorkers.Count >= 20) AchievementsManager.instance.SetAchievment(5);
    }

    public void FireWorker(Persona persona)
    {
        if (persona.startWorker) return;
        if (persona.table != null) persona.table.DeAssignedWorker();
        hiredWorkers.Remove(persona);
        MailManager.instance.NewMail(new WorkerFiredMail(persona.personaName, persona));
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