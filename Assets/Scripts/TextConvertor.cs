using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class TextConvertor 
{
    public static string moneyText(long num)
    {
        StringBuilder sb = new StringBuilder();
        long module = Math.Abs(num);

        if (module > 1000000000)
        {
            sb.Append(module / 1000000000);
            if (module % 1000000000 != 0)
            {
                sb.Append(".");
                sb.Append(module % 1000000000 / 100000000);
            }
            sb.Append("B");
        }
        else if (module > 1000000)
        {
            sb.Append(module / 1000000);
            if (module % 1000000 != 0)
            {
                sb.Append(".");
                sb.Append(module % 1000000 / 100000);
            }
            sb.Append("M");

        }
        else if (module > 1000)
        {
            sb.Append(module / 1000);
            if (module % 1000 != 0)
            {
                sb.Append(".");
                sb.Append(module % 1000);
            }
            else sb.Append(".000");

        }
        else sb.Append(module);

        sb.Append(" $");

        return sb.ToString();
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
        if (bugs >= 1000) return "> 1000";
        else if (bugs >= 500) return "> 500";
        else if (bugs >= 250) return "> 250";
        else if (bugs >= 100) return "> 100";
        else if (bugs >= 50) return "> 50";
        else if (bugs >= 10) return "> 10";
        else return Localization.Localize("unknown");
    }
}