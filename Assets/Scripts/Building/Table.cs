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

    protected override void Awake()
    {
        base.Awake();
        canvas = GetComponentInChildren<Canvas>();
        assignText = assignBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        model = GetComponentInChildren<PersonaModel>();
    }

    public override void Rotate(bool Right)
    {
        base.Rotate(Right);
        if (Right) canvas.transform.Rotate(0, -45, 0, Space.World);
        else canvas.transform.Rotate(0, 45, 0, Space.World);
    }

    protected override void Move()
    {
        base.Move();
        canvas.gameObject.SetActive(false);
    }

    public override void Put()
    {
        base.Put();
        canvas.gameObject.SetActive(true);
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
}
