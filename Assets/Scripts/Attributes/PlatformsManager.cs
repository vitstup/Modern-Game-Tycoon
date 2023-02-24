using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    public static PlatformsManager instance;

    [field: SerializeField] public Platform[] platforms { get; private set; }

    [field: SerializeField] public float unavailableAuditory { get; private set; }

    private void Awake()
    {
        instance = this;
        SetPlatforms();
        TimeManager.MonthUpdateEvent.AddListener(UpdateMarketShare);
    }

    private void Start()
    {
        UpdateMarketShare();
    }


    private void SetPlatforms()
    {
        var info = Resources.LoadAll<PlatformInfo>("Platforms");
        platforms = new Platform[info.Length];
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = new Platform(info[i]);
        }
    }

    private void UpdateMarketShare()
    {
        float availableSumm = 0;
        float unavailableSumm = 0;
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].avaialable()) availableSumm += platforms[i].info.popularity;
            else unavailableSumm += platforms[i].info.popularity;
        }
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].marketShare = platforms[i].info.popularity / (platforms[i].avaialable()? availableSumm : unavailableSumm);
        }
        unavailableAuditory = unavailableSumm / (availableSumm + unavailableSumm);
    }

    public Platform[] availablePlatforms()
    {
        List<Platform> avaialable = new List<Platform>();
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].avaialable()) avaialable.Add(platforms[i]);
        }
        return avaialable.ToArray();
    }
}