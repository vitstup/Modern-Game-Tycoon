using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnginePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI engineName;
    [SerializeField] private TextMeshProUGUI licensePrice;
    [SerializeField] private TextMeshProUGUI releaseDate;
    [SerializeField] private TextMeshProUGUI genre;
    [SerializeField] private TextMeshProUGUI features;
    [SerializeField] private TextMeshProUGUI developmentSpeed;
    [SerializeField] private TextMeshProUGUI commision;

    [SerializeField] private Image image;


    [SerializeField] private Button interactionBtn;

    private Engine engine;

    public void SetInfo(Engine engine)
    {
        engineName.text = engine.info.name;
        licensePrice.text = TextConvertor.moneyText(engine.info.licensePrice);
        string month = Localization.Localize("month." + (engine.info.release.month + 1));
        releaseDate.text = month + " " + engine.info.release.year;
        genre.text = Localization.Localize(engine.info.genre.localizationKey);
        features.text = engine.info.features.Length.ToString();
        developmentSpeed.text = TextConvertor.percentText(engine.info.developmentSpeed);
        commision.text = TextConvertor.percentText(engine.info.commision);

        image.sprite = engine.info.sprite;

        this.engine = engine;
    }

    public void Interacte()
    {

    }

}