using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class LocalizedTMProDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    [SerializeField] private string[] Keys;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        Localization.LanguageChangedEvent.AddListener(Localize);
    }

    private void Start()
    {
        Localize();
    }

    private void Localize()
    {
        for (int i = 0; i < Keys.Length; i++)
        {
            dropdown.options[i].text = Localization.Localize(Keys[i]);
        }
    }

    public void SetKeys(string[] Keys)
    {
        this.Keys = Keys;
        if (dropdown == null) Awake();
        Localize();
    }
}