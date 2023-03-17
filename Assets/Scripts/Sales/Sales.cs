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

    private static float NumToMillions(float num)
    {
        return num / 1000000f;
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
            if (game.publisher != null) auditory += NumToMillions(game.publisher.GetAuditory(game.size));
            else auditory *= priceBuff;
            auditory -= NumToMillions(game.sales[i].sales);
            auditory *= interest;

            float sales = auditory * 10000f * (1f + game.hype) * game.interest;

            sales *= Random.Range(0.9f, 1.1f);
            if (sales < 1 && game.interest > 0.5f) sales = 1;

            float price = game.publisher != null ? game.publisher.GetPayment(game.size) : game.price;
            float profit = sales * price * (1f - game.engine.info.commision) * (1f - game.platforms[i].info.commision);

            game.todayProfit += (int)profit;
            game.todaySales += (int)sales;

            game.reviews += ReviewChance.Review(chances[i], sales > 50? (int)(sales / 50f) : 1);

            game.sales[i].sales += (int)sales;
            game.sales[i].profit += (int)profit;

            Debug.Log("auditory " + auditory + ", sizeDebaff" + auditorySizeDebaff);
        }
        game.interest -= (1f - game.reviews.UserScore()) * Constans.interestDecreaseSpeed * (1 + game.hype);
        if (game.interest < 0.05f) game.interest = 0.05f;

        game.AddRecentProfit();

        if (game.firstDayProfit == 0) game.firstDayProfit = game.todayProfit; 

        Main.instance.AddMoney(game.todayProfit);

        if (!game.bugMailSended) TryToSendBugsMail(game);

        if (game.reviews.UserScore() >= 0.95f) AchievementsManager.instance.SetAchievment(10);
        if (game.reviews.UserScore() <= 0.05f) AchievementsManager.instance.SetAchievment(11);
    }

    private static void TryToSendBugsMail(Game game)
    {
        float bugs = game.bugs / Constans.sizesScale[game.size];
        if (bugs > 50)
        {
            if (Random.Range(0, 100) < bugs - 50)
            {
                string sender = Localization.Localize("maleName.") + Random.Range(0, Constans.maleNamesLength);
                MailManager.instance.NewMail(new BugsMail(sender, game));
            }
        }
    }
}