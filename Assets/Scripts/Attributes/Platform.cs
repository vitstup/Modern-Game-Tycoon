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

    public float GetPointsPenalty(int computeUsage, int graphicsUsage)
    {
        if (computeUsage == 0) computeUsage = 1; // for not divideByZeroException
        if (graphicsUsage == 0) computeUsage = 1; // for not divideByZeroException

        float computeEff = info.computeCapabilities < computeUsage? (float)info.computeCapabilities / computeUsage: 1;
        float graphicsEff = info.graphicCapabilities < graphicsUsage ? (float)info.graphicCapabilities / graphicsUsage: 1;

        return (computeEff + graphicsEff) / 2;
    }

    public bool avaialable()
    {
        return Date.Enabled(info.release, info.end);
    }
}