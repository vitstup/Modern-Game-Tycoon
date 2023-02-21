using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeatureToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image toggleImg;
    [SerializeField] private Sprite inactiveToggle;
    [SerializeField] private Sprite activeToggle;
    [SerializeField] private Transform Handle;

    private Vector3 handleInactive = new Vector3(-16f, 0, 0);
    private Vector3 handleActive = new Vector3(16f, 0, 0);

    [SerializeField] private TextMeshProUGUI featureName;
    [SerializeField] private TextMeshProUGUI computing;
    [SerializeField] private TextMeshProUGUI graphics;

    private FeatureInfo feature;


    public void SetInfo(FeatureInfo feature, ToggleGroup toggleGroup)
    {
        toggle.group = toggleGroup;
        featureName.text = Localization.Localize(feature.localizationKey);
        computing.text = feature.computeUsage.ToString();
        graphics.text = feature.graphicUsage.ToString();

        this.feature = feature;

        toggle.isOn = false;
    }

    private void ToggleAnimation(bool value)
    {
        toggleImg.sprite = value ? activeToggle : inactiveToggle;
        Handle.localPosition = value ? handleActive : handleInactive;
    }

    public void ToggleChanged(bool value)
    {
        ToggleAnimation(value);
        var game = ProjectManager.instance.project as GameProject;
        if (value) game.currentStage.features.Add(feature);
        else game.currentStage.features.Remove(feature); // maybe check if list have this element before removing it
        StageUI.instance.UpdateDynamicTexts();
    }
}