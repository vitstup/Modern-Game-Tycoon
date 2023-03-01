using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreelanceUI : MonoBehaviour
{
    public static FreelanceUI instance;

    [SerializeField] private GameObject freelancePanel;

    [SerializeField] private TextMeshProUGUI freelanceName;

    [SerializeField] private TextMeshProUGUI plotPoints;
    [SerializeField] private TextMeshProUGUI gameDesignPoints;
    [SerializeField] private TextMeshProUGUI gameplayPoints;
    [SerializeField] private TextMeshProUGUI graphicsPoints;
    [SerializeField] private TextMeshProUGUI soundPoints;

    [SerializeField] private TextMeshProUGUI taskDifficulty;

    [SerializeField] private TextMeshProUGUI payment;
    [SerializeField] private TextMeshProUGUI term;
    [SerializeField] private TextMeshProUGUI penalty;

    private Freelance freelance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NewFreelance();
    }

    public void NewFreelance()
    {
        freelance = new Freelance(Random.Range(0, 5));

        freelanceName.text = freelance.projectName;

        string pointsText = " " + Localization.Localize("lpoints") + ": ";
        int fontWeight = 700;
        plotPoints.text = Localization.Localize("plot") + pointsText + TextConvertor.ChangeWeight(freelance.plotNeeded.ToString(), fontWeight);
        gameDesignPoints.text = Localization.Localize("gamedesign") + pointsText + TextConvertor.ChangeWeight(freelance.gameDesignNeeded.ToString(), fontWeight);
        gameplayPoints.text = Localization.Localize("gameplay") + pointsText + TextConvertor.ChangeWeight(freelance.gameplayNeeded.ToString(), fontWeight);
        graphicsPoints.text = Localization.Localize("graphics") + pointsText + TextConvertor.ChangeWeight(freelance.graphicsNeeded.ToString(), fontWeight);
        soundPoints.text = Localization.Localize("sound") + pointsText + TextConvertor.ChangeWeight(freelance.soundNeeded.ToString(), fontWeight);

        UpdateDifficultyText();

        payment.text = Localization.Localize("payment") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(freelance.payment), Constans.GreenColor);
        term.text = Localization.Localize("term") + ": " + TextConvertor.ChangeColor(freelance.term + " " + Localization.Localize("days"), Constans.GreenColor);
        penalty.text = Localization.Localize("penalty") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(freelance.penalty), Color.red);
    }

    public void StartDeveloping()
    {
        ProjectUI.instance.OpenProjectCanvas(false);
        ProjectManager.instance.DevelopmentStarted(freelance);
        NewFreelance();
    }

    public void UpdateDifficultyText()
    {
        int difficulty = freelance.GetDifficulty();
        taskDifficulty.text = Localization.Localize("taskdifficulty") + string.Format(" <sprite={0}> ", 2 - difficulty) + Localization.Localize("diff." + difficulty);
    }
}