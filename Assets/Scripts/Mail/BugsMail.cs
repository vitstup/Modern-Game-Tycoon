using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsMail : Mail
{
    private string text;

    public BugsMail(string sender, int day, int month, int year) : base(sender, day, month, year)
    {

    }

    public override string GetMessage()
    {
        return base.GetMessage();
    }
}