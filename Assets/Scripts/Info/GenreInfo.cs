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

    [System.Serializable]
    private class FeatureBonus
    {
        [field: SerializeField] public FeatureInfo feature { get; private set; }
        [field: SerializeField] public float bonus { get; private set; }
    }

    [SerializeField] private string _localizationKey;
    [SerializeField] private ThemeBonus[] _themeBonuses;
    [SerializeField] private FeatureBonus[] _featuresBonuses;


    [SerializeField] private PrototypingSliders _prototypingSliders;
    [SerializeField] private DevelopingSliders _developingSliders;
    [SerializeField] private DesignSliders _designSliders;

    public string localizationKey => _localizationKey;
    public PrototypingSliders prototypingSliders => _prototypingSliders;
    public DevelopingSliders developingSliders => _developingSliders;
    public DesignSliders designSliders => _designSliders;

    public float GetThemeBonus(ThemeInfo theme)
    {
        for (int i = 0; i < _themeBonuses.Length; i++)
        {
            if (theme == _themeBonuses[i].theme) return _themeBonuses[i].bonus;
        }
        return 0f;
    }

    public float GetFeaturesBonus(FeatureInfo[] features)
    {
        float bonus = 0;
        for (int f = 0; f < features.Length; f++)
        {
            for (int b = 0; b < _featuresBonuses.Length; b++)
            {
                if (features[f] == _featuresBonuses[b].feature ) { bonus += _featuresBonuses[b].bonus; break; }
            }
        }
        return bonus;
    }
}