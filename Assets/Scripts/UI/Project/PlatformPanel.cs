using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlatformPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI platformName;
    [SerializeField] private TextMeshProUGUI licensePrice;
    [SerializeField] private TextMeshProUGUI releaseDate;
    [SerializeField] private TextMeshProUGUI computing;
    [SerializeField] private TextMeshProUGUI graphics;
    [SerializeField] private TextMeshProUGUI marketShare;
    [SerializeField] private TextMeshProUGUI commision;

    [SerializeField] private Image image;

    [SerializeField] private Button interactionBtn;

    private Platform platform;

    public void SetInfo(Platform platform)
    {
        platformName.text = platform.info.name;
        licensePrice.text = TextConvertor.moneyText(platform.info.licensePrice);
        string month = Localization.Localize("month." + (platform.info.release.month + 1));
        releaseDate.text = month + " " + platform.info.release.year;
        computing.text = "<sprite=0>" + platform.info.computeCapabilities;
        graphics.text = "<sprite=1>" + platform.info.graphicCapabilities;
        marketShare.text = "<sprite=2>" + TextConvertor.percentText(platform.marketShare);
        commision.text = TextConvertor.percentText(platform.info.commision);

        image.sprite = platform.info.sprite;

        this.platform = platform;
    }
    public void Interacte()
    {

    }

}