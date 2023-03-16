using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DesignStage : Stage
{
    private DesignSliders sliders = new DesignSliders();

    protected override float SlidersEfficiency(GameProject project)
    {
        return StageSlider.CompareSliders(project.genre.designSliders.GetSliders(), sliders.GetSliders());
    }

    public override string[] SlidersLocalization()
    {
        return sliders.GetLocalizationKeys();
    }

    public override void SetSlider(int slider, int value)
    {
        sliders.SetSlider(slider, value);
    }

    public override string StageLocalizationKey()
    {
        return "design";
    }

    public override FeaturesGroup.Stage StageType()
    {
        return FeaturesGroup.Stage.design;
    }
    public override StageSlider GetSliders()
    {
        return sliders;
    }
    public DesignStage() { }
    public DesignStage(SaveLoad.GameProjectSaver.StageSaver saver) : base(saver) { }
}