using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Mail 
{
    [field: SerializeField] public string adress { get; private set; }
    [field: SerializeField] public string sender { get; private set; }


    [SerializeField] private int day;
    [SerializeField] private int month;
    [SerializeField] private int year;

    public bool wasOpened; // was mail opened at least one time
    public bool isFavourite;


    public Mail(string sender)
    {
        this.sender = sender;

        day = TimeManager.instance.day + 1;
        month = TimeManager.instance.month + 1;
        year = TimeManager.instance.year;

        adress = RandomString("qrstuvwxyzGhZHTDGSJ", UnityEngine.Random.Range(7, 10));
    }

    // adress
    private static System.Random ran = new System.Random();
    private static string RandomString(string alphabet, int lenght)
    {
        char[] arr = new char[lenght];
        for (int i = 0; i < arr.Length; ++i)
        {
            arr[i] = alphabet[ran.Next(alphabet.Length)];
        }
        return new String(arr);
    }
    // adress end

    public abstract string GetMessage(int localization);

    public abstract string GetTheme();

    public string GetDate()
    {
        string result = day >= 10 ? day + "." : "0" + day + ".";
        result += month >= 10 ? month + "." : "0" + month + ".";
        result += year.ToString();
        return result;
    }
}