using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonaTab : MonoBehaviour
{
    public static PersonaTab instance;

    [SerializeField] private GameObject Panel;

    [SerializeField] private TextMeshProUGUI personaName;
    [SerializeField] private TextMeshProUGUI proffesion;

    [SerializeField] private TextMeshProUGUI programming;
    [SerializeField] private TextMeshProUGUI gameDesign;
    [SerializeField] private TextMeshProUGUI artDesign;
    [SerializeField] private TextMeshProUGUI soundDesign;
    [SerializeField] private TextMeshProUGUI screenwriting;

    [SerializeField] private TextMeshProUGUI salary;

    [SerializeField] private Button interactionBtn;
    [SerializeField] private TextMeshProUGUI interactionText;

    [SerializeField] private Image programmingBar;
    [SerializeField] private Image gameDesignBar;
    [SerializeField] private Image artDesignBar;
    [SerializeField] private Image soundDesignBar;
    [SerializeField] private Image screenwritingBar;

    private Persona selected;

    private void Awake() => instance = this;

    public void OpenPanel(Persona persona)
    {
        Panel.SetActive(true);

        selected = persona;

        personaName.text = persona.personaName;
        proffesion.text = Localization.Localize("proffesion." + persona.skills.mainSkill);
        programming.text = persona.skills.programming.ToString();
        gameDesign.text = persona.skills.gameDesign.ToString();
        artDesign.text = persona.skills.artDesign.ToString();
        soundDesign.text = persona.skills.soundDesign.ToString();
        screenwriting.text = persona.skills.screenwriting.ToString();

        salary.text = TextConvertor.moneyText(persona.salary);

        programmingBar.fillAmount = persona.skills.programming / 100f;
        gameDesignBar.fillAmount = persona.skills.gameDesign / 100f;
        artDesignBar.fillAmount = persona.skills.artDesign / 100f;
        soundDesignBar.fillAmount = persona.skills.soundDesign / 100f;
        screenwritingBar.fillAmount = persona.skills.screenwriting / 100f;

        if (RosterUI.instance.state == RosterUI.State.Personal)
        {
            interactionText.text = Localization.Localize("fire");
        }
        else if (RosterUI.instance.state == RosterUI.State.hirePersonal)
        {
            interactionText.text = Localization.Localize("hire");
        }
        else if (RosterUI.instance.state == RosterUI.State.Assign)
        {
            interactionText.text = Localization.Localize("assign");
        }
    }

    public void DoInteraction()
    {
        if (RosterUI.instance.state == RosterUI.State.Personal)
        {
            RosterManager.instance.FireWorker(selected);
        }
        else if (RosterUI.instance.state == RosterUI.State.hirePersonal)
        {
            RosterManager.instance.HireWorker(selected);
        }
        else if (RosterUI.instance.state == RosterUI.State.Assign)
        {
            RosterManager.instance.AssignWorker(selected);
        }
        RosterUI.instance.UpdateUI();
    }
}