using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesUI : MonoBehaviour
{
    public static SalesUI instance;

    [SerializeField] private GameObject SalesCanvas;

    [SerializeField] private SalePanelScript[] panels;


    private void Awake()
    {
        instance = this;
    }

    public void OpenSalesCanvas(bool open)
    {
        SalesCanvas.SetActive(open);
        TryToUpdatePanels();
    }

    public void TryToUpdatePanels()
    {
        if (SalesCanvas.activeSelf) UpdatePanels();
    }

    public void UpdatePanels()
    {
        var games = Statistics.instance.GetReverseGames();
        for (int i = 0; i < panels.Length; i++)
        {
            if (i >= games.Count) { panels[i].gameObject.SetActive(false); continue; }

            panels[i].SetInfo(games[i]);
            panels[i].gameObject.SetActive(true);
        }
    }
}