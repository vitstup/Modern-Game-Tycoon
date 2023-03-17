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
    }

    public void DevelopmentStoped()
    {
        DevelopmentEvent?.Invoke(false);
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

    public void LoadProject(SaveLoad.ProjectSaver saver) // use this method only for setting current project after save loading
    {
        if (saver == null) return;

        if (saver is SaveLoad.GameSaver) project = new Game(saver as SaveLoad.GameSaver);
        else if (saver is SaveLoad.ContractSaver) project = new Contract(saver as SaveLoad.ContractSaver);
        else if (saver is SaveLoad.FreelanceSaver) project = new Freelance(saver as SaveLoad.FreelanceSaver);
        else if (saver is SaveLoad.GameUpdateSaver) project = new GameUpdate(saver as SaveLoad.GameUpdateSaver);

        DevelopmentEvent?.Invoke(true);
    }
}