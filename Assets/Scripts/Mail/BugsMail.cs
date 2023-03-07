using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BugsMail : Mail
{
    private static readonly string[] text = new string[] 
    {
        "Hi there, \r\n\r\nI found a lot of bugs in %subject%, \r\ncan you fix them, game is pretty interesting\r\nbut it's impossible to play because all this bugs.\r\n\r\n\r\n\r\nSincerely,\r\n%sender%",
        "Всем привет,\r\n\r\nЯ нашел много ошибок в %subject%,\r\nможешь починить, игра очень интересная\r\nно играть невозможно, потому что это все глюки.\r\n\r\n\r\n\r\nИскренне,\r\n%sender%",
        "Bonjour ? tous,\r\n\r\nJ'ai trouv? beaucoup de bugs dans %subject%,\r\npouvez-vous les r?parer, le jeu est assez int?ressant\r\nmais c'est impossible de jouer ? cause de tout ?a bugs.\r\n\r\n\r\n\r\nSinc?rement,\r\n%sender%",
        "Hi,\r\n\r\nIch habe viele Fehler in %subject% gefunden,\r\nkannst du sie reparieren, das Spiel ist ziemlich interessant\r\naber es ist unm?glich zu spielen, weil all diese Bugs.\r\n\r\n\r\n\r\nAufrichtig,\r\n%sender%",
        "Hola,\r\n\r\nEncontr? muchos errores en %subject%,\r\npuedes arreglarlos, el juego es bastante interesante\r\npero es imposible jugar debido a todos estos errores.\r\n\r\n\r\n\r\nAtentamente,\r\n%sender%",
        "Ciao,\r\n\r\nHo trovato molti bug in %subject%,\r\npuoi aggiustarli, il gioco ? piuttosto interessante\r\nma ? impossibile giocare perch? tutto questo bug.\r\n\r\n\r\n\r\nCordiali saluti,\r\n%sender%",
    };

    private string gameName;

    public BugsMail(string sender, Game game) : base(sender)
    {
        gameName = game.projectName;
    }
    
    public override string GetMessage(int localization)
    {
        string result = text[localization];
        result = result.Replace("%subject%", gameName);
        result = result.Replace("%sender%", sender);
        return result;
    }

    public override string GetTheme()
    {
        return Localization.Localize("mailTheme.0");
    }
}