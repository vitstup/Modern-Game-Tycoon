using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectManager : MonoBehaviour
{
    public class developmentEvent : UnityEvent<bool> { }
    public static developmentEvent DevelopmentEvent = new developmentEvent();

    public static ProjectManager instance;

    [field: SerializeField] public Project project { get; private set; }

    public int maxSizeGameCreated; // use this to generate contracts 

    private void Awake()
    {
        instance = this;
        TimeManager.DayUpdateEvent.AddListener(DayUpdate);
    }

    private void DayUpdate()
    {
        if (project != null)
        {
            project.Develop();
            
            if (project is GameProject) CreationUI.instance.UpdateInfo(project as GameProject);
            else if (project is Freelance) CreationUI.instance.UpdateInfo(project as Freelance);
            else if (project is GameUpdate) CreationUI.instance.UpdateInfo(project as GameUpdate);
        }
    }

    public void DevelopmentStarted(Project project)
    {
        this.project = project;
        project.DevelopmentStarted();
        DevelopmentEvent?.Invoke(true);
        // typing sound if have assign workers
    }

    public void DevelopmentStoped()
    {
        DevelopmentEvent?.Invoke(false);
        // no typing sound if have assign workers
    }

    public void CancelProject()
    {
        project.Cancel();
        project = null;
        DevelopmentStoped();
    }

    public void DoneDevelopment()
    {
        project.Done();
        project = null;
        DevelopmentStoped();
    }
}