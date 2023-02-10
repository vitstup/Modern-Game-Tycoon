using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "engine", menuName = "Engine")]
public class EngineInfo : ScriptableObject
{
    [SerializeField] private Company _company;
    [SerializeField] private int _licensePrice;
    [SerializeField] private float _commision;
    [SerializeField] private float _developmentSpeed;
    [SerializeField] private GenreInfo _genre;
    [SerializeField] private Date _release;
    [SerializeField] private Date _end;

    [SerializeField] private FeatureInfo[] _features;

    [SerializeField] private Sprite _sprite;

    public Company company => _company;
    public int licensePrice => _licensePrice;
    public float commision => _commision;
    public float developmentSpeed => _developmentSpeed;
    public GenreInfo genre => _genre;
    public Date release => _release;
    public Date end => _end;
    public FeatureInfo[] features => _features;
    public Sprite sprite => _sprite;
}