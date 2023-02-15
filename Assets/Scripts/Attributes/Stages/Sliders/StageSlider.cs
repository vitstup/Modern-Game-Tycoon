using UnityEngine;

[System.Serializable]
public abstract class StageSlider
{
    public abstract void SetSlider(int slider, int value);

    public abstract int[] GetSliders();

    public abstract string[] GetLocalizationKeys();

    public static float CompareSliders(int[] sliders1, int[] sliders2)
    {
        float baf = 0;
        if (sliders1.Length != sliders2.Length) Debug.LogError("Different sliders length, something is wrong");
        for (int i = 0; i < sliders1.Length; i++)
        {
            int difference = System.Math.Abs(sliders1[i] - sliders2[i]);
            baf += 0.25f - (0.025f * difference);
        }
        return baf;
    }
}