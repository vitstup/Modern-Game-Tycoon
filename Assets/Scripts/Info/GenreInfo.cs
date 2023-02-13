using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "genre", menuName = "Genre")]
public class GenreInfo : ScriptableObject
{
    [System.Serializable]
    private class ThemeBonus
    {
        [field: SerializeField] public ThemeInfo theme { get; private set; }
        [field: SerializeField] public float bonus { get; private set; }

    }

    [SerializeField] private string _localizationKey;
    [SerializeField] private ThemeBonus[] _themeBonuses;

    // stage sliders
    // maybe bonuses from features

    public string localizationKey => _localizationKey;

    public float GetThemeBonus(ThemeInfo theme)
    {
        for (int i = 0; i < _themeBonuses.Length; i++)
        {
            if (theme == _themeBonuses[i].theme) return _themeBonuses[i].bonus;
        }
        return 0f;
    }
}