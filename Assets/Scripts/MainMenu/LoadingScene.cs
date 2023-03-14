using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private void Start()
    {
        LoadingScript.instance.LoadScene(1);
    }
}