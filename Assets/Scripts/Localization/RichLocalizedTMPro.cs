using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RichLocalizedTMPro : LocalizedTMPro
{
    private enum State
    {
        Light = 0,
        Normal = 1,
        Bold = 2,
    }

    [SerializeField]private State state;

    protected override async void Localize()
    {
        if (text == null) Awake();  
        StringBuilder sb = new StringBuilder();
        if (state == State.Light) sb.Append("<font-weight=\"300\">");
        else if(state == State.Normal) sb.Append("<font-weight=\"400\">");
        else if(state == State.Bold) sb.Append("<font-weight=\"700\">");

        sb.Append(Localization.Localize(Key));

        sb.Append("</font-weight>");
        text.text = sb.ToString();
    }
}