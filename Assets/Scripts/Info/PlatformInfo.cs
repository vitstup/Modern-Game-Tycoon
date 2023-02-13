using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Platform", fileName = "platform")]
public class PlatformInfo : ScriptableObject
{
    

    [SerializeField] private Company _company;
    [SerializeField] private int _licensePrice;
    [SerializeField] private float _commision;
    [SerializeField] private int _computeCapabilities;
    [SerializeField] private int _graphicCapabilities;
    [SerializeField] private Date _release;
    [SerializeField] private Date _end;

    [Range(0, 1f)]
    [SerializeField] private float _popularity;

    // maybe market share or something like that

    [SerializeField] private Sprite _sprite;

    public Company company => _company;
    public int licensePrice => _licensePrice;
    public float commision => _commision;
    public virtual int computeCapabilities => _computeCapabilities;
    public virtual int graphicCapabilities => _graphicCapabilities;
    public Date release => _release;
    public Date end => _end;
    public float popularity => _popularity;
    public virtual Sprite sprite => _sprite;
}