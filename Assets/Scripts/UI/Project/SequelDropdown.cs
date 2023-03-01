using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SequelDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private Game[] possibleForSequels;

    private void Awake() => dropdown = GetComponent<TMP_Dropdown>();

    private void Start() => UpdateVariants();


    public void UpdateVariants()
    {
        var games = Statistics.instance.GetGamesWithoutSequel();

        dropdown.options.Clear();
        dropdown.options.Add(new TMP_Dropdown.OptionData() { text = Localization.Localize("none") });

        for (int i = 0; i < games.Length; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = games[i].projectName });
        }

        possibleForSequels = games;
    }

    public Game GetCurrentValue()
    {
        if (dropdown.value == 0) return null;
        else return possibleForSequels[dropdown.value - 1];
    }

    public void SetValue(int value)
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
            UpdateVariants();
        }
        dropdown.value = value;
    }

    // dropdown.options.Add(new TMP_Dropdown.OptionData() { text = GA.features[features[i]].name });
}