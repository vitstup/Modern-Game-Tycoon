using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public static AttributesManager instance;

    [field: SerializeField] public Engine[] engines { get; private set; }
    [field: SerializeField] public FeaturesGroup[] features { get; private set; }
    [field: SerializeField] public ThemeInfo[] themes { get; private set; }
    [field: SerializeField] public GenreInfo[] genres { get; private set; }

    private void Awake()
    {
        instance = this;
        SetEngines();
        SetFeatures();
        SetGenres();
        SetThemes();
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

    

    public Engine[] availableEngines()
    {
        List<Engine> avaialable = new List<Engine>();
        for (int i = 0; i < engines.Length; i++)
        {
            if (Date.Enabled(engines[i].info.release, engines[i].info.end)) avaialable.Add(engines[i]);
        }
        return avaialable.ToArray();
    }


    // id methods

    public int GetEngineId(Engine engine)
    {
        for (int i = 0; i < engines.Length; i++)
        {
            if (engine == engines[i]) return i;
        }
        return -1;
    }

    public int GetGenreId(GenreInfo genre)
    {
        for (int i = 0; i < genres.Length; i++)
        {
            if (genre == genres[i]) return i;
        }
        return -1;
    }

    public int GetThemeId(ThemeInfo theme)
    {
        for (int i = 0; i < themes.Length; i++)
        {
            if (theme == themes[i]) return i;
        }
        return -1;
    }

    public int GetFeatureId(FeatureInfo feature)
    {
        int othFeatures = 0;
        for (int i = 0; i < features.Length; i++)
        {
            for (int f = 0; f < features[i].features.Length; f++)
            {
                if (feature == features[i].features[f]) return othFeatures + f;
            }
            othFeatures += features[i].features.Length;
        }
        return -1;
    }

    public FeatureInfo GetFeatureById(int id)
    {
        int othFeatures = 0;
        for (int i = 0; i < features.Length; i++)
        {
            if (id < othFeatures + features[i].features.Length)
            {
                return features[i].features[id - othFeatures];
            }
            othFeatures += features[i].features.Length;
        }
        throw new System.Exception("There is no such id");
    }
}