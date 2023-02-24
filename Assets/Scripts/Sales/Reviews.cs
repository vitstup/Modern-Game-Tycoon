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

    public float GetInterest()
    {
        return UserScore() * UserScore();
    }

    public Reviews(int positive, int negative)
    {
        this.positive = positive;
        this.negative = negative;
    }

    public static Reviews operator +(Reviews r1, Reviews r2)
    {
        return new Reviews(r1.positive + r2.positive, r1.negative + r2.negative);
    }
}