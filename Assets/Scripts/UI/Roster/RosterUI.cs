using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class RosterUI : MonoBehaviour
{
    public static RosterUI instance;

    public enum State
    {
        hirePersonal = 0,
        Personal = 1,
        Assign = 2,
    }

    public State state { get; private set; }

    private State previosState; // return to this state after assign state

    public SortStatus sortStatus { get; private set; }

    [SerializeField] private GameObject RosterCanvas;

    [SerializeField] private TextMeshProUGUI applicationsText;

    [SerializeField] private PersonaPanelScript[] personaPanels;

    [SerializeField] private PersonasTable ProffesionTable;
    [SerializeField] private PersonasTable AverageSkillTable;
    [SerializeField] private PersonasTable TotalSkillTable;

    [SerializeField] private Button interactionBtn;
    [SerializeField] private TextMeshProUGUI interactionText;

    [SerializeField] private Toggle selectToggle;
    [SerializeField] private TextMeshProUGUI selectedCount;
    [SerializeField] private TextMeshProUGUI selectedSalary;

    private List<PersonaPanelScript> selectedPanels = new List<PersonaPanelScript>();

    [SerializeField] private RectTransform LeftPanel;
    [SerializeField] private Transform PanelsTransform;


    [SerializeField] private PersonaPanelScript personaPanelPrefab;
    [SerializeField] private Transform content;


    private void Awake() => instance = this;

    private void Start()
    {
        InitializePersonaPanels();
    }

    private void InitializePersonaPanels()
    {
        personaPanels = new PersonaPanelScript[Constans.maxWorkers];
        for (int i = 0; i < personaPanels.Length; i++)
        {
            personaPanels[i] = Instantiate(personaPanelPrefab, content);
        }
    }

    public void OpenHirePersonal()
    {
        state = State.hirePersonal;
        UpdateUI();
    }

    public void OpenPersonal()
    {
        state = State.Personal;
        UpdateUI();
    }

    public void OpenAssign()
    {
        if (state != State.Assign) previosState = state;
        state = State.Assign;
        RosterCanvas.SetActive(true);
        TimeManager.instance.ChangeRunStatus(RunStatus.stoped);
        UpdateUI();
    }

    public void CloseRoster()
    {
        RosterCanvas.SetActive(false);
        TimeManager.instance.ChangeRunStatus(RunStatus.standart);
    }

    public void ResetAfterAssignStage()
    {
        if (state == State.Assign) { state = previosState; Debug.Log("Changed"); }
    }

    public void UpdateUI()
    {
        var personaList = (state == State.hirePersonal) ? RosterManager.instance.availableWorkers : RosterManager.instance.hiredWorkers;

        ProffesionTable.UpdateInfo(personaList);
        AverageSkillTable.UpdateInfo(personaList);
        TotalSkillTable.UpdateInfo(personaList);
        TotalSkillTable.gameObject.SetActive(true);

         applicationsText.text = personaList.Count.ToString() + " " + Localization.Localize("applications");

        if(state == State.Assign)
        {
            LeftPanel.gameObject.SetActive(false);
            selectToggle.gameObject.SetActive(false);
            interactionBtn.gameObject.SetActive(false);
            TotalSkillTable.gameObject.SetActive(true);
            PanelsTransform.localPosition = new Vector3(LeftPanel.sizeDelta.x / -2, 0);
        }
        else
        {
            LeftPanel.gameObject.SetActive(true);
            selectToggle.gameObject.SetActive(true);
            interactionBtn.gameObject.SetActive(true);
            PanelsTransform.localPosition = new Vector3(0, 0);
            if (state == State.hirePersonal)
            {
                TotalSkillTable.gameObject.SetActive(false);
                interactionText.text = Localization.Localize("hire");
            }
            else if (state == State.Personal)
            {
                TotalSkillTable.gameObject.SetActive(true);
                interactionText.text = Localization.Localize("fire");
            }
        }

        UpdatePanels(Sort(personaList));

        UpdateSelectedInfo();
    }

    public void SortPanels(int value)
    {
        sortStatus = (SortStatus)value;
        UpdateUI();
    }

    private List<Persona> Sort(List<Persona> personas)
    {
        if (sortStatus == SortStatus.name) personas = personas.OrderBy(i => i.personaName).ToList();
        else if (sortStatus == SortStatus.programming) personas = personas.OrderByDescending(i => i.skills.programming).ToList();
        else if (sortStatus == SortStatus.gameDesign) personas = personas.OrderByDescending(i => i.skills.gameDesign).ToList();
        else if (sortStatus == SortStatus.artDesign) personas = personas.OrderByDescending(i => i.skills.artDesign).ToList();
        else if (sortStatus == SortStatus.soundDesign) personas = personas.OrderByDescending(i => i.skills.soundDesign).ToList();
        else if (sortStatus == SortStatus.screenwriting) personas = personas.OrderByDescending(i => i.skills.screenwriting).ToList();
        else if (sortStatus == SortStatus.salary) personas = personas.OrderByDescending(i => i.salary).ToList();
        else if (sortStatus == SortStatus.totalSkills) personas = personas.OrderByDescending(i => i.skills.GetSumm()).ToList();
        return personas;
    }
    
    private void UpdatePanels(List<Persona> personas)
    {
        for (int i = 0; i < personaPanels.Length; i++)
        {
            if (personas.Count <= i) { personaPanels[i].gameObject.SetActive(false); continue; }
            personaPanels[i].gameObject.SetActive(true);
            personaPanels[i].UpdateInfo(personas[i]);
        }
    }

    public void SelectPanel(PersonaPanelScript panel, bool value)
    {
        if (value == true) selectedPanels.Add(panel);
        else selectedPanels.Remove(panel);
        UpdateSelectedInfo();
    }

    public void ToggleChanged(bool value)
    {
        if (value == false)
        {
            while(selectedPanels.Count > 0) selectedPanels[0].ChangeToggle(false);
        }
        else
        {
            for (int i = 0; i < personaPanels.Length; i++)
            {
                if (selectedPanels.Contains(personaPanels[i])) continue;
                if (personaPanels[i].gameObject.activeSelf) personaPanels[i].ChangeToggle(true);
            }
        }
        UpdateSelectedInfo();
    }

    public void UpdateSelectedInfo()
    {
        int salary = 0;
        for (int i = 0; i < selectedPanels.Count; i++)
        {
            salary += selectedPanels[i].lastSelected.salary;
        }
        bool all = false;
        if(state == State.Personal && selectedPanels.Count == RosterManager.instance.hiredWorkers.Count) all = true;
        else if(state == State.hirePersonal && selectedPanels.Count == RosterManager.instance.availableWorkers.Count) all = true;

        selectedSalary.text = TextConvertor.moneyText(salary);

        selectedCount.text = (all && selectedPanels.Count > 0)? Localization.Localize("all") : selectedCount.text = selectedPanels.Count.ToString();

    }

    public void DoInteraction()
    {
        for (int i = 0; i < selectedPanels.Count; i++)
        {
            if (state == State.hirePersonal) RosterManager.instance.HireWorker(selectedPanels[i].lastSelected);
            else if (state == State.Personal) RosterManager.instance.FireWorker(selectedPanels[i].lastSelected);
        }
        selectToggle.isOn = false;
        UpdateUI();
    }
}
