using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RichLocalizedTMPro : LocalizedTMPro
{
    private enum State
    {
        Light = 0,
        Semelight = 1,
        Normal = 2,
        Semibold = 3,
        Bold = 4,
    }

    [SerializeField]private State state;

    protected override void Localize()
    {
        StringBuilder sb = new StringBuilder();
        if (state == State.Light) sb.Append("<font-weight=\"300\">");
        else if(state == State.Semelight) sb.Append("<font-weight=\"400\">");
        else if(state == State.Normal) sb.Append("<font-weight=\"500\">");
        else if(state == State.Semibold) sb.Append("<font-weight=\"600\">");
        else if(state == State.Bold) sb.Append("<font-weight=\"700\">");

        sb.Append(Localization.Localize(Key));

        sb.Append("</font-weight>");
        text.text = sb.ToString();
    }
}