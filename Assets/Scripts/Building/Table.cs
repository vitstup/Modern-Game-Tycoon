using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Table : Building
{
    private Canvas canvas;
    [SerializeField] private Button assignBtn;
    [SerializeField] private Button pcBtn;
    private TextMeshProUGUI assignText;

    private PersonaModel model;

    [SerializeField] private GameObject[] pcModels;

    [HideInInspector] public int currentPc { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        BuildingManager.buildingSomething.AddListener(ShowCanvas);
        canvas = GetComponentInChildren<Canvas>(true);
        assignText = assignBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
        model = GetComponentInChildren<PersonaModel>();

        assignText.text = Localization.Localize("assign");

        if (pcModels.Length != ComputerManager.instance.computers.Length) Debug.LogError("Something wrong with pc models");
    }

    public override void Rotate(bool Right)
    {
        base.Rotate(Right);
        if (Right) canvas.transform.Rotate(0, -45, 0, Space.World);
        else canvas.transform.Rotate(0, 45, 0, Space.World);
    }

    public override int GetPrice()
    {
        return price + ComputerManager.instance.computers[currentPc].price;
    }

    public override void SetBuilding(bool takeMoney)
    {
        base.SetBuilding(takeMoney);
        canvas.transform.rotation = Quaternion.Euler(30f, transform.rotation.y + 45f, 0f);
        ShowCanvas(false);
    }

    public void SetModernPc() // Use this method only for unspawned prefabs (in shopitem prices update, and before autofurniture)
    {
        if (!isBuilded)
        {
            currentPc = ComputerManager.instance.GetModernPcId();
            UpdatePcModel();
        }
        else Debug.LogError("Trying to set most modern pc, not in prefab");
    }

    private void ShowCanvas(bool show)
    {
        canvas.gameObject.SetActive(!show);
    }

    public void OpenRoster()
    {
        RosterManager.instance.selectedTable = this;
        RosterUI.instance.OpenAssign();
    }

    public void AssignedWorker(Persona persona)
    {
        assignText.text = persona.personaName;
        model.SetModel(persona.modelId);
    }

    public void DeAssignedWorker()
    {
        assignText.text = Localization.Localize("assign");
        model.HideModel();
    }

    public void UpgradePc()
    {
        int newPcId = ComputerManager.instance.GetModernPcId();
        if (newPcId > currentPc)
        {
            Main.instance.MinusMoney(ComputerManager.instance.computers[newPcId].price);
            currentPc = newPcId;
            UpdatePcModel();
            SetPcBtn(false);
        }
        else Debug.LogWarning("Trying to update pc, witch is already modern");
    }

    private void UpdatePcModel()
    {
        for (int i = 0; i < pcModels.Length; i++)
        {
            if (i == currentPc) { pcModels[i].SetActive(true); continue; }
            pcModels[i].SetActive(false);
        }
    }

    public void SetPcBtn(bool show)
    {
        pcBtn.gameObject.SetActive(show);
    }

    public void AfterLoading() // this method used after save loading 
    {
        canvas.transform.rotation = Quaternion.Euler(30f, 45f - transform.rotation.y, 0f);
        UpdatePcModel();
    }
}