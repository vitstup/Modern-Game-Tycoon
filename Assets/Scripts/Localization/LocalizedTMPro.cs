using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedTMPro : MonoBehaviour
{
    // add listening to language change event
    private TextMeshProUGUI text;
    [SerializeField]private string Key;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Localize();
    }

    private void Localize()
    {
        text.text = Localization.Localize(Key);
    }
}