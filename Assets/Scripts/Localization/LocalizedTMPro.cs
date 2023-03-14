using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedTMPro : MonoBehaviour
{
    protected TextMeshProUGUI text;
    [SerializeField] protected string Key;

    protected void Awake()
    {
        Localization.LanguageChangedEvent.AddListener(Localize);
    }

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