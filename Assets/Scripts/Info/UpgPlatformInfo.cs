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
        [field: SerializeField] public int _computeCapabilities { get; private set; }
        [field: SerializeField] public int _graphicCapabilities { get; private set; }
        [field: SerializeField] public Sprite _sprite { get; private set; }

        public bool updated()
        {
            return Date.Released(date);
        }
    }


    [SerializeField] private PlatformUodate[] _platformUodates;

    public override int computeCapabilities => GetCompute();
    public override int graphicCapabilities => GetGraphics();
    public override Sprite sprite => GetSprite();

    //public PlatformUodate[] platformUodates => _platformUodates;

    private int GetCompute()
    {
        for (int i = _platformUodates.Length - 1; i >= 0 ; i--)
        {
            if (_platformUodates[i].updated()) return _platformUodates[i]._computeCapabilities;
        }
        return base.computeCapabilities;
    }
    private int GetGraphics()
    {
        for (int i = _platformUodates.Length - 1; i >= 0; i--)
        {
            if (_platformUodates[i].updated()) return _platformUodates[i]._graphicCapabilities;
        }
        return base.graphicCapabilities;
    }
    private Sprite GetSprite()
    {
        for (int i = _platformUodates.Length - 1; i >= 0; i--)
        {
            if (_platformUodates[i].updated()) return _platformUodates[i]._sprite;
        }
        return base.sprite;
    }
}