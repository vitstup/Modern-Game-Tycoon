using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constans 
{
    public const int maleNamesLength = 135;
    public const int femaleNamesLength = 196;
    public const int surnamesLength = 154;

    public const int maleModels = 14;
    public const int femaleModels = 12;

    public const int basePayPerPoint = 15;

    public static readonly int[] DaysInMonth =  { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public const int maxAvailableWorkers = 30;
    public const float AvailableFindWorkChance = 0.015f;
    public const float AvailableAppearChance = 0.5f;
    public const int minSkill = 5;
    public const int minSalary = 1000;

    public static readonly Color GreenColor = new Color(0.58f, 0.86f, 0.58f);
    public static readonly Color GrayBgInactive = new Color(0.44f, 0.44f, 0.44f);
    public static readonly Color GrayBgActive = new Color(0.49f, 0.49f, 0.49f);
    public static readonly Color WhiteTextInactive = new Color(0.83f, 0.83f, 0.83f);
    public static readonly Color WhiteTextActive = new Color(1, 1, 1);
    public static readonly Color BlackTextInactive = new Color(0.2f, 0.2f, 0.2f);
    public static readonly Color BlackTextActive = new Color(0, 0 ,0);
}