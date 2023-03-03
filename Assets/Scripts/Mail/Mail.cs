using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail 
{
    [field: SerializeField] public string adress { get; private set; }
    [field: SerializeField] public string sender { get; private set; }
    [field: SerializeField] public string theme { get; private set; }


    [field: SerializeField] public int day { get; private set; }
    [field: SerializeField] public int month { get; private set; }
    [field: SerializeField] public int year { get; private set; }


    public Mail(string sender, int day, int month, int year)
    {
        this.sender = sender;

        this.day = day;
        this.month = month;
        this.year = year;

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

    public virtual string GetMessage()
    {
        return "";
    }
}