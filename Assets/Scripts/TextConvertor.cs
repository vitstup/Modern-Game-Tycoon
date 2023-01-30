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

        

        if (module > 100000000)
        {
            sb.Append((module - module % 1000000000) / 1000000000);
            sb.Append(",");
            sb.Append((module % 1000000000 / 1000000).ToString("000"));
            sb.Append(" B");
        }
        else if (module > 1000000)
        {
            sb.Append((module - module % 1000000) / 1000000);
            sb.Append(",");
            sb.Append((module % 1000000 / 1000).ToString("000"));
            sb.Append(" M");
        }
        else if (module > 1000)
        {
            sb.Append((module - module % 1000) / 1000);
            sb.Append(".");
            sb.Append((module % 1000).ToString("000"));
        }
        else if (module < 1000) sb.Append(module);

        sb.Append(" $");

        return sb.ToString();
    }
}