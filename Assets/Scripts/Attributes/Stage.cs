using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage 
{
    public List<FeatureInfo> features = new List<FeatureInfo>();

    public StageSlider sliders;

    // workload

    // development efficiency

    public Stage(StageSlider sliders)
    {
        this.sliders = sliders;
    }

    public int CalculateWorkload(GameProject project)
    {
        int workload = 50;
        for (int i = 0; i < features.Count; i++)
        {
            workload += features[i].workload;
        }
        return workload * Constans.sizesScale[project.size];
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
    
}