using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonaPanelScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI personaName;
    [SerializeField] private TextMeshProUGUI proffesion;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skill;
    [SerializeField] private TextMeshProUGUI salary;

    private Image background;
    [SerializeField] private Image bar;

    [SerializeField] private Toggle toggle;

    [SerializeField] private TextMeshProUGUI assignedText;

    public Persona lastSelected { get; private set; }

    private void Start() => background = GetComponent<Image>();

    public void UpdateInfo(Persona persona)
    {
        personaName.text = persona.personaName;
        proffesion.text = Localization.Localize("proffesion." + persona.skills.mainSkill.ToString());
        int type = 0;
        int skillValue = 0;

        if (RosterUI.instance.sortStatus == SortStatus.name || RosterUI.instance.sortStatus == SortStatus.salary || RosterUI.instance.sortStatus == SortStatus.totalSkills)
        {
            type = persona.skills.mainSkill;
        }
        else
        {
            if (RosterUI.instance.sortStatus == SortStatus.gameDesign) type = 1;
            else if (RosterUI.instance.sortStatus == SortStatus.artDesign) type = 2;
            else if (RosterUI.instance.sortStatus == SortStatus.soundDesign) type = 3;
            else if (RosterUI.instance.sortStatus == SortStatus.screenwriting) type = 4;
        }

        skillName.text = Localization.Localize("skill." + type.ToString());
        skillValue = persona.skills.GetSkill(type);
        skill.text = skillValue.ToString();
        bar.fillAmount = skillValue / 100f;
        salary.text = TextConvertor.moneyText(persona.salary);

        lastSelected = persona;

        if (RosterUI.instance.state == RosterUI.State.Assign)
        {
            if (persona.table != null) assignedText.gameObject.SetActive(true);
            else assignedText.gameObject.SetActive(false);
            toggle.gameObject.SetActive(false);
        }
        else
        {
            assignedText.gameObject.SetActive(false);
            toggle.gameObject.SetActive(true);
            toggle.isOn = false;
        }
    }

    public void Open()
    {
        PersonaTab.instance.OpenPanel(lastSelected);
    }
    
    public void ToggleChanged(bool value)
    {
        RosterUI.instance.SelectPanel(this, value);
    }

    public void ChangeToggle(bool value)
    {
        toggle.isOn = value;
    }
}