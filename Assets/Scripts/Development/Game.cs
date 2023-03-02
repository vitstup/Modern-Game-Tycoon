using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game : GameProject
{
    public Publisher publisher;
    public float price;

    public Game sequelOf;

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

    public int firstDayProfit;

    public float hype;

    public int daysTillMarketingCampaign;

    public float interest = 1f; // decreasing every sale day

    public override void Done()
    {
        base.Done();
        Statistics.instance.games.Add(this);
        if (ProjectManager.instance.maxSizeGameCreated < size) ProjectManager.instance.maxSizeGameCreated = size;
        SalesUI.instance.OpenSalesCanvas(true);
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

    public void UpdateGame(float plot, float gameDesign, float gameplay, float graphics, float sound, float bugs, float interest, Platform[] platforms)
    {
        AddPoints(new float[] { plot, gameDesign, gameplay, graphics, sound }, 1f);
        UpdateBugs(bugs);
        this.interest += interest;
        if (this.interest > 1f) this.interest = 1f;
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] != null) this.platforms[i] = platforms[i];
        }
    }
}