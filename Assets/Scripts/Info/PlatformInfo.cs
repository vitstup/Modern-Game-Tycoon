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
    [SerializeField] private Date _release;
    [SerializeField] private Date _end;

    // maybe market share or something like that

    [SerializeField] private Sprite _sprite;

    public int licensePrice => _licensePrice;
    public float commision => _commision;
    public int computeCapabilities => _computeCapabilities;
    public int graphicCapabilities => _graphicCapabilities;
    public Date release => _release;
    public Date end => _end;
    public Sprite sprite => _sprite;
}