using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Feature", fileName = "feature")]
public class FeatureInfo : ScriptableObject
{
    [SerializeField] private string _localizationKey;
    [SerializeField] private int _computeUsage;
    [SerializeField] private int _graphicUsage;
    // points
    // maybe features group enum

    public string localizationKey => _localizationKey;
    public int computeUsage => _computeUsage;
    public int graphicUsage => _graphicUsage;
}