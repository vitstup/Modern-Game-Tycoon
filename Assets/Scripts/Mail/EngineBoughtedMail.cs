using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EngineBoughtedMail : Mail 
{

    private static readonly string[] text = new string[] 
    {
        "Hi there, \r\n\r\nWe inform you that the purchase of the %subject% license was successful. \r\nNow you can start using the engine. \r\nYou can see your identification code on our website in your personal account. \r\nYou can also download the engine there.\r\n\r\nOur website: https://%sender%.com/%_subject%/\r\n\r\nSincerely,\r\n%sender% Team",
        "Всем привет,\r\n\r\nСообщаем вам, что покупка лицензии %subject% прошла успешно.\r\nТеперь вы можете приступить к работе с движком.\r\nВы можете увидеть свой идентификационный код на нашем сайте в личном кабинете.\r\nВы также можете скачать движок там.\r\n\r\nИскренне,\r\nКоманда %sender%",
        "Salut,\r\n\r\nNous vous informons que l'achat de la licence %subject% a r?ussi.\r\nVous pouvez maintenant commencer ? utiliser le moteur.\r\nVous pouvez voir votre code d'identification sur notre site Web dans votre compte personnel.\r\nVous pouvez ?galement y t?l?charger le moteur.\r\n\r\nNotre site Web : https://%sender%.com/%_subject%/\r\n\r\nSinc?rement,\r\n?quipe %sender%",
        "Hi,\r\n\r\nWir informieren Sie, dass der Kauf der %subject%-Lizenz erfolgreich war.\r\nJetzt k?nnen Sie mit der Verwendung des Motors beginnen.\r\nSie k?nnen Ihren Identifikationscode auf unserer Website in Ihrem pers?nlichen Konto einsehen.\r\nDort k?nnen Sie die Engine auch herunterladen.\r\n\r\nUnsere Website: https://%sender%.com/%_subject%/\r\n\r\nAufrichtig,\r\n%sender%-Team",
        "Hola,\r\n\r\nLe informamos que la compra de la licencia %subject% fue exitosa.\r\nAhora puede empezar a utilizar el motor.\r\nPuede ver su c?digo de identificaci?n en nuestro sitio web en su cuenta personal.\r\nTambi?n puede descargar el motor all?.\r\n\r\nNuestro sitio web: https: //%sender%.com/%_subject%/\r\n\r\nAtentamente,\r\n%sender% Equipo",
        "Ciao,\r\n\r\nTi informiamo che l'acquisto della licenza %subject% ? andato a buon fine.\r\nOra puoi iniziare a usare il motore.\r\nPuoi vedere il tuo codice identificativo sul nostro sito web nel tuo account personale.\r\nPuoi anche scaricare il motore l?.\r\n\r\nIl nostro sito web: https://%sender%.com/%_subject%/\r\n\r\nCordiali saluti,\r\n%sender% squadra"
    };

    private string engineName;

    public EngineBoughtedMail(string sender, Engine engine) : base(sender)
    {
        engineName = engine.info.name;
    }

    public override string GetMessage(int localization)
    {
        string result = text[localization];
        result = result.Replace("%subject%", engineName);
        result = result.Replace("%_subject%", engineName.Replace(" ", "_"));
        result = result.Replace("%sender%", sender);
        return result;
    }

    public override string GetTheme()
    {
        return Localization.Localize("mailTheme.1");
    }
}