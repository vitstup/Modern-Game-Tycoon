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
        var reverse = GetReverseGames();

        List<Game> gamesWithSequel = new List<Game>();
        List<Game> gamesWithoutSequel = new List<Game>(reverse);

        for (int i = 0; i < reverse.Count; i++)
        {
            if (reverse[i].sequelOf != null) gamesWithSequel.Add(reverse[i].sequelOf);
        }
        for (int i = 0; i < gamesWithSequel.Count; i++)
        {
            gamesWithoutSequel.Remove(gamesWithSequel[i]);
        }

        return gamesWithoutSequel.ToArray();
    }

    public int GetGameId(Game game)
    {
        for (int i = 0; i < games.Count; i++)
        {
            if (game == games[i]) return i;
        }
        throw new System.Exception("There is no such game");
    }
}