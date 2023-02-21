using UnityEngine;

[System.Serializable]
public class DesignSliders : StageSlider
{
    [Range(0, 10)] [SerializeField] private int atmospherity_sightness;
    [Range(0, 10)] [SerializeField] private int abundance_backgroundSound;
    [Range(0, 10)] [SerializeField] private int character_worldDesign;
    [Range(0, 10)] [SerializeField] private int optimization_beautyGraphics;

    public override string[] GetLocalizationKeys()
    {
        return new string[] { "stageslider.8", "stageslider.9", "stageslider.10", "stageslider.11", };
    }

    public override int[] GetSliders()
    {
        return new int[] { atmospherity_sightness, abundance_backgroundSound, character_worldDesign, optimization_beautyGraphics };
    }

    public override void SetSlider(int slider, int value)
    {
        switch (slider)
        {
            case 0: atmospherity_sightness = value; break;
            case 1: abundance_backgroundSound = value; break;
            case 2: character_worldDesign = value; break;
            case 3: optimization_beautyGraphics = value; break;
        }
    }
}