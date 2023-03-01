using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class TextConvertor 
{

    private static string GetFirstLetter(int num)
    {
        return num.ToString()[0].ToString();
    }

    public static string numText(long summ)
    {
        long module = Math.Abs(summ);

        int billiards = (int)(module / 1000000000);
        int millions = (int)((module - billiards) / 1000000);
        int thousands = (int)((module - billiards - millions) / 1000);
        int units = (int)(module % 1000);

        if (billiards > 0) return billiards + "." + GetFirstLetter(millions) + "B";
        else if (millions > 0) return millions + "." + GetFirstLetter(thousands) + "M";
        else if (thousands > 0)
        {
            string unitsString = units.ToString();
            if (units == 0) unitsString = "000";
            else if (units < 10) unitsString = "00" + unitsString;
            else if (units < 100) unitsString = "0" + unitsString;

            return thousands + "." + unitsString;
        }
        else return units.ToString();
    }

    public static string moneyText(long num)
    {
        return numText(num) + " $";
    }

    public static string moneyTextWitMinus(long num)
    {
        return num < 0? "-" + moneyText(num) : moneyText(num);
    }

    public static string percentText(float num)
    {
        num *= 100f;
        return Math.Round(num, 1).ToString() + " %";
    }

    public static string ChangeColor(string text, Color color)
    {
        return string.Format("<color={0}>", "#" + ColorUtility.ToHtmlStringRGB(color)) + text; 
    }

    public static string ChangeColorAndSize(string text, Color color, int size)
    {
        return string.Format("<size={0}>", size) + ChangeColor(text, color);
    }

    public static string GameSizeText(int size)
    {
        if (size == 0) return Localization.Localize("indie");
        else return Constans.gameSizes[size];
    }

    public static string BugsText(float bugs)
    {
        if (bugs < 1f) return "0";
        else if (bugs < 10f) return Localization.Localize("few");
        else return "> " + ((int)(bugs - bugs % 10)).ToString(); 
    }

    public static string ReviewChanceText(float chance)
    {
        float minChance = (float) Math.Round((chance - 0.15f) / 0.1f) * 0.1f;
        float maxChance = (float) Math.Round((chance + 0.15f) / 0.1f) * 0.1f;
        if (minChance < 0) minChance = 0;
        if (maxChance > 1) maxChance = 1;

        return (int)(minChance * 100) + " - " + (int)(maxChance * 100) + " %";
    }

    public static string corrOfPricText(Game game)
    {
        int idealPay = Constans.sizesPrices[game.size];
        float diif = Math.Abs(game.price - idealPay) / idealPay;
        if (diif > 0.7f) return ReviewChanceText(0.15f);
        else if (diif > 0.4f) return ReviewChanceText(0.55f);
        else return ReviewChanceText(0.85f);
    }

    public static string ChangeWeight(string text, int weight)
    {
        return string.Format("<font-weight=\"{0}\">", weight) + text + "</font-weight>";
    }
}