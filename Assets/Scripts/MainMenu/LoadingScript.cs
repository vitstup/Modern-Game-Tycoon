using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public static LoadingScript instance;

    private AsyncOperation operation;

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image loadingProgress;

    private void Awake() => instance = this;

    public void LoadScene(int id)
    {
        operation = SceneManager.LoadSceneAsync(id);
        loadingPanel.SetActive(true);
    }


    private void Update()
    {
        if (operation == null) return;
        loadingProgress.fillAmount = operation.progress;
    }
}