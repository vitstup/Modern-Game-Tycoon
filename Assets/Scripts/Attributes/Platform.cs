using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Platform
{
    [field: SerializeField] public PlatformInfo info { get; private set; }
    [field: SerializeField] public bool boughted { get; private set; }
    public float marketShare;

    public Platform(PlatformInfo info)
    {
        this.info = info;
        if (info.licensePrice == 0) boughted = true;
    }

    public void Buy()
    {
        if (!boughted)
        {
            Main.instance.MinusMoney(info.licensePrice);
            boughted = true;
        }
        else Debug.LogWarning("Trying to buy already boughted platform");
    }
}