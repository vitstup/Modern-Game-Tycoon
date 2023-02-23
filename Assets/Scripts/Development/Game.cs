using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game : GameProject
{
    public Publisher publisher;
    public float price;

    // sequel


    // money parametrs
    // sales parametrs

    // hype

    public override void Done()
    {
        Statistics.instance.games.Add(this);
    }
}