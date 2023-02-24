using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static Statistics instance;
    [field: SerializeField] public List<Game> games { get; private set; } = new List<Game>();

    private void Awake()
    {
        instance = this;
        TimeManager.DayUpdateEvent.AddListener(DayUpdate);
    }

    private void DayUpdate()
    {
        for (int i = 0; i < games.Count; i++)
        {
            Sales.Sale(games[i]);
        }
        SalesUI.instance.TryToUpdatePanels();
    }
}