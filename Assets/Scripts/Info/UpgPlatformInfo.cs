using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "upgPlatform", menuName = "Upgradble platform")]
public class UpgPlatformInfo : PlatformInfo
{
    [System.Serializable]
    public class PlatformUodate
    {
        [SerializeField] private Date date;
        [SerializeField] private int _computeCapabilities;
        [SerializeField] private int _graphicCapabilities;
        [SerializeField] private Sprite _sprite;
    }


    [SerializeField] private PlatformUodate[] _platformUodates;

    public PlatformUodate[] platformUodates => _platformUodates;
}