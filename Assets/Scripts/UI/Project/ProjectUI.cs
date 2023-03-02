using UnityEngine;

public class ProjectUI : MonoBehaviour
{
    public static ProjectUI instance;

    [SerializeField] private GameObject ProjectCanvas;
    [SerializeField] private GameObject StageCanvas;

    [SerializeField] private SequelDropdown sequelDropdown;
    [SerializeField] private SequelDropdown updatesDropdown;
    private void Awake()
    {
        instance = this;
    }

    public void OpenProjectCanvas(bool open)
    {
        TimeManager.instance.NecessaryPause(open);
        ProjectCanvas.SetActive(open);

        FreelanceUI.instance.UpdateDifficultyText();
        sequelDropdown.UpdateVariants();
        updatesDropdown.UpdateVariants();
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