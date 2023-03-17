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

    public bool bugMailSended;

    public override void Done()
    {
        base.Done();
        Statistics.instance.games.Add(this);
        if (ProjectManager.instance.maxSizeGameCreated < size) ProjectManager.instance.maxSizeGameCreated = size;
        SalesUI.instance.OpenSalesCanvas(true);

        AchievementsManager.instance.SetAchievment(1);
        if (Statistics.instance.games.Count >= 5) AchievementsManager.instance.SetAchievment(4);
        if (bugs >= 500) AchievementsManager.instance.SetAchievment(7);
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

    public Game() { }

    public Game(SaveLoad.GameSaver saver) : base(saver)
    {
        if (saver.publisher >= 0) publisher = PublishersManager.instance.publishers[saver.publisher];
        // sequel
        price = saver.price;
        todaySales = saver.todaySales;
        todayProfit = saver.todayProfit;
        recentProfits = saver.recentProfits;
        sales = saver.sales;
        firstDayProfit = saver.firstDayProfit;
        hype = saver.hype;
        daysTillMarketingCampaign = saver.daysTillMarketingCampaign;
        interest = saver.interest;
        bugMailSended = saver.bugMailSended;
    }
}