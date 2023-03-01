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
            MarketingManager.instance.HypeDecrease(games[i]);
            Sales.Sale(games[i]);
        }
        SalesUI.instance.TryToUpdatePanels();
    }

    public List<Game> GetReverseGames()
    {
        List<Game> result = new List<Game>();
        for (int i = games.Count - 1; i >= 0; i--)
        {
            result.Add(games[i]); 
        }
        return result;
    }

    public Game[] GetGamesWithoutSequel()
    {
        List<Game> gamesWithSequel = new List<Game>();
        List<Game> gamesWithoutSequel = new List<Game>();

        for (int i = 0; i < games.Count; i++) // games with sequel
        {
            if (games[i].sequelOf != null) gamesWithSequel.Add(games[i].sequelOf);
        }

        for (int i = 0; i < games.Count; i++) // games without sequel
        {
            bool haveSequel = false;
            for (int s = 0; s < gamesWithSequel.Count; s++)
            {
                if (games[i] == gamesWithSequel[s]) { haveSequel = true; break; }
            }
            if (!haveSequel) gamesWithoutSequel.Add(games[i]);
        }

        return gamesWithoutSequel.ToArray();
    }
}