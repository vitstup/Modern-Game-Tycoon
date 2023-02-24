using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class ReviewChance 
{
    public static float BasePoints = 200; // medium points for 100% positive reviews

    private static float CurrentBestPoints()
    {
        float months = TimeManager.instance.GetMonthsFromStart();
        if (months > TimeManager.instance.GetEndDateMonths()) months = TimeManager.instance.GetEndDateMonths();

        return BasePoints + months * 1.5f;
    }

    public static float[] PositiveReviewChance(GameProject game)
    {
        float points = 0;
        points += game.plot * game.genre.pointsDistribution.plot;
        points += game.gameDesign * game.genre.pointsDistribution.gameDesign;
        points += game.gameplay * game.genre.pointsDistribution.gameplay;
        points += game.graphics * game.genre.pointsDistribution.graphics;
        points += game.sound * game.genre.pointsDistribution.sound;

        points /= Constans.sizesScale[game.size];

        float bugsInfluence = game.bugs / points;

        float positiveChance = points / CurrentBestPoints();

        float result = positiveChance - bugsInfluence;

        // Debug.Log("points " + points + ", best points " + CurrentBestPoints() + ", result " + result + ", bugs influence " + bugsInfluence);

        if (result > 1) result = 1;
        else if (result < 0.1) result = 0.1f;

        float[] results = new float[game.platforms.Length];

        for (int i = 0; i < results.Length; i++)
        {
            if (game.platforms[i] == null) continue;
            results[i] = result * game.platforms[i].GetPointsPenalty(game.GetComputingUsage(), game.GetGraphicsUsage());
        }

        return results;
    }

    public static void SetStartReviews(GameProject game)
    {
        float positive = PositiveReviewChance(game)[0];
        float reviewCount = Constans.sizesScale[game.size] * 10;
        game.reviews = new Reviews((int)(positive * reviewCount), (int)(( 1 - positive) * reviewCount));
    }

    public static Reviews Review(float positiveChance, int reviewCount)
    {
        return new Reviews((int)(positiveChance * reviewCount), (int)((1 - positiveChance) * reviewCount));
    }
}