using System.Collections;
using System.Collections.Generic;
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
}