using TMPro;
using UnityEngine;

public class FreelanceDoneUI : MonoBehaviour
{
    public static FreelanceDoneUI instance;

    [SerializeField] private GameObject freelanceDoneCanvas;

    [SerializeField] private TextMeshProUGUI freelanceName;
    [SerializeField] private TextMeshProUGUI term;
    [SerializeField] private TextMeshProUGUI devDuration;
    [SerializeField] private TextMeshProUGUI payment;
    [SerializeField] private TextMeshProUGUI penalty;
    [SerializeField] private TextMeshProUGUI recievedPayment;

    private Freelance freelance;

    private void Awake()
    {
        instance = this;
    }

    public void OpenFreelanceDone(Freelance freelance)
    {
        freelanceDoneCanvas.SetActive(true);
        TimeManager.instance.NecessaryPause(true);

        freelanceName.text = freelance.projectName;
        term.text = Localization.Localize("term") + ": " + TextConvertor.ChangeColor(freelance.term + " " + Localization.Localize("days"), Constans.GreenColor);
        devDuration.text = Localization.Localize("devduration") + ": " + TextConvertor.ChangeColor(freelance.developmentDuration + " " + Localization.Localize("days"), freelance.developmentDuration > freelance.term ? Color.red : Constans.GreenColor);
        payment.text = Localization.Localize("payment") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(freelance.payment), Constans.GreenColor);
        penalty.text = Localization.Localize("penalty") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(freelance.GetPenalty()), freelance.GetPenalty() > 0? Color.red : Constans.GreenColor);
        recievedPayment.text = Localization.Localize("receivedpayment") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(freelance.GetRecievedPayment()), Constans.GreenColor);

        this.freelance = freelance;
    }

    public void Continue()
    {
        Main.instance.AddMoney(freelance.GetRecievedPayment());
        freelanceDoneCanvas.SetActive(false);
        TimeManager.instance.NecessaryPause(false);
    }
}