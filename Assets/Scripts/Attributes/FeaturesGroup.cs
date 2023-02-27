using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FeaturesGroup 
{
    public enum Stage
    {
        prototyping = 0,
        developing = 1,
        design = 2,
    }


    [field: SerializeField] public Stage stage { get; private set; }
    [field: SerializeField] public string localizationKey { get; private set; }
    [field: SerializeField] public string nonValueKey { get; private set; }
    [field: SerializeField] public FeatureInfo[] features { get; private set; }

    [SerializeField] private string filesPath;

    public void SetFeatures()
    {
        string stageFolder = "Prototyping/";
        if (stage == Stage.developing) stageFolder = "Developing/";
        if (stage == Stage.design) stageFolder = "Design/";
        features = Resources.LoadAll<FeatureInfo>("Features/" + stageFolder + filesPath);
    }

    public FeatureInfo[] GetEnabledFeatures(FeatureInfo[] givenFeatures)
    {
        List<FeatureInfo> returnFeatures = new List<FeatureInfo>();
        for (int i = 0; i < features.Length; i++)
        {
            for (int f = 0; f < givenFeatures.Length; f++)
            {
                if (givenFeatures[f] == features[i]) returnFeatures.Add(givenFeatures[f]);
            }
        }
        return returnFeatures.ToArray();
    }

    public string GetSelectedFeatureLocalization(FeatureInfo[] givenFeatures)
    {
        for (int i = 0; i < features.Length; i++)
        {
            for (int f = 0; f < givenFeatures.Length; f++)
            {
                if (givenFeatures[f] == features[i]) return features[i].localizationKey;
            }
        }
        return nonValueKey;
    }
}