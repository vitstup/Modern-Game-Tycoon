using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class LocalizedTMProDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    [SerializeField] private string[] Keys;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
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
        Localize();
    }
}