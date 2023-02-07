using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetModernPc() // Use this method in shopitem prices update
    {
        if (!isBuilded)
        {
            currentPc = ComputerManager.instance.GetModernPcId();
            UpdatePcModel();
        }
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
}
