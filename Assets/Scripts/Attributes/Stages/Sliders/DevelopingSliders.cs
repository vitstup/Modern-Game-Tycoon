using UnityEngine;

[System.Serializable]
public class DevelopingSliders : StageSlider
{
    [SerializeField] private int convenience_functionality;
    [SerializeField] private int familiarity_innovationess;
    [SerializeField] private int passivity_intensivityGameplay;
    [SerializeField] private int simplicity_depthGameplay;

    public override string[] GetLocalizationKeys()
    {
        return new string[] { "stageslider.4", "stageslider.5", "stageslider.6", "stageslider.7", };
    }

    public override int[] GetSliders()
    {
        return new int[] { convenience_functionality, familiarity_innovationess, passivity_intensivityGameplay, simplicity_depthGameplay };
    }

    public override void SetSlider(int slider, int value)
    {
        switch (slider)
        {
            case 0: convenience_functionality = value; break;
            case 1: familiarity_innovationess = value; break;
            case 2: passivity_intensivityGameplay = value; break;
            case 3: simplicity_depthGameplay = value; break;
        }
    }
}