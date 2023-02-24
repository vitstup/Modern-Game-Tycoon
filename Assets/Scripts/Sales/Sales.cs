using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sales
{
    private static float startUsers = 300; // in millions

    private static float GetCurrentUsers()
    {
        return startUsers + 7 * TimeManager.instance.GetMonthsFromStart();
    }

    private static float GetPriceBuff(Game game)
    {
        if (game.publisher != null) return 0;
        float priceBuff = Constans.sizesPrices[game.size] / game.price;
        if (priceBuff > 1) priceBuff = 1 + (priceBuff - 1) * 0.75f;
        else if (priceBuff < 1) priceBuff = 1 - (1 - priceBuff) * 1.5f;
        return priceBuff;
    }

    public static void Sale(Game game)
    {
        game.todayProfit = 0;
        game.todaySales = 0;

        float users = GetCurrentUsers();

        float priceBuff = GetPriceBuff(game);

        float auditorySizeDebaff = Constans.sizesScale[System.Math.Abs(game.size - (Constans.sizesScale.Length - 1))];

        var chances = ReviewChance.PositiveReviewChance(game);
        for (int i = 0; i < chances.Length; i++)
        {
            if (game.platforms[i] == null) continue;

            float auditory = game.platforms[i].marketShare * users;
            auditory *= game.platforms[i].avaialable() ? 1 - PlatformsManager.instance.unavailableAuditory : PlatformsManager.instance.unavailableAuditory;

            float interest = game.reviews.GetInterest();

            auditory /= AttributesManager.instance.genres.Length;
            auditory /= auditorySizeDebaff;
            if (game.publisher != null) auditory += game.publisher.auditory;
            else auditory *= priceBuff;
            auditory -= game.sales[i].sales / 1000000f;
            auditory *= interest;

            float sales = auditory * 10000f * (1f + game.hype);

            float price = game.publisher != null ? game.publisher.GetPayment(game.size) : game.price;
            float profit = sales * price * (1f - game.engine.info.commision) * (1f - game.platforms[i].info.commision);

            game.todayProfit += (int)profit;
            game.todaySales += (int)sales;

            game.reviews += ReviewChance.Review(chances[i], sales > 50? (int)(sales / 50f) : 1);

            game.sales[i].sales += (int)sales;
            game.sales[i].profit += (int)profit;

            Debug.Log("auditory " + auditory + ", sizeDebaff" + auditorySizeDebaff);
        }

        game.AddRecentProfit();

        if (game.firstDaySales == 0) game.firstDaySales = game.todaySales;

        Main.instance.AddMoney(game.todayProfit);
    }
}