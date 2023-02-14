using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public static AttributesManager instance;

    [field: SerializeField] public Platform[] platforms { get; private set; }
    [field: SerializeField] public Engine[] engines { get; private set; }
    [field: SerializeField] public FeaturesGroup[] features { get; private set; }
    [field: SerializeField] public ThemeInfo[] themes { get; private set; }
    [field: SerializeField] public GenreInfo[] genres { get; private set; }

    private void Awake()
    {
        instance = this;
        SetPlatforms();
        SetEngines();
        SetFeatures();
        SetGenres();
        SetThemes();

        TimeManager.DayUpdateEvent.AddListener(UpdateMarketShare);
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

    private void SetFeatures()
    {
        for (int i = 0; i < features.Length; i++)
        {
            features[i].SetFeatures();
        }
    }

    private void SetThemes()
    {
        themes = Resources.LoadAll<ThemeInfo>("Themes");
    }

    private void SetGenres()
    {
        genres = Resources.LoadAll<GenreInfo>("Genres");
    }

    private void UpdateMarketShare()
    {
        float summary = 0;
        for (int i = 0; i < platforms.Length; i++)
        {
            if (!Date.Enabled(platforms[i].info.release, platforms[i].info.end)) continue;
            summary += platforms[i].info.popularity;
        }
        for (int i = 0; i < platforms.Length; i++)
        {
            if (!Date.Enabled(platforms[i].info.release, platforms[i].info.end)) { platforms[i].marketShare = 0; continue; }
            platforms[i].marketShare = platforms[i].info.popularity / summary;
        }
    }

    public Engine[] availableEngines()
    {
        List<Engine> avaialable = new List<Engine>();
        for (int i = 0; i < engines.Length; i++)
        {
            if (Date.Enabled(engines[i].info.release, engines[i].info.end)) avaialable.Add(engines[i]);
        }
        return avaialable.ToArray();
    }

    public Platform[] availablePlatforms()
    {
        List<Platform> avaialable = new List<Platform>();
        for (int i = 0; i < platforms.Length; i++)
        {
            if (Date.Enabled(platforms[i].info.release, platforms[i].info.end)) avaialable.Add(platforms[i]);
        }
        return avaialable.ToArray();
    }
}