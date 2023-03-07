using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformBoughtedMail : Mail
{
    private static readonly string[] text = new string[]
    {
         "Hi there, \r\n\r\nWe inform you that the purchase of the %subject% developer license was successful. \r\n\r\nContact our technical specialist to start developing on this platform.\r\n\r\nSpecialist phone 555 17 83\r\n\r\nOur website: https://%sender%.com/%_subject%/\r\n\r\nSincerely,\r\n%sender% Team",
         "Всем привет,\r\n\r\nСообщаем вам, что покупка лицензии разработчика %subject% прошла успешно.\r\n\r\nСвяжитесь с нашим техническим специалистом, чтобы начать разработку на этой платформе.\r\n\r\nТелефон специалиста 555 17 83\r\n\r\nНаш сайт: https: //%sender%.com/%_subject%/\r\n\r\nИскренне,\r\nКоманда %sender%",
         "Salut,\r\n\r\nNous vous informons que l'achat de la licence d?veloppeur %subject% a r?ussi.\r\n\r\nContactez notre sp?cialiste technique pour commencer ? d?velopper sur cette plateforme.\r\n\r\nT?l?phone sp?cialis? 555 17 83\r\n\r\nNotre site Web : https://%sender%.com/%_subject%/\r\n\r\nSinc?rement,\r\n?quipe %sender%",
         "Hi,\r\n\r\nWir informieren Sie, dass der Kauf der %subject%-Entwicklerlizenz erfolgreich war.\r\n\r\nKontaktieren Sie unseren technischen Spezialisten, um mit der Entwicklung auf dieser Plattform zu beginnen.\r\n\r\nFachtelefon 555 17 83\r\n\r\nUnsere Website: https://%sender%.com/%_subject%/\r\n\r\nAufrichtig,\r\n%sender%-Team",
         "Hola,\r\n\r\nLe informamos que la compra de la licencia de desarrollador %subject% fue exitosa.\r\n\r\nP?ngase en contacto con nuestro especialista t?cnico para comenzar a desarrollar en esta plataforma.\r\n\r\nTel?fono especialista 555 17 83\r\n\r\nNuestro sitio web: https: //%sender%.com/%_subject%/\r\n\r\nAtentamente,\r\n%sender% Equipo",
         "Ciao,\r\n\r\nTi informiamo che l'acquisto della licenza per sviluppatori %subject% ? andato a buon fine.\r\n\r\nContatta il nostro specialista tecnico per iniziare a sviluppare su questa piattaforma.\r\n\r\nTelefono specializzato 555 17 83\r\n\r\nIl nostro sito web: https://%sender%.com/%_subject%/\r\n\r\nCordiali saluti,\r\n%sender% squadra",
    };

    private string platformName;

    public PlatformBoughtedMail(string sender, Platform platform) : base(sender)
    {
        platformName = platform.info.name;
    }

    public override string GetMessage(int localization)
    {
        string result = text[localization];
        result = result.Replace("%subject%", platformName);
        result = result.Replace("%_subject%", platformName.Replace(" ", "_"));
        result = result.Replace("%sender%", sender);
        return result;
    }

    public override string GetTheme()
    {
        return Localization.Localize("mailTheme.2");
    }
}