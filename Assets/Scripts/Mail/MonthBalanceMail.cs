using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonthBalanceMail : Mail
{
    private static readonly string[] text = new string[]
    {
         "Hey boss,\r\n\r\n\r\nI have analyzed your income and expenses for the last month:\r\n\r\n\r\n%info%\r\n\r\n\r\nSincerely,\r\n%sender%",
         "Привет босс,\r\n\r\n\r\nКак вы и просили, я собрал статистику о доходах и расходах за месяц:\r\n\r\n\r\n%info%\r\n\r\n\r\nИскренне,\r\n%sender%",
         "Salut patron,\r\n\r\n\r\nJ'ai analys? vos revenus et d?penses pour la derni?re mese:\r\n\r\n\r\n%Info%\r\n\r\n\r\nSinc?rement,\r\n%sender%",
         "Hallo Chef,\r\n\r\n\r\nIch habe Ihre Einnahmen und Ausgaben f?r das letzte Monat analysiert:\r\n\r\n\r\n%info%\r\n\r\n\r\nAufrichtig,\r\n%sender%",
         "Hola jefe,\r\n\r\n\r\nHe analizado sus ingresos y gastos del ?ltimo mese:\r\n\r\n\r\n%info%\r\n\r\n\r\nAtentamente,\r\n%sender%",
         "Ehi capo,\r\n\r\n\r\n\r\n\r\nHo analizzato le entrate e le spese dell'ultimo mese:\r\n\r\n\r\n\r\n\r\n%info%\r\n\r\n\r\n\r\n\r\nCordiali saluti,\r\n%sender%",
    };

    private long income;
    private long expenses;

    public MonthBalanceMail(string sender, long income, long expenses) : base(sender)
    {
        this.income = income;
        this.expenses = expenses;
    }

    public override string GetMessage(int localization)
    {
        string incomeStr = Localization.Localize("income") + ": " + TextConvertor.moneyText(income);
        string expensestr = Localization.Localize("expenses") + ": " + TextConvertor.moneyText(expenses);

        string result = text[localization];
        result = result.Replace("%info%", incomeStr + "\n" + expensestr);
        result = result.Replace("%sender%", sender);
        return result;
    }

    public override string GetTheme()
    {
        return Localization.Localize("mailTheme.5");
    }
}