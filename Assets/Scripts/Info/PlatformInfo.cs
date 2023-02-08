using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Platform", fileName = "platform")]
public class PlatformInfo : ScriptableObject
{
    [SerializeField] private int _licensePrice;
    [SerializeField] private float _commision;
    [SerializeField] private int _computeCapabilities;
    [SerializeField] private int _graphicCapabilities;
    [SerializeField] private int _releaseMonth;
    [SerializeField] private int _releaseYear;

    // maybe market share or something like that

    [SerializeField] private Sprite _sprite;

    public int licensePrice => licensePrice;
    public float commision => _commision;
    public int computeCapabilities => _computeCapabilities;
    public int graphicCapabilities => _graphicCapabilities;
    public int releaseMonth => _releaseMonth;
    public int releaseYear => _releaseYear;
    public Sprite sprite => _sprite;
}