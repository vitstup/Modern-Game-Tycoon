using UnityEngine;

public class SteamChecker : MonoBehaviour
{
    public static SteamChecker instance;

    [HideInInspector] public bool steamInitialized;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        steamInitialized = SteamManager.Initialized;
        Debug.Log("Steam initialization status:" + steamInitialized);
    }

    // maybe some anti pirate logic here
}