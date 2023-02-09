using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public static AttributesManager instance;

    [field: SerializeField] public Platform[] platforms { get; private set; }
    [field: SerializeField] public Engine[] engines { get; private set; }

    private void Awake()
    {
        instance = this;
        SetPlatforms();
        SetEngines();
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

    private void SetEngines()
    {
        var info = Resources.LoadAll<EngineInfo>("Engines");
        engines = new Engine[info.Length];
        for (int i = 0; i < engines.Length; i++)
        {
            engines[i] = new Engine(info[i]);
        }
    }
}