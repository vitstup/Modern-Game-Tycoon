using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkerFiredMail : Mail
{
    private static readonly string[] text = new string[]
    {
         "Hi boss,\r\n\r\n\r\nI have prepared all the papers on dismissal, I will not leave it that way. You will regret firing me. I always knew this place was a hole. At least  I'm finally free.\r\nIf you want to contact me, you know my email\r\nhttps: //mail.com/%_subject%/\r\n\r\n\r\n%sender%.",
         "Привет, босс,\r\n\r\n\r\nЯ подготовил все бумаги об увольнении, но я это так не оставлю. Вы пожалеете, что уволили меня. Я всегда знал, что это место - дыра. По крайней мере, я наконец-то свободен.\r\nЕсли вы хотите со мной связаться, вы знаете мою электронную почту\r\nhttps: //mail.com/%_subject%/\r\n\r\n\r\n%sender%.",
         "Bonjour patron,\r\n\r\n\r\nJ'ai pr?par? tous les papiers sur le licenciement, je ne le laisserai pas ainsi. Vous regretterez de m'avoir vir?. J'ai toujours su que cet endroit ?tait un trou. Au moins, je suis enfin libre.\r\nSi vous voulez me contacter, vous connaissez mon email\r\nhttps : //mail.com/%_subject%/\r\n\r\n\r\n%sender%.",
         "Hallo Chef,\r\n\r\n\r\nIch habe alle Entlassungspapiere vorbereitet, so werde ich es nicht belassen. Sie werden es bereuen, mich gefeuert zu haben. Ich wusste immer, dass dieser Ort ein Loch ist. Wenigstens bin ich endlich frei.\r\nWenn Sie mich kontaktieren m?chten, kennen Sie meine E-Mail\r\nhttps: //mail.com/%_subject%/\r\n\r\n\r\n%sender%.",
         "Hola jefe,\r\n\r\n\r\nHe preparado todos los papeles de despido, no lo dejar? as?. Te arrepentir?s de haberme despedido. Siempre supe que este lugar era un agujero. Al menos finalmente soy libre.\r\nSi quieres contactarme, conoces mi email\r\nhttps: //mail.com/%_subject%/\r\n\r\n\r\n%sender%.",
         "Salve capo,\r\n\r\n\r\nHo preparato tutte le carte sul licenziamento, non la lascer? cos?. Ti pentirai di avermi licenziato. Ho sempre saputo che questo posto era un buco. Almeno sono finalmente libero.\r\nSe vuoi contattarmi, conosci la mia email\r\nhttps: //mail.com/%_subject%/\r\n\r\n\r\n%sender%.",
    };

    private string workerName;

    public WorkerFiredMail(string sender, Persona persona) : base(sender)
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
        return Localization.Localize("mailTheme.4");
    }
}