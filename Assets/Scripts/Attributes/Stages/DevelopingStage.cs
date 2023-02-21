using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DevelopingStage : Stage
{
    private DevelopingSliders sliders = new DevelopingSliders();

    protected override float SlidersEfficiency(GameProject project)
    {
        return StageSlider.CompareSliders(project.genre.developingSliders.GetSliders(), sliders.GetSliders());
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
        return "developing";
    }
}