using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnginePanel : MonoBehaviour
{
    public class engineEvent : UnityEvent<Engine> { }
    public static engineEvent SelectedEngine = new engineEvent();

    [SerializeField] private TextMeshProUGUI engineName;
    [SerializeField] private TextMeshProUGUI licensePrice;
    [SerializeField] private TextMeshProUGUI releaseDate;
    [SerializeField] private TextMeshProUGUI genre;
    [SerializeField] private TextMeshProUGUI features;
    [SerializeField] private TextMeshProUGUI developmentSpeed;
    [SerializeField] private TextMeshProUGUI commision;

    [SerializeField] private Image image;


    [SerializeField] private Button interactionBtn;
    [SerializeField] private TextMeshProUGUI interactionText;

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

        UpdateBtn();
    }

    private void UpdateBtn()
    {
        if (engine.boughted) interactionText.text = Localization.Localize("select");
        else interactionText.text = Localization.Localize("buy");
    }

    public void Interacte()
    {
        if (engine.boughted)
        {
            SelectedEngine?.Invoke(engine);
        } 
        else engine.Buy();
        UpdateBtn();
    }

}