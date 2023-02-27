using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PolishingStage : Stage
{
    protected override float SlidersEfficiency(GameProject project)
    {
        throw new System.NotImplementedException();
    }

    public override string[] SlidersLocalization()
    {
        throw new System.NotImplementedException();
    }

    public override void SetSlider(int slider, int value)
    {
        throw new System.NotImplementedException();
    }

    public override string StageLocalizationKey()
    {
        return "polishing";
    }

    public override FeaturesGroup.Stage StageType()
    {
        throw new System.NotImplementedException();
    }
}