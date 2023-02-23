using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreationUI : MonoBehaviour
{
    public static CreationUI instance;

    [SerializeField] private GameObject creationPanel;

    [SerializeField] private TextMeshProUGUI projectName;

    [SerializeField] private GameObject bugsPanel;
    [SerializeField] private TextMeshProUGUI bugsText;

    [SerializeField] private GameObject fillPanel;
    [SerializeField] private Image fillAmount;
    [SerializeField] private TextMeshProUGUI fillStageText;

    [SerializeField] private GameObject realisePanel;
    [SerializeField] private TextMeshProUGUI realiseStageText;

    [SerializeField] private GameObject cancelPanel;
    [SerializeField] private TextMeshProUGUI forefitText;

    private void Awake()
    {
        instance = this;
        ProjectManager.DevelopmentEvent.AddListener(SetPanel);
    }

    public void UpdateInfo(GameProject game)
    {
        projectName.text = game.projectName;
        bugsText.text = TextConvertor.BugsText(game.bugs);

        if (game.currentStage is PolishingStage)
        {
            realiseStageText.text = Localization.Localize(game.currentStage.StageLocalizationKey());

            realisePanel.SetActive(true);
            fillPanel.SetActive(false);
        }
        else
        {
            fillAmount.fillAmount = game.currentStage.ReadyPercent();
            fillStageText.text = Localization.Localize(game.currentStage.StageLocalizationKey());
            realisePanel.SetActive(false);
            fillPanel.SetActive(true);
        }
    }

    public void UpdateInfo(Freelance freelance)
    {

    }

    public void UpdateInfo(GameUpdate update)
    {

    }

    private void SetPanel(bool show)
    {
        creationPanel.SetActive(show);
        if (show)
        {
            if (ProjectManager.instance.project is GameProject)
            {
                bugsPanel.SetActive(true);
                UpdateInfo(ProjectManager.instance.project as GameProject);
            }
        }
    }

    public void OpenCancelPanel()
    {
        cancelPanel.SetActive(true);
        int forefit = ProjectManager.instance.project.GetForefit();
        forefitText.gameObject.SetActive(forefit > 0);
        forefitText.text = Localization.Localize("forfeit") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(forefit), Color.red);
    }

    public void Release()
    {
        GameReadyUI.instance.OpenGameReady(ProjectManager.instance.project as GameProject);
    }
}