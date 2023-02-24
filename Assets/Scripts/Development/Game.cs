using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game : GameProject
{
    public Publisher publisher;
    public float price;

    // sequel

    [System.Serializable]
    public struct PlatformSales
    {
        public int sales;
        public int profit;
    }

    public int todaySales;
    public int todayProfit;
    public int[] recentProfits = new int[14];
    public PlatformSales[] sales = new PlatformSales[4];

    public int firstDaySales;

    public float hype;

    //public int daysFromMarketingCompany;

    public override void Done()
    {
        Statistics.instance.games.Add(this);
        ReviewChance.SetStartReviews(this);
    }

    public void AddRecentProfit()
    {
        int[] newRecentProfits = new int[recentProfits.Length];
        newRecentProfits[recentProfits.Length - 1] = todayProfit;
        for (int i = 1; i < recentProfits.Length; i++)
        {
            newRecentProfits[i - 1] = recentProfits[i];
        }
        recentProfits = newRecentProfits;
    }

    public int TotalSales()
    {
        int totalSales = 0;
        for (int i = 0; i < sales.Length; i++)
        {
            totalSales += sales[i].sales;
        }
        return totalSales;
    }
    public int TotalProfit()
    {
        int totalProfit = expenses * -1;
        for (int i = 0; i < sales.Length; i++)
        {
            totalProfit += sales[i].profit;
        }
        return totalProfit;
    }
}