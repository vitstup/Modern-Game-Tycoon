using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSliderUI : MonoBehaviour
{
    [field: SerializeField] public int id { get; private set; }
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    public void SetText(string localizationKey)
    {
        sliderText.text = Localization.Localize(localizationKey);
    }

    public void SliderValueChanged(float value)
    {
        var game = ProjectManager.instance.project as GameProject;
        game.currentStage.SetSlider(id, (int)value);
    }

    public void ChangeValue(int value)
    {
        slider.value = value;
    }
}