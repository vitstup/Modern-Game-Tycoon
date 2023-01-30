using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedTMPro : MonoBehaviour
{
    // add listening to language change event
    protected TextMeshProUGUI text;
    [SerializeField] protected string Key;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Localize();
    }

    protected virtual void Localize()
    {
        text.text = Localization.Localize(Key);
    }
}