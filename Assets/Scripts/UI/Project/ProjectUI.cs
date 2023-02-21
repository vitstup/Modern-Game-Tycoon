using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectUI : MonoBehaviour
{
    public static ProjectUI instance;

    [SerializeField] private GameObject ProjectCanvas;
    [SerializeField] private GameObject StageCanvas;
    private void Awake()
    {
        instance = this;
    }

    public void OpenProjectCanvas(bool open)
    {
        TimeManager.instance.NecessaryPause(open);
        ProjectCanvas.SetActive(open);
    }

    public void TryToOpenProjectCanvas()
    {
        if (ProjectManager.instance.project == null) OpenProjectCanvas(true);
    }

    public void OpenStageCanvas()
    {
        StageCanvas.SetActive(true);
        StageUI.instance.SetInfo(ProjectManager.instance.project as GameProject);
        TimeManager.instance.NecessaryPause(true);
    }
}