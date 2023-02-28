using UnityEngine;
using TMPro;

public class ContractDoneUI : MonoBehaviour
{
    public static ContractDoneUI instance;

    [SerializeField] private GameObject contractDoneCanvas;

    [SerializeField] private TextMeshProUGUI contractName;
    [SerializeField] private TextMeshProUGUI term;
    [SerializeField] private TextMeshProUGUI devDuration;
    [SerializeField] private TextMeshProUGUI reqUserRating;
    [SerializeField] private TextMeshProUGUI rating;
    [SerializeField] private TextMeshProUGUI bonus;

    private Contract contract;

    private void Awake()
    {
        instance = this;
    }

    public void OpenContractDoneUI(Contract contract)
    {
        contractDoneCanvas.SetActive(true);
        TimeManager.instance.NecessaryPause(true);

        contractName.text = contract.projectName;
        term.text = Localization.Localize("term") + ": " + TextConvertor.ChangeColor(contract.term + " " + Localization.Localize("days"), Constans.GreenColor);
        devDuration.text = Localization.Localize("devduration") + ": " + TextConvertor.ChangeColor(contract.developmentDuration + " " + Localization.Localize("days"), contract.developmentDuration > contract.term? Color.red : Constans.GreenColor);
        reqUserRating.text = Localization.Localize("requserrating") + ": " + TextConvertor.ChangeColor(TextConvertor.percentText(contract.minScore), Constans.GreenColor);
        rating.text = Localization.Localize("userrating") + ": " + TextConvertor.ChangeColor(TextConvertor.percentText(contract.reviews.UserScore()), contract.reviews.UserScore() > contract.minScore? Constans.GreenColor : Color.red);
        bonus.text = Localization.Localize("receivedbonus") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(contract.GetReceivedBonus()), contract.GetReceivedBonus() > 0? Constans.GreenColor: Color.red);

        this.contract = contract;
    }

    public void Continue()
    {
        Main.instance.AddMoney(contract.GetReceivedBonus());
        contractDoneCanvas.SetActive(false);
        TimeManager.instance.NecessaryPause(false);
    }
}