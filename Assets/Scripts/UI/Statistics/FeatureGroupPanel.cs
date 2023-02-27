using UnityEngine;
using TMPro;

public class FeatureGroupPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI featureGroupText;
    [SerializeField] private TextMeshProUGUI featureText;

    public void SetInfo(string groupLoc, string featureLoc)
    {
        Debug.Log("group localization " + groupLoc + ", feature localization " + featureLoc);
        featureGroupText.text = Localization.Localize(groupLoc);
        featureText.text = Localization.Localize(featureLoc);
    }
}