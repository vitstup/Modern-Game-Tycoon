using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficesUI : MonoBehaviour
{
    public static OfficesUI instance;

    [SerializeField] private OfficePanel officePanelPrefab;
    [SerializeField] private Transform content;

    private OfficePanel[] panels;

    private void Awake() => instance = this;

    public void SetOfficePanels(Office[] offices)
    {
        panels = new OfficePanel[offices.Length];
        for (int i = 0; i < offices.Length; i++)
        {
            panels[i] = Instantiate(officePanelPrefab, content);
            panels[i].SetInfo(offices[i], i);
        }
        UpdatePanels();
    }

    public void UpdatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].UpdateBtns(i == OfficeManager.instance.currentOffice);
        }
    }
}