using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "engine", menuName = "Engine")]
public class EngineInfo : ScriptableObject
{
    [SerializeField] private int _licensePrice;
    [SerializeField] private float _commision;
    [SerializeField] private float _developmentSpeed;
    [SerializeField] private GenreInfo _genre;
    [SerializeField] private int _releaseMonth;
    [SerializeField] private int _releaseYear;

    [SerializeField] private FeatureInfo[] _features;

    [SerializeField] private Sprite _sprite;

    public int licensePrice => licensePrice;
    public float commision => _commision;
    public float developmentSpeed => _developmentSpeed;
    public GenreInfo genre => _genre;
    public int releaseMonth => _releaseMonth;
    public int releaseYear => _releaseYear;
    public FeatureInfo[] features => _features;
    public Sprite sprite => _sprite;
}