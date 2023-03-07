using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkerHiredMail : Mail
{
    private static readonly string[] text = new string[]
    {
         "Hello there,\r\n\r\nI inform you that I %subject% have read your job offer and agree to all terms and conditions.\r\nTomorrow I will be at work by 8 am.\r\nIf you need something from me, you can contact me by e-mail: \r\nhttps: //mail.com/%_subject%/\r\n\r\nSincerely,\r\n%sender%",
         "Привет,\r\n\r\nСообщаю вам, что я %subject% прочитал ваше предложение о работе и согласен со всеми условиями.\r\nЗавтра к 8 часам буду на месте.\r\nЕсли вам что-то нужно от меня, вы можете связаться со мной по электронной почте:\r\nhttps: //mail.com/%_subject%/\r\n\r\nИскренне,\r\n%sender%",
         "Bonjour,\r\n\r\nJe vous informe que j'ai %subject% avoir lu votre offre d'emploi et j'accepte tous les termes et conditions.\r\nDemain, je serai au travail ? 8 heures du matin.\r\nSi vous avez besoin de quelque chose de ma part, vous pouvez me contacter par e-mail :\r\nhttps : //mail.com/%_subject%/\r\n\r\nSinc?rement,\r\n%sender%",
         "Hallo,\r\n\r\nIch informiere Sie, dass ich %subject% Ihr Stellenangebot gelesen habe und mit allen Bedingungen einverstanden bin.\r\nMorgen bin ich um 8 Uhr bei der Arbeit.\r\nWenn Sie etwas von mir ben?tigen, k?nnen Sie mich per E-Mail kontaktieren:\r\nhttps: //mail.com/%_subject%/\r\n\r\nAufrichtig,\r\n%sender%",
         "Hola,\r\n\r\nLes informo que I %subject% he le?do su oferta de trabajo y estoy de acuerdo con todos los t?rminos y condiciones.\r\nMa?ana estar? en el trabajo a las 8 am.\r\nSi necesita algo de m?, puede contactarme por correo electr?nico:\r\nhttps: //mail.com/%_subject%/\r\n\r\nAtentamente,\r\n%sender%",
         "Ciao,\r\n\r\nTi informo che %subject% ho letto la tua offerta di lavoro e accetto tutti i termini e le condizioni.\r\nDomani sar? al lavoro entro le 8:00.\r\nSe hai bisogno di qualcosa da me, puoi contattarmi via e-mail:\r\nhttps: //mail.com/%_subject%/\r\n\r\nCordiali saluti,\r\n%sender%",
    };

    private string workerName;

    public WorkerHiredMail(string sender, Persona persona) : base(sender)
    {
        workerName = persona.personaName;
    }

    public override string GetMessage(int localization)
    {
        string result = text[localization];
        result = result.Replace("%subject%", workerName);
        result = result.Replace("%_subject%", workerName.Replace(" ", "_"));
        result = result.Replace("%sender%", sender);
        return result;
    }

    public override string GetTheme()
    {
        return Localization.Localize("mailTheme.3");
    }
}