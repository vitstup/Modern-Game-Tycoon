using UnityEngine;
using TMPro;

public class UpdateDoneUI : MonoBehaviour
{
    public static UpdateDoneUI instance;

    [SerializeField] private GameObject updateDoneCanvas;

    [SerializeField] private TextMeshProUGUI gameName;
    [SerializeField] private TextMeshProUGUI updateText;

    private GameUpdate update;

    private void Awake()
    {
        instance = this;
    }

    public void OpenUpdateDone(GameUpdate update)
    {
        updateDoneCanvas.SetActive(true);
        TimeManager.instance.NecessaryPause(true);

        gameName.text = update.updateGame.projectName;
        updateText.text = Localization.Localize(update.projectName) + " " + Localization.Localize("update");

        this.update = update;
    }

    public void Polish()
    {
        update.isPolishing = true;
        updateDoneCanvas.SetActive(false);
        TimeManager.instance.NecessaryPause(false);
    }

    public void Release()
    {
        ProjectManager.instance.DoneDevelopment();
        updateDoneCanvas.SetActive(false);
        TimeManager.instance.NecessaryPause(false);
    }
}