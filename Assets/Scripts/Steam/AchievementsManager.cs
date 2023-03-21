using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Destroy(gameObject); 
    }

    public void SetAchievment(int id)
    {
        if (!SteamChecker.instance.steamInitialized) { Debug.LogWarning("Achievemt not given because steam wasn't initialized"); return; }
        bool completed = Steamworks.SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_2_" + id);
        if (!completed) Debug.LogWarning("Achievement wasn's setted");
        SetStats();
    }

    private void SetStats()
    {
        bool completed = Steamworks.SteamUserStats.StoreStats();
        if (!completed) Debug.LogWarning("User stats not completed");
    }
}