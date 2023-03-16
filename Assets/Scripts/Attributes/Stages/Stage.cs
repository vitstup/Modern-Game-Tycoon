using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Stage 
{
    public List<FeatureInfo> features = new List<FeatureInfo>();

    public float workload { get; private set; }
    public float workloadDone;

    public int CalculateWorkload(GameProject project)
    {
        int workload = 50;
        for (int i = 0; i < features.Count; i++)
        {
            workload += features[i].workload;
        }
        workload *= Constans.sizesScale[project.size];
        this.workload = workload;
        return workload;
    }

    public int GetComputeUsage()
    {
        int usage = 0;
        for (int i = 0; i < features.Count; i++)
        {
            usage += features[i].computeUsage;
        }
        return usage;
    }

    public int GetGraphicsUsage()
    {
        int usage = 0;
        for (int i = 0; i < features.Count; i++)
        {
            usage += features[i].graphicUsage;
        }
        return usage;

    }
    
    public bool StageDone()
    {
        return workloadDone >= workload && workload > 0;
    }

    public float ReadyPercent()
    {
        return workloadDone / workload;
    }

    public float StageEfficiency(GameProject project)
    {
        return FeaturesEfficiency(project) + SlidersEfficiency(project);
    }

    protected float FeaturesEfficiency(GameProject project)
    {
        return project.genre.GetFeaturesBonus(features.ToArray());
    }

    protected abstract float SlidersEfficiency(GameProject project);

    public abstract string[] SlidersLocalization();

    public abstract void SetSlider(int slider, int value);

    public abstract string StageLocalizationKey();

    public abstract FeaturesGroup.Stage StageType();

    public abstract StageSlider GetSliders();

    public string[,] GetGroupsLocalization()
    {
        int currentGroup = 0;
        string[,] localizations = new string[5, 2];
        var groups = AttributesManager.instance.features;
        for (int i = 0; i < groups.Length; i++)
        {
            if (groups[i].stage == StageType())
            {
                localizations[currentGroup, 0] = groups[i].localizationKey;
                localizations[currentGroup, 1] = groups[i].GetSelectedFeatureLocalization(features.ToArray());
                currentGroup++;
            }
        }
        return localizations;
    }

    public Stage() { }

    public Stage(SaveLoad.GameProjectSaver.StageSaver saver)
    {
        workload = saver.workload;
        workloadDone = saver.workloadDone;

        for (int i = 0; i < saver.features.Length; i++)
        {
            features.Add(AttributesManager.instance.GetFeatureById(saver.features[i]));
        }

        var sliders = GetSliders();
        for (int i = 0; i < saver.sliders.Length; i++)
        {
            sliders.SetSlider(i, saver.sliders[i]);
        }
    }
}