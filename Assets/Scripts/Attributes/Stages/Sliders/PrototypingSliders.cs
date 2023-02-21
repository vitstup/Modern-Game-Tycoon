using UnityEngine;

[System.Serializable]
public class PrototypingSliders : StageSlider
{
    [Range(0, 10)] [SerializeField] private int casual_hardcore;
    [Range(0, 10)] [SerializeField] private int kindness_cruelty;
    [Range(0, 10)] [SerializeField] private int length_elaboration;
    [Range(0, 10)] [SerializeField] private int plot_gameplaySignificance;

    public override string[] GetLocalizationKeys()
    {
        return new string[] { "stageslider.0", "stageslider.1", "stageslider.2", "stageslider.3", };
    }

    public override int[] GetSliders()
    {
        return new int[] { casual_hardcore, kindness_cruelty, length_elaboration, plot_gameplaySignificance };
    }

    public override void SetSlider(int slider, int value)
    {
        switch (slider)
        {
            case 0: casual_hardcore = value; break;
            case 1: kindness_cruelty = value; break;
            case 2: length_elaboration = value; break;
            case 3: plot_gameplaySignificance = value; break;
        }
    }
}