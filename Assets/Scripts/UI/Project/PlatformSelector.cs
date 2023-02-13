using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlatformSelector : MonoBehaviour
{
    [SerializeField] private int id;

    [SerializeField] private GameObject platformInfoPanel;

    [SerializeField] private TextMeshProUGUI platformName;

    [SerializeField] private TextMeshProUGUI compute;
    [SerializeField] private TextMeshProUGUI graphics;
    [SerializeField] private TextMeshProUGUI marketShare;

    [SerializeField] private Image image;

    public void SetInfo(Platform platform)
    {
        if (platform == null) { SetNoInfo(); return; }
        platformInfoPanel.SetActive(true);

        platformName.text = platform.info.name;
        compute.text = platform.info.computeCapabilities.ToString();
        graphics.text = platform.info.graphicCapabilities.ToString();
        marketShare.text = TextConvertor.percentText(platform.marketShare);

        image.sprite = platform.info.sprite;

    }

    private void SetNoInfo()
    {
        if (id == 0) platformName.text = Localization.Localize("mainplatform");
        else platformName.text = Localization.Localize("platform") + " " + (id + 1);
        platformInfoPanel.SetActive(false);
    }

    public void UndoPlatform()
    {

    }

    public void Interacte()
    {

    }
}