using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reviews 
{
    public int positive;
    public int negative;

    public float UserScore()
    {
        return positive / (float)(positive + negative);
    }
}