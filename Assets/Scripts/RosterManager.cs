using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosterManager : MonoBehaviour
{
    public static RosterManager instance;

    public List<Persona> availableWorkers = new List<Persona>();
    public List<Persona> hiredWorkers = new List<Persona>();

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
            if (availableWorkers.Count < Constans.maxAvailableWorkers) availableWorkers.Add(new Persona(5, 1000));
        } 
    }
}